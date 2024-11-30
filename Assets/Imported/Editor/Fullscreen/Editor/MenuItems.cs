#region

using System;
using System.Collections.Generic;
using System.Linq;
using FullscreenEditor.Linux;
using FullscreenEditor.Windows;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

#endregion

namespace FullscreenEditor
{
    static class MenuItems
    {
        [MenuItem(Shortcut.TOOLBAR_PATH, true), MenuItem(Shortcut.FULLSCREEN_ON_PLAY_PATH, true)]
        static bool SetCheckMarks()
        {
            Menu.SetChecked(Shortcut.TOOLBAR_PATH, FullscreenPreferences.ToolbarVisible);
            Menu.SetChecked(Shortcut.FULLSCREEN_ON_PLAY_PATH, FullscreenPreferences.FullscreenOnPlayEnabled);
            return true;
        }

        [MenuItem(Shortcut.TOOLBAR_PATH, false, 0)]
        static void Toolbar()
        {
            FullscreenPreferences.ToolbarVisible.Value = !FullscreenPreferences.ToolbarVisible;
        }

        [MenuItem(Shortcut.FULLSCREEN_ON_PLAY_PATH, false, 0)]
        static void FullscreenOnPlay()
        {
            FullscreenPreferences.FullscreenOnPlayEnabled.Value = !FullscreenPreferences.FullscreenOnPlayEnabled;
        }

        [MenuItem(Shortcut.CURRENT_VIEW_PATH, false, 100)]
        static void CVMenuItem()
        {
            ScriptableObject focusedView = FullscreenUtility.IsLinux
                ? EditorWindow.focusedWindow
                : // Linux does not support View fullscreen, only EditorWindow
                FullscreenUtility.GetFocusedViewOrWindow();

            if (!focusedView || focusedView is PlaceholderWindow)
            {
                return;
            }

            if (focusedView is EditorWindow)
            {
                Fullscreen.ToggleFullscreen(focusedView as EditorWindow);
            }
            else
            {
                Fullscreen.ToggleFullscreen(focusedView);
            }
        }

        [MenuItem(Shortcut.GAME_VIEW_PATH, false, 100)]
        static void GVMenuItem()
        {
            EditorWindow gameView = FindCandidateForFullscreen(Types.GameView, FullscreenUtility.GetMainGameView());
            Fullscreen.ToggleFullscreen(Types.GameView, gameView);
        }

        [MenuItem(Shortcut.SCENE_VIEW_PATH, false, 100)]
        static void SVMenuItem()
        {
            SceneView sceneView = FindCandidateForFullscreen(SceneView.lastActiveSceneView);
            Fullscreen.ToggleFullscreen(sceneView);
        }

        [MenuItem(Shortcut.MAIN_VIEW_PATH, false, 100)]
        static void MVMenuItem()
        {
            ScriptableObject mainView = FullscreenUtility.GetMainView();

            if (FullscreenUtility.IsLinux)
            {
                if (wmctrl.IsInstalled)
                {
                    wmctrl.ToggleNativeFullscreen(mainView);
                }
                else
                {
                    Logger.Warning(
                        "wmctrl not installed, cannot fullscreen main view. Install it using 'sudo apt-get install wmctrl'");
                }

                return;
            }

            if (!mainView)
            {
                Logger.Error("No Main View found, this should not happen");
                return;
            }

            Fullscreen.ToggleFullscreen(mainView);
        }

        [MenuItem(Shortcut.MOSAIC_PATH, true, 100)]
        static bool MosaicValidate()
        {
            return FullscreenRects.ScreenCount >= 2;
        }

        [MenuItem(Shortcut.MOSAIC_PATH, false, 100)]
        static void MosaicMenuItem()
        {
            FullscreenContainer[] openFullscreens = Fullscreen.GetAllFullscreen();

            if (openFullscreens.Length > 0)
            {
                foreach (FullscreenContainer fs in openFullscreens)
                {
                    fs.Close();
                }

                return;
            }

            List<DisplayInfo> displays = DisplayInfo
                .GetDisplays()
                .Where(d => (d.displayDevice.StateFlags & DisplayDeviceStateFlags.AttachedToDesktop) != 0)
                .ToList();

            for (int i = 0; i < displays.Count && i < 8; i++)
            {
                EditorWindow candidate =
                    FindCandidateForFullscreen(Types.GameView, FullscreenUtility.GetMainGameView());

                if (candidate)
                {
                    candidate = Object.Instantiate(candidate);
                    candidate.Show();
                }

                FullscreenWindow fs = Fullscreen.MakeFullscreen(Types.GameView, candidate, true);
                EditorWindow gameView = fs.ActualViewPyramid.Window;
                int targetDisplay = FullscreenPreferences.MosaicMapping.Value[i];

                fs.Rect = displays[i].DpiCorrectedArea;
                FullscreenUtility.SetGameViewDisplayTarget(gameView, targetDisplay);
            }
        }

        [MenuItem(Shortcut.CLOSE_ALL_FULLSCREEN, false, 250)]
        static void CloseAll()
        {
            foreach (FullscreenContainer fs in Fullscreen.GetAllFullscreen())
            {
                fs.Close();
            }
        }

        [MenuItem(Shortcut.CLOSE_ALL_FULLSCREEN, true, 250)]
        static bool CloseAllValidate()
        {
            return Fullscreen.GetAllFullscreen().Length > 0;
        }

        [MenuItem(Shortcut.PREFERENCES_PATH, false, 1000)]
        static void OpenPreferences()
        {
#if UNITY_2018_3_OR_NEWER
            Type windowType = ReflectionUtility.FindClass("UnityEditor.SettingsWindow");
            windowType.InvokeMethod("Show", SettingsScope.User, "Preferences/Fullscreen Editor");
#else
            var windowType = ReflectionUtility.FindClass("UnityEditor.PreferencesWindow");
            windowType.InvokeMethod("ShowPreferencesWindow");
            After.Frames(3, () =>
            {
                var window = EditorWindow.GetWindow(windowType);
                var sections = window.GetFieldValue<IList>("m_Sections").Cast<object>().ToList();
                var index =
                    sections.FindIndex(section => section.GetFieldValue<GUIContent>("content").text == "Fullscreen");
                window.SetPropertyValue("selectedSectionIndex", index);
            });
#endif
        }

        static T FindCandidateForFullscreen<T>(T mainCandidate = null) where T : EditorWindow
        {
            return FindCandidateForFullscreen(typeof(T), mainCandidate) as T;
        }

        static EditorWindow FindCandidateForFullscreen(Type type, EditorWindow mainCandidate = null)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            if (!type.IsOfType(typeof(EditorWindow)))
            {
                throw new ArgumentException("Invalid type, type must inherit from UnityEditor.EditorWindow", "type");
            }

            if (mainCandidate && !mainCandidate.IsOfType(type))
            {
                throw new ArgumentException("Main candidate type must match the type argument or be null",
                    "mainCandidate");
            }

            // if (mainCandidate && !Fullscreen.GetFullscreenFromView(mainCandidate))
            if (mainCandidate)
            {
                return mainCandidate; // Our candidate is not null and is not fullscreened either
            }

            return Resources // Returns the first window of our type that is not in fullscreen
                .FindObjectsOfTypeAll(type)
                .Cast<EditorWindow>()
                .FirstOrDefault(window => !Fullscreen.GetFullscreenFromView(window));
        }
    }
}