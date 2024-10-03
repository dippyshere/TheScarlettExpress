#region

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

#endregion

namespace Dypsloom.Shared.Utility
{
    /// <summary>
    ///     Schedule Manager allows you to make delayed calls using coroutines.
    /// </summary>
    public class SchedulerManager : MonoBehaviour
    {
        /// <summary>
        ///     Schedule an action delayed by 'delay' seconds.
        /// </summary>
        /// <param name="action">The action to invoke.</param>
        /// <param name="delay">The delay before the invoke.</param>
        public static void Schedule(Action action, float delay)
        {
            Instance.StartCoroutine(Instance.ScheduleIE(action, delay));
        }

        /// <summary>
        ///     The Coroutine to execute the action.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <param name="delay">The delay before the action is invoked.</param>
        /// <returns></returns>
        public IEnumerator ScheduleIE(Action action, float delay)
        {
            yield return new WaitForSeconds(delay);
            action?.Invoke();
        }

        #region Singleton Setup

        Dictionary<int, Stack<GameObject>> m_GameObjectPool = new();
        Dictionary<int, int> m_InstantiatedGameObjects = new();

        static SchedulerManager s_Instance;

        public static SchedulerManager Instance
        {
            get
            {
                if (s_Instance == null)
                {
                    s_Instance = new GameObject("Schedule Manager").AddComponent<SchedulerManager>();
                }

                return s_Instance;
            }
        }

        /// <summary>
        ///     Set up the static instance.
        /// </summary>
        protected void OnEnable()
        {
            if (s_Instance == null)
            {
                s_Instance = this;
                SceneManager.sceneUnloaded -= SceneUnloaded;
            }
        }

        /// <summary>
        ///     Remove the static instance when unloaded.
        /// </summary>
        void SceneUnloaded(Scene scene)
        {
            s_Instance = null;
            SceneManager.sceneUnloaded -= SceneUnloaded;
        }

        /// <summary>
        ///     Check for scene unload.
        /// </summary>
        void OnDisable()
        {
            SceneManager.sceneUnloaded += SceneUnloaded;
        }

        #endregion
    }
}