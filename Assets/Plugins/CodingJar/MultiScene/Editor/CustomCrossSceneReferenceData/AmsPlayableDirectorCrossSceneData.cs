#if UNITY_2017_1_OR_NEWER

#region

using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine.Playables;

#endregion

namespace CodingJar.MultiScene.Editor
{
	/// <summary>
	///     Custom class that allows us to add data to a PlayableDirector cross-scene reference in order for it to be
	///     restorable at runtime.
	/// </summary>
	static class AmsPlayableDirectorCrossSceneData
    {
        [InitializeOnLoadMethod]
        static void RegisterCustomDataProcessor()
        {
            AmsCrossSceneReferenceProcessor.AddCustomCrossSceneDataProcessor<PlayableDirector>(
                GetCustomCrossSceneReferenceData);
        }

        /// <summary>
        ///     Check a cross-scene reference for data that cannot be saved by simple field look-ups at runtime.
        /// </summary>
        static List<GenericData> GetCustomCrossSceneReferenceData(EditorCrossSceneReference crossRef)
        {
            PlayableDirector playableDirector = crossRef.fromObject as PlayableDirector;
            if (!playableDirector)
            {
                throw new ArgumentException("crossRef.fromObject contained an incompatible class");
            }

            List<GenericData> genericData = new();

            SerializedProperty fromProperty = crossRef.fromProperty;
            string propertyPath = fromProperty.propertyPath;
            SerializedObject serializedObject = fromProperty.serializedObject;

            if (propertyPath.StartsWith("m_SceneBindings") && propertyPath.EndsWith("value"))
            {
                SerializedProperty spElement = serializedObject.FindProperty(
                    fromProperty.propertyPath.Substring(0,
                        fromProperty.propertyPath.Length - fromProperty.name.Length - 1));
                genericData.Add(spElement.FindPropertyRelative("key").objectReferenceValue);
            }
            else if (propertyPath.StartsWith("m_ExposedReferences") && propertyPath.EndsWith("second"))
            {
                SerializedProperty spElement =
                    serializedObject.FindProperty(propertyPath.Substring(0,
                        propertyPath.Length - fromProperty.name.Length - 1));
                genericData.Add(spElement.FindPropertyRelative("first").stringValue);
            }

            return genericData;
        }
    }
}
#endif