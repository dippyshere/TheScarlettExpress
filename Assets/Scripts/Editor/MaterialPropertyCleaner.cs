#define UPDATING_FLOAT_TO_INT
#if UNITY_EDITOR

#region

using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

#endregion

public class MaterialPropertyCleaner : EditorWindow
{
    const float REMOVE_BUTTON_WIDTH = 60;
    const float TYPE_SPACING = 4;
    const float SCROLLBAR_WIDTH = 15;
    const float MATERIAL_SPACING = 20;

    List<Material> m_selectedMaterials = new();
    SerializedObject[] m_serializedObjects;
    Vector2 scrollPos;
    GUIStyle warningStyle, errorStyle;

    protected virtual void OnEnable()
    {
        GetSelectedMaterials();

        Undo.undoRedoPerformed += OnUndoRedo;
    }

    void OnDisable()
    {
        Undo.undoRedoPerformed -= OnUndoRedo;
    }

    protected virtual void OnGUI()
    {
        if (m_selectedMaterials == null || m_selectedMaterials.Count <= 0)
        {
            EditorGUILayout.LabelField("No Material Selected", new GUIStyle("LargeLabel"));
        }
        else
        {
            EditorGUIUtility.labelWidth = position.width * 0.5f - SCROLLBAR_WIDTH - 2;
            GUIStyle typeLabelStyle = new("LargeLabel");
            errorStyle = new GUIStyle("CN StatusError");
            warningStyle = new GUIStyle("CN StatusWarn");

#if UPDATING_FLOAT_TO_INT
            if (GUILayout.Button("Transfer Floats to Matching Ints"))
            {
                for (int i = 0; i < m_selectedMaterials.Count; i++)
                {
                    Material mat = m_selectedMaterials[i];

                    if (!HasShader(mat))
                    {
                        Debug.LogError("Material " + mat.name + " doesn't have a shader");
                        continue;
                    }

                    SerializedProperty floats = m_serializedObjects[i].FindProperty("m_SavedProperties.m_Floats");
                    SerializedProperty ints = m_serializedObjects[i].FindProperty("m_SavedProperties.m_Ints");

                    if (floats != null && floats.isArray && ints != null && ints.isArray)
                    {
                        for (int intID = 0; intID < ints.arraySize; intID++)
                        {
                            SerializedProperty intProp = ints.GetArrayElementAtIndex(intID);
                            string intPropName = GetName(intProp);
                            if (!ShaderHasProperty(mat, intPropName, PropertyType.Float))
                            {
                                for (int floatID = 0; floatID < floats.arraySize; floatID++)
                                {
                                    SerializedProperty floatProp = floats.GetArrayElementAtIndex(floatID);
                                    if (GetName(floatProp) == intPropName)
                                    {
                                        SerializedProperty intSecond = intProp.FindPropertyRelative("second");
                                        SerializedProperty floatSecond = floatProp.FindPropertyRelative("second");
                                        intSecond.intValue = Mathf.RoundToInt(floatSecond.floatValue);
                                        floats.DeleteArrayElementAtIndex(floatID);
                                        m_serializedObjects[i].ApplyModifiedProperties();

                                        Debug.Log("Transferred Float " + intPropName + " to Int of same name");
                                    }
                                }
                            }
                            else
                            {
                                Debug.LogError // This would happen if you revert from using an int to using a float again
                                (
                                    "Material " + mat.name + " has an Int property " + intPropName +
                                    " whereas the Shader " + mat.shader.name + " has it as a Float property.\n" +
                                    "The Int material property should be cleaned away"
                                );
                            }
                        }
                    }
                }

                GUIUtility.ExitGUI();
            }

            EditorGUILayout.Space();
#endif

            if (GUILayout.Button("Remove Old Texture References"))
            {
                for (int i = 0; i < m_selectedMaterials.Count; i++)
                {
                    RemoveUnusedProperties("m_SavedProperties.m_TexEnvs", i, PropertyType.TexEnv);
                }

                GUIUtility.ExitGUI();
            }

            if (GUILayout.Button("Remove Old Int References"))
            {
                for (int i = 0; i < m_selectedMaterials.Count; i++)
                {
                    RemoveUnusedProperties("m_SavedProperties.m_Ints", i, PropertyType.Int);
                }

                GUIUtility.ExitGUI();
            }

            if (GUILayout.Button("Remove Old Float References"))
            {
                for (int i = 0; i < m_selectedMaterials.Count; i++)
                {
                    RemoveUnusedProperties("m_SavedProperties.m_Floats", i, PropertyType.Float);
                }

                GUIUtility.ExitGUI();
            }

            if (GUILayout.Button("Remove Old Color References"))
            {
                for (int i = 0; i < m_selectedMaterials.Count; i++)
                {
                    RemoveUnusedProperties("m_SavedProperties.m_Colors", i, PropertyType.Color);
                }

                GUIUtility.ExitGUI();
            }

            if (GUILayout.Button("Remove All Old References"))
            {
                for (int i = 0; i < m_selectedMaterials.Count; i++)
                {
                    Material mat = m_selectedMaterials[i];
                    if (HasShader(mat))
                    {
                        RemoveUnusedProperties("m_SavedProperties.m_TexEnvs", i, PropertyType.TexEnv);
                        RemoveUnusedProperties("m_SavedProperties.m_Ints", i, PropertyType.Int);
                        RemoveUnusedProperties("m_SavedProperties.m_Floats", i, PropertyType.Float);
                        RemoveUnusedProperties("m_SavedProperties.m_Colors", i, PropertyType.Color);
                    }
                    else
                    {
                        Debug.LogError("Material " + mat.name + " doesn't have a shader");
                    }
                }

                GUIUtility.ExitGUI();
            }

            GUIStyle scrollBarStyle = new GUIStyle(GUI.skin.verticalScrollbar);
            scrollBarStyle.fixedWidth = SCROLLBAR_WIDTH;
            scrollPos = EditorGUILayout.BeginScrollView(scrollPos, true, true, GUIStyle.none, scrollBarStyle,
                GUI.skin.box);
            EditorGUILayout.BeginVertical();

            for (int i = 0; i < m_selectedMaterials.Count; i++)
            {
                EditorGUILayout.Space(MATERIAL_SPACING);

                Material m_selectedMaterial = m_selectedMaterials[i];

                EditorGUILayout.BeginHorizontal();
                if (GUILayout.Button(m_selectedMaterial.name, GUILayout.Width(EditorGUIUtility.labelWidth)))
                {
                    EditorGUIUtility.PingObject(m_selectedMaterial);
                }

                if (!HasShader(m_selectedMaterial))
                {
                    EditorGUILayout.LabelField("NULL Shader", errorStyle);
                }
                else
                {
                    if (GUILayout.Button(m_selectedMaterial.shader.name,
                            GUILayout.Width(EditorGUIUtility.labelWidth))) //, new GUIStyle("miniButton")
                    {
                        EditorGUIUtility.PingObject(m_selectedMaterial.shader);
                    }
                }

                EditorGUILayout.EndHorizontal();

                m_serializedObjects[i].Update();

                EditorGUI.indentLevel++;
                {
                    EditorGUILayout.Space(TYPE_SPACING);

                    EditorGUILayout.LabelField("Textures", typeLabelStyle);
                    EditorGUI.indentLevel++;
                    ProcessProperties("m_SavedProperties.m_TexEnvs", i, PropertyType.TexEnv);
                    EditorGUI.indentLevel--;

                    EditorGUILayout.Space(TYPE_SPACING);

                    EditorGUILayout.LabelField("Ints", typeLabelStyle);
                    EditorGUI.indentLevel++;
                    ProcessProperties("m_SavedProperties.m_Ints", i, PropertyType.Int);
                    EditorGUI.indentLevel--;

                    EditorGUILayout.Space(TYPE_SPACING);

                    EditorGUILayout.LabelField("Floats", typeLabelStyle);
                    EditorGUI.indentLevel++;
                    ProcessProperties("m_SavedProperties.m_Floats", i, PropertyType.Float);
                    EditorGUI.indentLevel--;

                    EditorGUILayout.Space(TYPE_SPACING);

                    EditorGUILayout.LabelField("Colors", typeLabelStyle);
                    EditorGUI.indentLevel++;
                    ProcessProperties("m_SavedProperties.m_Colors", i, PropertyType.Color);
                    EditorGUI.indentLevel--;
                }
                EditorGUI.indentLevel--;

                EditorGUILayout.Space(MATERIAL_SPACING);
            }

            EditorGUILayout.EndVertical();
            EditorGUILayout.EndScrollView();

            EditorGUIUtility.labelWidth = 0;
        }
    }

