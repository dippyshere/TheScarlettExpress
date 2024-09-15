#region

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

#endregion

namespace FullscreenEditor
{
    static class KeepFullscreenBelow
    {
        [InitializeOnLoadMethod]
        static void InitPatch()
        {
            Type eApp = typeof(EditorApplication);
            EditorApplication.CallbackFunction callback =
                eApp.GetFieldValue<EditorApplication.CallbackFunction>("windowsReordered");
            callback += () => BringWindowsAbove();
            eApp.SetFieldValue("windowsReordered", callback);
            FullscreenCallbacks.afterFullscreenOpen += f => BringWindowsAbove();
        }

        // https://github.com/mukaschultze/fullscreen-editor/issues/54
        // This is needed because ContainerWindows created by ShowAsDropDown are not
        // returned by 'windows' property
        public static IEnumerable<ScriptableObject> GetAllContainerWindowsOrdered()
        {
            IEnumerable<ScriptableObject> ordered = Types.ContainerWindow
                .GetPropertyValue<ScriptableObject[]>("windows")
                .Reverse();

            IEnumerable<ScriptableObject> missing = Resources
                .FindObjectsOfTypeAll(Types.ContainerWindow)
                .Select(cw => cw as ScriptableObject);

            return ordered
                .Concat(missing)
                .Distinct();
        }

        public static void BringWindowsAbove()
        {
            if (!FullscreenPreferences.KeepFullscreenBelow)
            {
                return;
            }

            FullscreenContainer[] fullscreens = Fullscreen.GetAllFullscreen();
            if (fullscreens.Length == 0)
            {
                return;
            }

            string methodName = "Internal_BringLiveAfterCreation";
            IEnumerable<ScriptableObject> windows = GetAllContainerWindowsOrdered()
                .Where(w => !Fullscreen.GetFullscreenFromView(w))
                .Where(w =>
                {
                    if (w.GetPropertyValue<int>("showMode") == (int)ShowMode.MainWindow)
                    {
                        return false; // Main Window should be kept below everything
                    }

                    if (fullscreens.FirstOrDefault(f => f.m_src.Container == w))
                    {
                        return false; // Keep other fullscreen containers below
                    }

                    return true;
                });

            foreach (ScriptableObject w in windows)
            {
                if (w.HasMethod(methodName, new[] { typeof(bool), typeof(bool), typeof(bool) }))
                {
                    w.InvokeMethod(methodName, true, false, false);
                }
                else
                {
                    w.InvokeMethod(methodName, true, false);
                }
            }
        }
    }
}