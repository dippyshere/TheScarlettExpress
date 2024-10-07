#if UNITY_2017_1_OR_NEWER

#region

using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Playables;

#endregion

namespace CodingJar.MultiScene.CustomResolvers
{
	/// <summary>
	///     Custom resolver for PlayableDirector class which uses an IExposedPropertyTable as opposed to fields
	/// </summary>
	static class AmsPlayableDirectorResolver
    {
#if UNITY_EDITOR
        [InitializeOnLoadMethod, RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
#endif
        static void AddCustomResolver()
        {
            AmsCrossSceneReferenceResolver.AddCustomResolver(HandleCrossSceneReference);
        }

        /// <summary>
        ///     Attempt to handle a cross-scene reference.
        /// </summary>
        static bool HandleCrossSceneReference(RuntimeCrossSceneReference xRef)
        {
            if (!(xRef.fromObject is PlayableDirector))
            {
                return false;
            }

            bool isDirty = false;

            string sourceField = xRef.sourceField;
            if (sourceField.StartsWith("m_SceneBindings"))
            {
                PlayableDirector_SceneBindings(xRef);
                isDirty = true;
            }

            if (sourceField.StartsWith("m_ExposedReferences"))
            {
                PlayableDirector_ExposedReferences(xRef);
                isDirty = true;
            }

            if (isDirty)
            {
                PlayableDirector playableDirector = xRef.fromObject as PlayableDirector;
                if (playableDirector)
                {
#if UNITY_2017_3_OR_NEWER
                    if (playableDirector.state == PlayState.Playing)
                    {
                        AmsDebug.LogWarning(playableDirector,
                            "To prevent issues, delay the PlayableDirector '{0}' until after cross-scene references are loaded. Cross-Scene Reference: {1}",
                            playableDirector, xRef);
                        playableDirector.RebuildGraph();
                    }
#else
	                if (playableDirector.gameObject.activeSelf)
	                {
		                AmsDebug.LogWarning(playableDirector, "Upgrade to Unity 2017.3 for proper Playables support. Hack work-around for 2017.1 and 2017.2: Disable/ReEnable the GameObject");
		                playableDirector.gameObject.SetActive(false);
		                playableDirector.gameObject.SetActive(true);
	                }
#endif
                }
            }

            return isDirty;
        }

        static void PlayableDirector_ExposedReferences(RuntimeCrossSceneReference xRef)
        {
            List<GenericData> data = xRef.data;

            PlayableDirector playableDirector = xRef.fromObject as PlayableDirector;
            for (int i = 0; i < data.Count; i += 2)
            {
                string key = data[i].@string;

                AmsDebug.Log(xRef.fromObject, "Restoring PlayableDirector Exposed Binding {0} = {1}", key,
                    xRef.toObject);
                playableDirector.ClearReferenceValue(key);
                playableDirector.SetReferenceValue(key, xRef.toObject);
            }
        }

        static void PlayableDirector_SceneBindings(RuntimeCrossSceneReference xRef)
        {
            List<GenericData> data = xRef.data;

            PlayableDirector playableDirector = xRef.fromObject as PlayableDirector;
            for (int i = 0; i < data.Count; i += 2)
            {
                Object key = data[i].@object;

                AmsDebug.Log(xRef.fromObject, "Restoring PlayableDirector Scene Binding {0} = {1}", key, xRef.toObject);
                playableDirector.SetGenericBinding(key, xRef.toObject);
            }
        }
    }
}
#endif