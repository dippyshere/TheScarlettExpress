#region

using System.Collections.Generic;
using UnityEditor;
using UnityEngine.SceneManagement;

#endregion

namespace CodingJar.MultiScene.Editor
{
    public static class AmsPlaymodeHandler
    {
        [InitializeOnLoadMethod]
        static void SaveCrossSceneReferencesBeforePlayInEditMode()
        {
#if UNITY_2017_2_OR_NEWER
            EditorApplication.playModeStateChanged += EditorApplication_playModeStateChanged;
#else
	        EditorApplication.playmodeStateChanged += EditorApplication_playModeStateChanged;
#endif
        }

#if UNITY_2017_2_OR_NEWER
        static void EditorApplication_playModeStateChanged(PlayModeStateChange playmodeState)
        {
            bool isExitingEditMode = playmodeState == PlayModeStateChange.ExitingEditMode;
#else
	    private static void EditorApplication_playModeStateChanged()
	    {
		    bool isExitingEditMode = !EditorApplication.isPlaying && EditorApplication.isPlayingOrWillChangePlaymode;
#endif

#if UNITY_5_6_OR_NEWER
            if (EditorUtility.scriptCompilationFailed)
            {
                AmsDebug.Log(null, "Skipping cross-scene references due to compilation errors");
                return;
            }
#endif

            if (isExitingEditMode)
            {
                List<Scene> allScenes = new(SceneManager.sceneCount);
                for (int i = 0; i < SceneManager.sceneCount; ++i)
                {
                    Scene scene = SceneManager.GetSceneAt(i);
                    if (scene.IsValid() && scene.isLoaded)
                    {
                        allScenes.Add(scene);
                    }
                }

                AmsDebug.Log(null, "Handling Cross-Scene Referencing for Playmode");
                AmsSaveProcessor.HandleCrossSceneReferences(allScenes);
            }
        }
    } // class
} // namespace