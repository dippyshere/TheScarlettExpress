﻿#region

using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

#endregion

namespace DialogueEditor
{
#if UNITY_EDITOR
    [CustomEditor(typeof(ConversationManager))]
    public class ConversationManagerEditor : Editor
    {
        const string PREVIEW_TEXT = "Placeholder text. This image acts as a preview of the in-game GUI.";
        const float BOX_HEIGHT = 75;
        const float BUFFER = 15;
        const float ICON_SIZE = 50;
        const float OPTION_HEIGHT = 35;
        const float OPTION_BUFFER = 5;
        const float OPTION_TEXT_BUF_Y = 10;
        SerializedProperty AllowMouseInteractionProperty;


        SerializedProperty BackgroundImageProperty;
        SerializedProperty BackgroundImageSlicedProperty;
        SerializedProperty OptionImageProperty;
        SerializedProperty OptionImageSlicedProperty;
        SerializedProperty ScrollTextProperty;
        SerializedProperty ScrollTextSpeedProperty;
        SerializedProperty ChihuahuaImageProperty;
        SerializedProperty EveImageProperty;
        SerializedProperty SterlingImageProperty;
        SerializedProperty BrownImageProperty;
        SerializedProperty GreenImageProperty;
        SerializedProperty PinkImageProperty;
        SerializedProperty RedImageProperty;
        SerializedProperty YellowImageProperty;
        SerializedProperty JosephImageProperty;
        SerializedProperty MatildaImageProperty;
        SerializedProperty MadamFruFruImageProperty;
        SerializedProperty EbonyImageProperty;
        SerializedProperty ChihuahuaVoiceBankProperty;
        SerializedProperty EbonyVoiceBankProperty;
        SerializedProperty EveVoiceBankProperty;
        SerializedProperty JosephVoiceBankProperty;
        SerializedProperty MatildaVoiceBankProperty;

        void OnEnable()
        {
            BackgroundImageProperty = serializedObject.FindProperty("BackgroundImage");
            BackgroundImageSlicedProperty = serializedObject.FindProperty("BackgroundImageSliced");
            OptionImageProperty = serializedObject.FindProperty("OptionImage");
            OptionImageSlicedProperty = serializedObject.FindProperty("OptionImageSliced");
            ScrollTextProperty = serializedObject.FindProperty("ScrollText");
            ScrollTextSpeedProperty = serializedObject.FindProperty("ScrollSpeed");
            AllowMouseInteractionProperty = serializedObject.FindProperty("AllowMouseInteraction");
            ChihuahuaImageProperty = serializedObject.FindProperty("ChihuahuaImage");
            EveImageProperty = serializedObject.FindProperty("EveImage");
            SterlingImageProperty = serializedObject.FindProperty("SterlingImage");
            BrownImageProperty = serializedObject.FindProperty("BrownImage");
            GreenImageProperty = serializedObject.FindProperty("GreenImage");
            PinkImageProperty = serializedObject.FindProperty("PinkImage");
            RedImageProperty = serializedObject.FindProperty("RedImage");
            YellowImageProperty = serializedObject.FindProperty("YellowImage");
            JosephImageProperty = serializedObject.FindProperty("JosephImage");
            MatildaImageProperty = serializedObject.FindProperty("MatildaImage");
            MadamFruFruImageProperty = serializedObject.FindProperty("MadamFruFruImage");
            EbonyImageProperty = serializedObject.FindProperty("EbonyImage");
            ChihuahuaVoiceBankProperty = serializedObject.FindProperty("ChihuahuaVoiceBank");
            EbonyVoiceBankProperty = serializedObject.FindProperty("EbonyVoiceBank");
            EveVoiceBankProperty = serializedObject.FindProperty("EveVoiceBank");
            JosephVoiceBankProperty = serializedObject.FindProperty("JosephVoiceBank");
            MatildaVoiceBankProperty = serializedObject.FindProperty("MatildaVoiceBank");
        }

