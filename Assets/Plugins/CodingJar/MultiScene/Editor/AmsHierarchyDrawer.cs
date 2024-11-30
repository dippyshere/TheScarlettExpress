#region

using System.Collections.ObjectModel;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

#endregion

namespace CodingJar.MultiScene.Editor
{
    static class AmsHierarchyDrawer
    {
        static GUIStyle _justifyRightLabel;
        static GUIStyle _justifyRightPopup;

        [InitializeOnLoadMethod]
        static void HookUpDrawer()
        {
            EditorApplication.hierarchyWindowItemOnGUI -= OnHierarchyWindowItemOnGUI;
            EditorApplication.hierarchyWindowItemOnGUI += OnHierarchyWindowItemOnGUI;
        }

        static void OnHierarchyWindowItemOnGUI(int instanceID, Rect selectionRect)
        {
            if (Application.isPlaying)
            {
                return;
            }

            // We can't early out because now we have widgets that respond to events.
            // We could potentially early-out on EventType.Used (which is used during scrolling).
            //if ( Event.current.type != EventType.Repaint )
            //	return;

            if (_justifyRightLabel == null)
            {
                _justifyRightLabel = new GUIStyle(GUI.skin.label);
                _justifyRightLabel.alignment = TextAnchor.UpperRight;
                _justifyRightLabel.richText = true;
            }

            if (_justifyRightPopup == null)
            {
                _justifyRightPopup = new GUIStyle(GUI.skin.FindStyle("Popup"));
                _justifyRightPopup.stretchWidth = false;
                _justifyRightPopup.alignment = TextAnchor.UpperRight;
                _justifyRightPopup.richText = true;
            }

            // Which object are we looking at?
            Object obj = EditorUtility.InstanceIDToObject(instanceID);
            bool bIsSceneHeader = instanceID != 0 && !obj;
            if (!bIsSceneHeader)
            {
                return;
            }

            // Make sure we have a scene
            Scene scene = GetSceneFromHandleID(instanceID);
            if (!scene.IsValid())
            {
                return;
            }

            selectionRect.xMax -= 32.0f;

            // Now figure out what these scene settings are
            Scene activeScene = SceneManager.GetActiveScene();
            AmsMultiSceneSetup sceneSetup = SceneManager.GetActiveScene().GetSceneSingleton<AmsMultiSceneSetup>(false);
            if (!sceneSetup)
            {
                GUI.Label(selectionRect, ColorText("AMS Not Found in " + activeScene.name, Color.red),
                    _justifyRightLabel);
                return;
            }

            // If we're the active scene...
            if (activeScene == scene)
            {
                GUI.Label(selectionRect, ColorText("<b>Active</b>", Color.green), _justifyRightLabel);
                return;
            }

            ReadOnlyCollection<AmsMultiSceneSetup.SceneEntry> entries = sceneSetup.GetSceneSetup();
            AmsMultiSceneSetup.SceneEntry entry = entries.FirstOrDefault(x => x.scene.editorPath == scene.path);
            if (entry == null)
            {
                GUI.Label(selectionRect, ColorText("(Not Managed)", Color.red), _justifyRightLabel);
                return;
            }

            if (entry.loadMethod == AmsMultiSceneSetup.LoadMethod.Additive ||
                entry.loadMethod == AmsMultiSceneSetup.LoadMethod.AdditiveAsync)
            {
                EditorBuildSettingsScene buildEntry =
                    EditorBuildSettings.scenes.FirstOrDefault(x => x.path == scene.path);
                if (buildEntry == null || !buildEntry.enabled)
                {
                    // Draw this next to the drop-down.
                    Rect textRect = new(selectionRect);
                    textRect.xMax -= 100.0f;
                    GUI.Label(textRect, ColorText("Not in Build", Color.red), _justifyRightLabel);
                }
            }

            EditorGUI.BeginChangeCheck();
            selectionRect.xMin = selectionRect.xMax - 100.0f;
            GUIUtility.GetControlID(FocusType.Passive); // This needs to happen in Unity 2018.x
            entry.loadMethod = (AmsMultiSceneSetup.LoadMethod)EditorGUI.EnumPopup(selectionRect, entry.loadMethod);
            if (EditorGUI.EndChangeCheck())
            {
                EditorSceneManager.MarkSceneDirty(sceneSetup.gameObject.scene);
            }
        }

        /// <summary>
        ///     Return the Scene that belongs to a particular Handle (which manifests as a HashCode)
        /// </summary>
        /// <param name="handleID">The handle to search for</param>
        /// <returns>The Scene from the SceneManager</returns>
        static Scene GetSceneFromHandleID(int handleID)
        {
            int numScenes = SceneManager.sceneCount;
            for (int i = 0; i < numScenes; ++i)
            {
                Scene scene = SceneManager.GetSceneAt(i);
                if (scene.GetHashCode() == handleID)
                {
                    return scene;
                }
            }

            return new Scene();
        }

        /**
         * Helper function to color text using RTF format
         */
        static string ColorText(string text, Color32 color)
        {
            return string.Format("<color=#{1:X2}{2:X2}{3:X2}{4:X2}>{0}</color>", text, color.r, color.g, color.b,
                color.a);
        }
    }
}