    protected virtual void OnProjectChange()
    {
        GetSelectedMaterials();
    }

    protected virtual void OnSelectionChange()
    {
        GetSelectedMaterials();
    }

    [MenuItem("Tools/Material Property Cleaner")]
    static void Init()
    {
        GetWindow<MaterialPropertyCleaner>("Property Cleaner");
    }

    void OnUndoRedo()
    {
        Repaint();
    }

    static bool ShaderHasProperty(Material mat, string name, PropertyType type)
    {
        switch (type)
        {
            case PropertyType.TexEnv:
                return mat.HasTexture(name);
            case PropertyType.Int:
                return mat.HasInteger(name);
            case PropertyType.Float:
                return mat.HasFloat(name);
            case PropertyType.Color:
                return mat.HasColor(name);
        }

        return false;
    }

    static string GetName(SerializedProperty property)
    {
        return property.FindPropertyRelative("first").stringValue; //return property.displayName;
    }

    static bool HasShader(Material mat)
    {
        return mat.shader.name != "Hidden/InternalErrorShader";
    }

    void RemoveUnusedProperties(string path, int i, PropertyType type)
    {
        if (!HasShader(m_selectedMaterials[i]))
        {
            Debug.LogError("Material " + m_selectedMaterials[i].name + " doesn't have a shader");
            return;
        }

        SerializedProperty properties = m_serializedObjects[i].FindProperty(path);
        if (properties != null && properties.isArray)
        {
            for (int j = properties.arraySize - 1; j >= 0; j--)
            {
                string propName = GetName(properties.GetArrayElementAtIndex(j));
                bool exists = ShaderHasProperty(m_selectedMaterials[i], propName, type);

                if (!exists)
                {
                    Debug.Log("Removed " + type + " Property: " + propName);
                    properties.DeleteArrayElementAtIndex(j);
                    m_serializedObjects[i].ApplyModifiedProperties();
                }
            }
        }
    }