        public override void OnInspectorGUI()
        {
            // Update the serializedProperty - always do this in the beginning of OnInspectorGUI.
            serializedObject.Update();
            ConversationManager t = (ConversationManager)target;

            RenderPreviewImage(t);

            // Create a gap in EditorGuiLayout for the preview image to be rendered
            EditorGUILayout.BeginVertical();
            GUILayout.Space(BOX_HEIGHT + OPTION_BUFFER + OPTION_HEIGHT);
            EditorGUILayout.EndVertical();

            // Background image
            GUILayout.Label("Dialogue Image Options", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(BackgroundImageProperty);
            EditorGUILayout.PropertyField(BackgroundImageSlicedProperty);
            EditorGUILayout.Space();

            // Option image
            GUILayout.Label("Dialogue Image Options", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(OptionImageProperty);
            EditorGUILayout.PropertyField(OptionImageSlicedProperty);
            EditorGUILayout.Space();

            // Text
            GUILayout.Label("Text options", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(ScrollTextProperty);
            if (t.ScrollText)
            {
                EditorGUILayout.PropertyField(ScrollTextSpeedProperty);
            }

            EditorGUILayout.Space();

            // Interaction options
            GUILayout.Label("Interaction options", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(AllowMouseInteractionProperty);
            
            EditorGUILayout.Space();
            
            // Character images
            GUILayout.Label("Character images", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(ChihuahuaImageProperty);
            EditorGUILayout.PropertyField(EveImageProperty);
            EditorGUILayout.PropertyField(SterlingImageProperty);
            EditorGUILayout.PropertyField(BrownImageProperty);
            EditorGUILayout.PropertyField(GreenImageProperty);
            EditorGUILayout.PropertyField(PinkImageProperty);
            EditorGUILayout.PropertyField(RedImageProperty);
            EditorGUILayout.PropertyField(YellowImageProperty);
            EditorGUILayout.PropertyField(JosephImageProperty);
            EditorGUILayout.PropertyField(MatildaImageProperty);
            EditorGUILayout.PropertyField(MadamFruFruImageProperty);
            EditorGUILayout.PropertyField(EbonyImageProperty);
            
            EditorGUILayout.Space();
            
            // voice bank defaults
            GUILayout.Label("Voice Bank Defaults", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(ChihuahuaVoiceBankProperty);
            EditorGUILayout.PropertyField(EbonyVoiceBankProperty);
            EditorGUILayout.PropertyField(EveVoiceBankProperty);
            EditorGUILayout.PropertyField(JosephVoiceBankProperty);
            EditorGUILayout.PropertyField(MatildaVoiceBankProperty);

            // Apply changes to the serializedProperty - always do this in the end of OnInspectorGUI.
            serializedObject.ApplyModifiedProperties();
        }

        void RenderPreviewImage(ConversationManager t)
        {
            Rect contextRect = EditorGUILayout.GetControlRect();

            // Draw a background box
            Rect boxRect;
            float width, height;
            width = contextRect.width * 0.75f;
            height = BOX_HEIGHT;
            if (t.BackgroundImage == null)
            {
                boxRect = new Rect(contextRect.x + contextRect.width * 0.125f, contextRect.y + 10, width, height);
                EditorGUI.DrawRect(boxRect, Color.black);
            }
            else
            {
                boxRect = new Rect(contextRect.x + contextRect.width * 0.125f, contextRect.y + 10, width, height);

                if (t.BackgroundImageSliced)
                {
                    GUIStyle style = new();
                    RectOffset ro = new();
                    ro.left = (int)t.BackgroundImage.border.w;
                    ro.top = (int)t.BackgroundImage.border.x;
                    ro.right = (int)t.BackgroundImage.border.y;
                    ro.bottom = (int)t.BackgroundImage.border.z;
                    style.border = ro;
                    style.normal.background = t.BackgroundImage.texture;
                    EditorGUI.LabelField(boxRect, "", style);
                }
                else
                {
                    GUI.DrawTexture(boxRect, t.BackgroundImage.texture);
                }
            }


            // Draw icon
            float difference = BOX_HEIGHT - ICON_SIZE;
            Rect iconRect = new(boxRect.x + BUFFER, boxRect.y + difference * 0.5f, ICON_SIZE, ICON_SIZE);
            EditorGUI.DrawRect(iconRect, Color.white);
            Rect tmpt = new(iconRect);
            tmpt.x += 2f;
            tmpt.y += ICON_SIZE * 0.1f;
            EditorGUI.LabelField(tmpt, "<Icon>");

            // Draw text
            float text_x, text_wid;
            text_x = iconRect.x + iconRect.width + difference * 0.5f;
            text_wid = boxRect.x + boxRect.width - difference * 0.5f - text_x;
            Rect textRect = new(text_x, iconRect.y, text_wid, ICON_SIZE);
            GUIStyle textStyle = new();
            textStyle.normal.textColor = Color.white;
            textStyle.wordWrap = true;
            textStyle.clipping = TextClipping.Clip;
            EditorGUI.LabelField(textRect, PREVIEW_TEXT, textStyle);


            // Option (left)
            float option_x, option_wid;
            option_wid = boxRect.width * 0.8f;
            option_x = boxRect.x + boxRect.width * 0.1f;
            Rect optionRect = new(option_x, boxRect.y + boxRect.height + OPTION_BUFFER, option_wid, OPTION_HEIGHT);
            Rect optionTextRect = new(optionRect);
            optionTextRect.x += optionRect.width * 0.4f;
            optionTextRect.y += OPTION_TEXT_BUF_Y;
            if (t.OptionImage == null)
            {
                EditorGUI.DrawRect(optionRect, Color.black);
            }
            else
            {
                if (t.OptionImageSliced)
                {
                    GUIStyle style = new();
                    RectOffset ro = new();
                    ro.left = (int)t.OptionImage.border.w;
                    ro.top = (int)t.OptionImage.border.x;
                    ro.right = (int)t.OptionImage.border.y;
                    ro.bottom = (int)t.OptionImage.border.z;
                    style.border = ro;
                    style.normal.background = t.OptionImage.texture;
                    EditorGUI.LabelField(optionRect, "", style);
                }
                else
                {
                    GUI.DrawTexture(optionRect, t.OptionImage.texture, ScaleMode.StretchToFill);
                }
            }

            EditorGUI.LabelField(optionTextRect, "Option.", textStyle);
        }
    }
#endif
}