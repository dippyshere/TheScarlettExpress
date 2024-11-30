#region

using UnityEditor;
using UnityEngine;

#endregion

namespace Kino
{
    [CanEditMultipleObjects, CustomEditor(typeof(Bloom))]
    public class BloomEditor : Editor
    {
        static readonly GUIContent _textThreshold = new("Threshold (gamma)");
        SerializedProperty _antiFlicker;
        BloomGraphDrawer _graph;
        SerializedProperty _highQuality;
        SerializedProperty _intensity;
        SerializedProperty _radius;
        SerializedProperty _softKnee;

        SerializedProperty _threshold;

        void OnEnable()
        {
            _graph = new BloomGraphDrawer();
            _threshold = serializedObject.FindProperty("_threshold");
            _softKnee = serializedObject.FindProperty("_softKnee");
            _radius = serializedObject.FindProperty("_radius");
            _intensity = serializedObject.FindProperty("_intensity");
            _highQuality = serializedObject.FindProperty("_highQuality");
            _antiFlicker = serializedObject.FindProperty("_antiFlicker");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            if (!serializedObject.isEditingMultipleObjects)
            {
                EditorGUILayout.Space();
                _graph.Prepare((Bloom)target);
                _graph.DrawGraph();
                EditorGUILayout.Space();
            }

            EditorGUILayout.PropertyField(_threshold, _textThreshold);
            EditorGUILayout.PropertyField(_softKnee);
            EditorGUILayout.PropertyField(_intensity);
            EditorGUILayout.PropertyField(_radius);
            EditorGUILayout.PropertyField(_highQuality);
            EditorGUILayout.PropertyField(_antiFlicker);

            serializedObject.ApplyModifiedProperties();
        }
    }
}