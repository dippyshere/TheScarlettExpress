#region

using UnityEngine;
using UnityEngine.Events;

#endregion

namespace Dypsloom.DypThePenguin.Scripts.Game
{
    /// <summary>
    ///     Trigger an event when X number of enemies are killed.
    /// </summary>
    public class EventOnXEnemyKills : MonoBehaviour
    {
        [Tooltip("The enemy kill count threshold."), SerializeField]
        protected int m_EnemyKillCount;

        [Tooltip("The event to call when the kill count reaches that amount."), SerializeField]
        protected UnityEvent m_Event;

        /// <summary>
        ///     Listen to the event.
        /// </summary>
        void Awake()
        {
            GameManager.Instance.EnemyDiedE += EnemyDied;
        }

        /// <summary>
        ///     Check if the kill count matches and call the event if it does.
        /// </summary>
        /// <param name="killCount"></param>
        void EnemyDied(int killCount)
        {
            if (killCount == m_EnemyKillCount)
            {
                m_Event.Invoke();
            }
        }
    }
}