    void ProcessProperties(string path, int i, PropertyType type)
    {
        SerializedProperty properties = m_serializedObjects[i].FindProperty(path);
        if (properties != null && properties.isArray)
        {
            for (int j = 0; j < properties.arraySize; j++)
            {
                string propName = GetName(properties.GetArrayElementAtIndex(j));
                bool exists = ShaderHasProperty(m_selectedMaterials[i], propName, type);

                if (!HasShader(m_selectedMaterials[i]))
                {
                    EditorGUILayout.LabelField(propName, "UNKNOWN", errorStyle);
                }
                else if (exists)
                {
                    EditorGUILayout.LabelField(propName, "Exists"); // in Shader
                }
                else
                {
                    EditorGUILayout.BeginHorizontal();
                    float w = EditorGUIUtility.labelWidth * 2 - REMOVE_BUTTON_WIDTH;
                    EditorGUILayout.LabelField(propName, "Old Reference", warningStyle, GUILayout.Width(w));
                    if (GUILayout.Button("Remove", GUILayout.Width(REMOVE_BUTTON_WIDTH)))
                    {
                        properties.DeleteArrayElementAtIndex(j);
                        m_serializedObjects[i].ApplyModifiedProperties();
                        GUIUtility.ExitGUI();
                    }

                    EditorGUILayout.EndHorizontal();
                }
            }
        }
    }

    void GetSelectedMaterials()
    {
        Object[] objects = Selection.objects;

        m_selectedMaterials = new List<Material>();

        for (int i = 0; i < objects.Length; i++)
        {
            Material newMat = objects[i] as Material;
            if (newMat != null)
            {
                m_selectedMaterials.Add(newMat);
            }
        }

        if (m_selectedMaterials != null)
        {
            m_serializedObjects = new SerializedObject[m_selectedMaterials.Count];
            for (int i = 0; i < m_serializedObjects.Length; i++)
            {
                m_serializedObjects[i] = new SerializedObject(m_selectedMaterials[i]);
            }
        }

        Repaint();
    }

    enum PropertyType
    {
        TexEnv,
        Int,
        Float,
        Color
    }
}
#endif