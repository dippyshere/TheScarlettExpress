#region

using UnityEditor;
using UnityEngine;

#endregion

namespace FullscreenEditor
{
    public class DisableSceneView
    {
        // TODO: Patcher resets the method to original after a while
        // TODO: or the shouldSkipRender is not being properly calculated
        static readonly Patcher patcher = new(
            typeof(SceneView).FindMethod("OnGUI"), // Original method
            typeof(DisableSceneView).FindMethod("OnGUI") // Replacement
        );

        public static bool RenderingDisabled
        {
            get { return patcher.IsPatched(); }
            set
            {
                if (value == patcher.IsPatched())
                {
                    return;
                }

                if (!value)
                {
                    patcher.Revert();
                }
                else if (FullscreenPreferences.DisableSceneViewRendering)
                {
                    patcher.SwapMethods();
                }

                if (!value || FullscreenPreferences.DisableSceneViewRendering)
                {
                    foreach (Camera c in SceneView.GetAllSceneCameras())
                    {
                        c.gameObject.SetActive(!value);
                    }
                }

                Logger.Debug("{0} Scene View Rendering", value ? "Disabled" : "Enabled");
                SceneView.RepaintAll();
            }
        }

        [InitializeOnLoadMethod]
        static void Init()
        {
            // Initial
            RenderingDisabled = Fullscreen.GetAllFullscreen().Length > 0;

            // On preferences change
            FullscreenPreferences.DisableSceneViewRendering.OnValueSaved += v =>
                RenderingDisabled = v && Fullscreen.GetAllFullscreen().Length > 0;

            // On fullscreen open
            FullscreenCallbacks.afterFullscreenOpen += f =>
                RenderingDisabled = true;

            // Disable the patching if we're the last fullscreen open
            FullscreenCallbacks.afterFullscreenClose += f =>
            {
                if (Fullscreen.GetAllFullscreen().Length <= 1)
                {
                    RenderingDisabled = false;
                }
            };
        }

        // This should not be a static method, as static has no this
        // However, the 'this' in the method is unreliable and should be casted before using
        void OnGUI()
        {
            SceneView _this = (object)this as SceneView;
            ViewPyramid vp = new(_this);
            FullscreenContainer
                shouldRender =
                    Fullscreen.GetFullscreenFromView(vp.Container, false); // Render if this window is in fullscreen

            _this.camera.gameObject.SetActive(shouldRender);

            if (shouldRender)
            {
                patcher.InvokeOriginal(_this); // This possibly throws a ExitGUIException
            }
            else
            {
                CustomOnGUI();
            }
        }

        void CustomOnGUI()
        {
            using (EditorGUILayout.VerticalScope mainScope = new(Styles.backgroundShadow))
            {
                using (new GUIColor(Styles.textStyle.normal.textColor * 0.05f))
                {
                    GUI.DrawTexture(mainScope.rect, FullscreenUtility.FullscreenIcon, ScaleMode.ScaleAndCrop);
                }

                GUILayout.FlexibleSpace();

                using (new EditorGUILayout.HorizontalScope())
                {
                    GUILayout.FlexibleSpace();

                    using (new GUIContentColor(Styles.textStyle.normal.textColor))
                    {
                        GUILayout.Label(FullscreenUtility.FullscreenIcon, Styles.textStyle);
                    }

                    using (new EditorGUILayout.VerticalScope())
                    {
                        GUILayout.Label("Scene View rendering has been disabled\nto improve fullscreen performance",
                            Styles.textStyle);
                    }

                    GUILayout.FlexibleSpace();
                }

                using (new EditorGUILayout.HorizontalScope())
                {
                    GUILayout.FlexibleSpace();
                    if (GUILayout.Button("Enable Temporarily", Styles.buttonStyle))
                    {
                        EnableRenderingTemporarily();
                        SceneView _this = (object)this as SceneView;
                        _this.Focus();
                    }

                    if (GUILayout.Button("Enable Permanently", Styles.buttonStyle))
                    {
                        EnableRenderingPermanently();
                        SceneView _this = (object)this as SceneView;
                        _this.Focus();
                    }

                    GUILayout.FlexibleSpace();
                }

                GUILayout.Label("* This can be changed later in the preferences", Styles.secondaryTextStyle);
                GUILayout.FlexibleSpace();
            }
        }

        public static void EnableRenderingTemporarily()
        {
            RenderingDisabled = false;
        }

        public static void EnableRenderingPermanently()
        {
            FullscreenPreferences.DisableSceneViewRendering.Value = false;
        }

        static class Styles
        {
            public static readonly GUIStyle textStyle = new("BoldLabel");
            public static readonly GUIStyle backgroundShadow = new("InnerShadowBg");
            public static readonly GUIStyle buttonStyle = new("LargeButton");
            public static readonly GUIStyle secondaryTextStyle = new(EditorStyles.centeredGreyMiniLabel);

            static Styles()
            {
                textStyle.wordWrap = true;
                textStyle.alignment = TextAnchor.MiddleCenter;
            }
        }
    }
}