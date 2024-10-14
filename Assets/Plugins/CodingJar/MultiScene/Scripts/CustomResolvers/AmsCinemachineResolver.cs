#if UNITY_2018_1_OR_NEWER

#region

using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

#endregion

namespace CodingJar.MultiScene.CustomResolvers
{
	/// <summary>
	///     Custom resolver for Cinemachine in order to ensure the pipelines are correctly built if we've
	///     changed any of its internal variables.
	/// </summary>
	static class AmsCinemachineResolver
    {
        static bool hasCinemachine;

#if UNITY_EDITOR
        [InitializeOnLoadMethod, RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
#endif
        static void AddCustomResolver()
        {
            // Do a conservative check to see if we have Cinemachine in the project.
            hasCinemachine = false;

            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                try
                {
                    foreach (Type type in assembly.GetTypes())
                    {
                        if (type.Namespace == "Cinemachine")
                        {
                            hasCinemachine = true;
                            break;
                        }
                    }
                }
                catch (Exception)
                {
                } // Just skip uncooperative assemblies

                if (hasCinemachine)
                {
                    break;
                }
            }

            if (hasCinemachine)
            {
                AmsCrossSceneReferenceResolver.AddCustomResolver(HandleCrossSceneReference);
            }
        }

        /// <summary>
        ///     Attempt to handle a cross-scene reference.
        /// </summary>
        static bool HandleCrossSceneReference(RuntimeCrossSceneReference xRef)
        {
            MonoBehaviour cinemachineBehaviour = xRef.fromObject as MonoBehaviour;
            if (!cinemachineBehaviour || !cinemachineBehaviour.isActiveAndEnabled)
            {
                return false;
            }

            string nameSpace = cinemachineBehaviour.GetType().Namespace;
            if (string.IsNullOrEmpty(nameSpace) || !nameSpace.StartsWith("Cinemachine"))
            {
                return false;
            }

            AmsDebug.LogWarning(xRef.fromObject,
                "xSceneRef on Cinemachine Behaviour: {0}. Disabling/Enabling to ensure pipeline is up to date.", xRef);
            cinemachineBehaviour.enabled = false;
            cinemachineBehaviour.enabled = true;

            return false;
        }
    }
}
#endif