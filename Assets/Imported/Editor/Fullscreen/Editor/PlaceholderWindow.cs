#region

using System;
using UnityEditor;
using UnityEngine;

#endregion

namespace FullscreenEditor
{
    /// <summary>The window that will be shown in the place of the original view when creating a fullscreen container.</summary>
    public class PlaceholderWindow : EditorWindow
    {
        const float PREVIEW_FRAMERATE = 24f;

        [SerializeField] Vector2 m_scroll;
        [SerializeField] FullscreenContainer m_fullscreenContainer;
        [SerializeField] bool m_containerForcefullyClosed;

        double m_nextUpdate;
        RenderTexture m_previewRT;

        FullscreenContainer FullscreenContainer
        {
            get
            {
                if (m_containerForcefullyClosed)
                {
                    return null;
                }

                if (!m_fullscreenContainer)
                {
                    m_fullscreenContainer = FullscreenUtility.GetRef<FullscreenContainer>(name);
                }

                if (!m_fullscreenContainer)
                {
                    m_containerForcefullyClosed = true;
                }

                return m_fullscreenContainer;
            }
            set { m_fullscreenContainer = value; }
        }

        bool PreviewSupported
        {
            get
            {
                return FullscreenContainer &&
                       !FullscreenContainer.Rect.Overlaps(position) &&
                       FullscreenContainer.m_dst.View &&
                       FullscreenContainer.m_src.Window &&
                       FullscreenContainer.m_dst.View.HasMethod("GrabPixels");
            }
        }

        void Update()
        {
            if (EditorApplication.timeSinceStartup < m_nextUpdate)
            {
                return;
            }

            m_nextUpdate = EditorApplication.timeSinceStartup + 1f / PREVIEW_FRAMERATE;
            RenderTexture.ReleaseTemporary(m_previewRT);
            m_previewRT = null;

            if (!PreviewSupported || m_containerForcefullyClosed)
            {
                return;
            }

            ScriptableObject view = FullscreenContainer ? FullscreenContainer.m_dst.View : null;
            int width = (int)FullscreenContainer.Rect.width;
            int height = (int)FullscreenContainer.Rect.height;

            if (!view || width < 10 || height < 10)
            {
                return;
            }

            m_previewRT = RenderTexture.GetTemporary(width, height, 0);
            view.InvokeMethod("GrabPixels", m_previewRT, new Rect(0f, 0f, width, height));
            Repaint();
        }

        void OnDisable()
        {
            RenderTexture.ReleaseTemporary(m_previewRT);
        }

        void OnGUI()
        {
            using (EditorGUILayout.ScrollViewScope scrollScope = new(m_scroll))
            {
                using (EditorGUILayout.VerticalScope mainScope = new(Styles.backgroundShadow))
                {
                    m_scroll = scrollScope.scrollPosition;

                    using (new GUIColor(Color.white, 0.1f))
                    {
                        if (m_previewRT)
                        {
                            Rect rtRect = SystemInfo.graphicsUVStartsAtTop
                                ? Rect.MinMaxRect(0f, 1f, 1f, 0f)
                                : // Direct3D like
                                Rect.MinMaxRect(0f, 0f, 1f, 1f); // OpenGL like

                            GUI.DrawTextureWithTexCoords(mainScope.rect, m_previewRT, rtRect);
                        }
                    }

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
                            if (FullscreenContainer && FullscreenContainer.ActualViewPyramid.Container)
                            {
                                GUILayout.Label("The view that lives here is in fullscreen mode", Styles.textStyle);
                                GUILayout.Label("Don't close this placeholder", Styles.textStyle);
                            }
                            else
                            {
                                GUILayout.Label(
                                    "The view that lived here was forcefully closed while in fullscreen, restore is not available",
                                    Styles.textStyle);
                                GUILayout.Label("Consider using the shortcuts to exit fullscreen", Styles.textStyle);
                                GUILayout.Label("You may close this placeholder", Styles.textStyle);
                            }
                        }

                        GUILayout.FlexibleSpace();
                    }

                    using (new EditorGUILayout.HorizontalScope())
                    {
                        GUILayout.FlexibleSpace();

                        if (FullscreenContainer && FullscreenContainer.ActualViewPyramid.Container)
                        {
                            if (GUILayout.Button("Restore View", Styles.buttonStyle))
                            {
                                FullscreenContainer.Close();
                            }
                        }
                        else if (GUILayout.Button("Close Placeholder", Styles.buttonStyle))
                        {
                            Close();
                        }

                        GUILayout.FlexibleSpace();
                    }

                    GUILayout.FlexibleSpace();

                    // This GUI may be called after "this" has been destroyed, that causes a NullReference for the "name" getter
                    // Not sure why this happens tho
                    EditorGUILayout.LabelField(SafeObjToString(() => name));
                    EditorGUILayout.Space();

                    if (FullscreenUtility.DebugModeEnabled)
                    {
                        EditorGUILayout.LabelField("Forcefully Closed",
                            SafeObjToString(() => m_containerForcefullyClosed));

                        EditorGUILayout.LabelField("Container", SafeObjToString(() => FullscreenContainer));
                        EditorGUILayout.LabelField("Rect", SafeObjToString(() => FullscreenContainer.Rect));

                        EditorGUILayout.LabelField("SRC Window",
                            SafeObjToString(() => FullscreenContainer.m_src.Window));
                        EditorGUILayout.LabelField("SRC View", SafeObjToString(() => FullscreenContainer.m_src.View));
                        EditorGUILayout.LabelField("SRC Container",
                            SafeObjToString(() => FullscreenContainer.m_src.Container));

                        EditorGUILayout.LabelField("DST Window",
                            SafeObjToString(() => FullscreenContainer.m_dst.Window));
                        EditorGUILayout.LabelField("DST View", SafeObjToString(() => FullscreenContainer.m_dst.View));
                        EditorGUILayout.LabelField("DST Container",
                            SafeObjToString(() => FullscreenContainer.m_dst.Container));

                        EditorGUILayout.Space();
                    }
                }
            }
        }

        string SafeObjToString<T>(Func<T> obj)
        {
            try
            {
                T i = obj();
                return i == null ? "null" : i.ToString();
            }
            catch
            {
                return "invalid";
            }
        }

        static class Styles
        {
            public static readonly GUIStyle textStyle = new("BoldLabel");
            public static readonly GUIStyle backgroundShadow = new("InnerShadowBg");
            public static readonly GUIStyle buttonStyle = new("LargeButton");

            static Styles()
            {
                textStyle.wordWrap = true;
                textStyle.alignment = TextAnchor.MiddleCenter;
            }
        }
    }
}