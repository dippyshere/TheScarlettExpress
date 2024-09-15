#region

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

#endregion

namespace FullscreenEditor
{
    [AttributeUsage(AttributeTargets.Field)]
    class DynamicMenuItemAttribute : Attribute
    {
        public DynamicMenuItemAttribute(bool allowNoneValue)
        {
            AllowNoneValue = allowNoneValue;
        }

        public bool AllowNoneValue { get; }
    }

    class Shortcut
    {
        public string GetShortcutString()
        {
            if (KeyCode == 0)
            {
                return "";
            }

            StringBuilder result = new();

            if (!Ctrl && !Shift && !Alt)
            {
                result.Append(NONE_CHAR);
            }
            else
            {
                if (Ctrl)
                {
                    result.Append(CTRL_CHAR);
                }

                if (Shift)
                {
                    result.Append(SHIFT_CHAR);
                }

                if (Alt)
                {
                    result.Append(ALT_CHAR);
                }
            }

            result.Append(keys[KeyCode]);

            return result.ToString();
        }

        #region Fields

        //Always end with an space if the path has no shortcut
        [DynamicMenuItem(true)] public const string TOOLBAR_PATH = "Fullscreen/Show Toolbar _F8";
        [DynamicMenuItem(true)] public const string FULLSCREEN_ON_PLAY_PATH = "Fullscreen/Fullscreen On Play ";
        [DynamicMenuItem(true)] public const string PREFERENCES_PATH = "Fullscreen/Preferences... ";
        [DynamicMenuItem(false)] public const string CURRENT_VIEW_PATH = "Fullscreen/Focused View _F9";
        [DynamicMenuItem(false)] public const string GAME_VIEW_PATH = "Fullscreen/Game View _F10";
        [DynamicMenuItem(false)] public const string SCENE_VIEW_PATH = "Fullscreen/Scene View _F11";
        [DynamicMenuItem(false)] public const string MAIN_VIEW_PATH = "Fullscreen/Main View _F12";
        [DynamicMenuItem(false)] public const string MOSAIC_PATH = "Fullscreen/Mosaic %F10";
        [DynamicMenuItem(true)] public const string CLOSE_ALL_FULLSCREEN = "Fullscreen/Close All %F12";

        const char CTRL_CHAR = '%';
        const char SHIFT_CHAR = '#';
        const char ALT_CHAR = '&';
        const char NONE_CHAR = '_';

        static readonly List<Shortcut> fieldsInfo = new();

        /* fixformat ignore:start */
        static readonly string[] keys =
        {
            "None",
            "F1", "F2", "F3", "F4", "F5", "F6", "F7", "F8", "F9", "F10", "F11", "F12",
            "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M",
            "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z",
            "LEFT", "RIGHT", "UP", "DOWN", "HOME", "END", "PGUP", "PGDN"
        };
        /* fixformat ignore:end */

        static bool changed;

        #endregion

        #region Properties

        public bool Ctrl { get; set; }
        public bool Shift { get; set; }
        public bool Alt { get; set; }
        public int KeyCode { get; set; }

        public bool AllowNoneValue { get; }
        public string FieldName { get; }
        public string BaseString { get; }

        public string Label
        {
            get { return BaseString.Substring(BaseString.LastIndexOf('/') + 1); }
        }

        static bool IsSourceFile
        {
            get { return !string.IsNullOrEmpty(ThisFilePath) && File.Exists(ThisFilePath); }
        }

        static string ThisFilePath
        {
            get
            {
                try
                {
                    return new StackFrame(true).GetFileName();
                }
                catch (Exception e)
                {
                    Logger.Exception(e);
                    return string.Empty;
                }
            }
        }

        #endregion

        #region Constructors

        static Shortcut()
        {
            Type type = typeof(Shortcut);
            FieldInfo[] fields = type.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static |
                                                BindingFlags.Instance);

            if (fields != null)
            {
                foreach (FieldInfo field in fields)
                {
                    object[] att = field.GetCustomAttributes(typeof(DynamicMenuItemAttribute), false);

                    if (att != null)
                    {
                        for (int i = 0; i < att.Length; i++)
                        {
                            fieldsInfo.Add(new Shortcut((DynamicMenuItemAttribute)att[i], field));
                        }
                    }
                }
            }
        }

        public Shortcut(DynamicMenuItemAttribute shortcutAttribute, FieldInfo field)
        {
            FieldName = field.Name;
            AllowNoneValue = shortcutAttribute.AllowNoneValue;

            string constant = (string)field.GetValue(null);
            int lastSpace = constant.LastIndexOf(' ') + 1;

            if (!constant.EndsWith(" "))
            {
                BaseString = constant.Remove(lastSpace);
            }
            else
            {
                BaseString = constant;
                return;
            }

            constant = constant.Substring(lastSpace);

            if (string.IsNullOrEmpty(constant))
            {
                return;
            }

            Ctrl = constant.Contains(CTRL_CHAR);
            Shift = constant.Contains(SHIFT_CHAR);
            Alt = constant.Contains(ALT_CHAR);

            constant = constant.Replace(CTRL_CHAR.ToString(), string.Empty);
            constant = constant.Replace(SHIFT_CHAR.ToString(), string.Empty);
            constant = constant.Replace(ALT_CHAR.ToString(), string.Empty);
            constant = constant.Replace(NONE_CHAR.ToString(), string.Empty);

            KeyCode = Array.IndexOf(keys, constant);

            if (KeyCode < 0 || KeyCode >= keys.Length)
            {
                Logger.Warning("Invalid shortcut term: {0}", constant);
                KeyCode = 0;
            }
        }

