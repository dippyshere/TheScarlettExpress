#region

using System;
using UnityEngine;
using UnityEngine.SceneManagement;

#endregion

namespace Dypsloom.DypThePenguin.Scripts.Game
{
    /// <summary>
    ///     The game manager.
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        [SerializeField] protected GameObject m_PauseMenu;

        protected int m_EnemyKillCount;
        protected bool m_IsPaused;

        public int EnemyKillCount
        {
            get { return m_EnemyKillCount; }
        }

        /// <summary>
        ///     Initialize component.
        /// </summary>
        void Awake()
        {
            m_IsPaused = false;
            m_PauseMenu?.SetActive(false);
        }

        public event Action<int> EnemyDiedE;

        /// <summary>
        ///     An enemy died.
        /// </summary>
        public void EnemyDied()
        {
            m_EnemyKillCount += 1;
            EnemyDiedE?.Invoke(m_EnemyKillCount);
        }

        /// <summary>
        ///     toggle Pause.
        /// </summary>
        public void TogglePause()
        {
            if (m_IsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

        /// <summary>
        ///     Pause game.
        /// </summary>
        public void Pause()
        {
            Time.timeScale = 0;
            m_PauseMenu.SetActive(true);
            m_IsPaused = true;
        }

        /// <summary>
        ///     Resume game.
        /// </summary>
        public void Resume()
        {
            Time.timeScale = 1;
            m_PauseMenu.SetActive(false);
            m_IsPaused = false;
        }

        /// <summary>
        ///     Quit game.
        /// </summary>
        public void Quit()
        {
            Application.Quit();
        }

        #region Singleton Setup

        static GameManager s_Instance;

        public static GameManager Instance
        {
            get
            {
                if (s_Instance != null)
                {
                    return s_Instance;
                }

                s_Instance = FindObjectOfType<GameManager>();
                if (s_Instance == null)
                {
                    s_Instance = new GameObject("Game Manager").AddComponent<GameManager>();
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