#region

using Dypsloom.Shared.Utility;
using UnityEngine;

#endregion

namespace Dypsloom.DypThePenguin.Scripts.Game
{
    /// <summary>
    ///     Return to the pool after a certain delay.
    /// </summary>
    public class ReturnToPoolDelay : MonoBehaviour
    {
        [SerializeField] protected float m_TimeAfterAwake = -1;
        [SerializeField] protected GameObject m_Target;

        /// <summary>
        ///     Cache components.
        /// </summary>
        protected void Awake()
        {
            if (m_TimeAfterAwake <= 0)
            {
                return;
            }

            ReturnToPoolDelayed(m_TimeAfterAwake);
        }

        /// <summary>
        ///     Return the the pool after delay seconds.
        /// </summary>
        /// <param name="delay">The delay.</param>
        public void ReturnToPoolDelayed(float delay)
        {
            SchedulerManager.Schedule(() => PoolManager.Destroy(m_Target), delay);
        }
    }
}