        #endregion

        #region Methods

        public override string ToString()
        {
            return BaseString + GetShortcutString();
        }

        public static void DoShortcutsGUI()
        {
            GUI.changed = false;

            using (new EditorGUI.DisabledGroupScope(EditorApplication.isCompiling || !IsSourceFile))
            {
                if (InternalEditorUtility.GetUnityVersion() >= new Version(2019, 1))
                {
                    EditorGUILayout.HelpBox(
                        string.Format("You can set custom shortcuts on a per user basis by editing them under {0} menu",
                            FullscreenUtility.IsMacOS ? "Unity/Shortcuts" : "Edit/Shortcuts"), MessageType.Info);
                }

                foreach (Shortcut field in fieldsInfo)
                {
                    DrawShortcut(field);
                }

                bool duplicated = AnyDuplicates();
                bool invalid = AnyInvalid();

                if (duplicated)
                {
                    EditorGUILayout.HelpBox("Some menu items have the same keystroke, this is not allowed.",
                        MessageType.Error);
                }

                if (invalid)
                {
                    EditorGUILayout.HelpBox(
                        "Some menu items don't have a valid keystroke, you won't be able to use their correspondent fullscreens.",
                        MessageType.Warning);
                }

                using (new EditorGUI.DisabledGroupScope(duplicated || !changed))
                {
                    if (GUILayout.Button("Apply Shortcuts"))
                    {
                        ApplyChanges();
                    }
                }
            }

            if (GUI.changed)
            {
                changed = true;
            }
        }

        static void ApplyChanges()
        {
            if (EditorApplication.isCompiling)
            {
                return;
            }

            AssetDatabase.StartAssetEditing();

            foreach (Shortcut field in fieldsInfo)
            {
                ReplaceConstant(field.FieldName, field);
            }

            AssetDatabase.StopAssetEditing();
            AssetDatabase.Refresh();
        }

        static bool AnyInvalid()
        {
            foreach (Shortcut field in fieldsInfo)
            {
                if (field == null || (!field.AllowNoneValue && field.KeyCode == 0))
                {
                    return true;
                }
            }

            return false;
        }

        static bool AnyDuplicates()
        {
            for (int i = 0; i < fieldsInfo.Count; i++)
            {
                for (int j = i + 1; j < fieldsInfo.Count; j++)
                {
                    Shortcut fieldI = fieldsInfo[i];
                    Shortcut fieldJ = fieldsInfo[j];

                    if (fieldI == null || fieldJ == null ||
                        (fieldI.KeyCode != 0 && fieldI.GetShortcutString() == fieldJ.GetShortcutString()))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        static Shortcut DrawShortcut(Shortcut shortcut)
        {
            using (new EditorGUILayout.HorizontalScope())
            {
                EditorGUILayout.LabelField(shortcut.Label, GUILayout.Width(130f));

                shortcut.Ctrl = GUILayout.Toggle(shortcut.Ctrl, FullscreenUtility.IsMacOS ? "Cmd" : "Ctrl",
                    EditorStyles.miniButtonLeft, GUILayout.Width(50f));
                shortcut.Shift = GUILayout.Toggle(shortcut.Shift, "Shift", EditorStyles.miniButtonMid,
                    GUILayout.Width(50f));
                shortcut.Alt = GUILayout.Toggle(shortcut.Alt, "Alt", EditorStyles.miniButtonRight,
                    GUILayout.Width(50f));
                shortcut.KeyCode = EditorGUILayout.Popup(shortcut.KeyCode, keys);

                if (GUILayout.Button(new GUIContent("X", "Clear Shortcut")))
                {
                    shortcut.Ctrl = false;
                    shortcut.Shift = false;
                    shortcut.Alt = false;
                    shortcut.KeyCode = 0;
                }
            }

            return shortcut;
        }

        static void ReplaceConstant(string constantName, object newValue)
        {
            try
            {
                if (!IsSourceFile)
                {
                    Logger.Error("Could not find the source code file to change value");
                    return;
                }

                StringBuilder fileText = new();
                bool changed = false;

                using (StreamReader file = File.OpenText(ThisFilePath))
                {
                    while (!file.EndOfStream)
                    {
                        string line = file.ReadLine();

                        if (!line.Contains(constantName))
                        {
                            fileText.AppendLine(line);
                            continue;
                        }

                        int indexOfValue = line.IndexOf('=');

                        fileText.Append(line.Remove(indexOfValue));
                        fileText.AppendLine(string.Format("= \"{0}\";", newValue));
                        fileText.Append(file.ReadToEnd());

                        changed = true;
                    }
                }

                fileText = fileText.Replace("\r\n", "\n").Replace("\r", "\n").Replace("\n", Environment.NewLine);

                if (changed)
                {
                    File.WriteAllText(ThisFilePath, fileText.ToString());
                }
                else
                {
                    Logger.Warning("Failed to find field {0} on {1}", constantName, ThisFilePath);
                }
            }
            catch (Exception e)
            {
                Logger.Exception(e);
                Logger.Error("Failed to save Fullscreen Editor shortcuts");
            }
        }

        #endregion
    }
}