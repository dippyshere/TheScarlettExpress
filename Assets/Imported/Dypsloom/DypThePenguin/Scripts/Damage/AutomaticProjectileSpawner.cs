#region

using System.Collections;
using UnityEngine;

#endregion

namespace Dypsloom.DypThePenguin.Scripts.Damage
{
    /// <summary>
    ///     A component that spawns projectile repetitively.
    /// </summary>
    public class AutomaticProjectileSpawner : ProjectileSpawner, IAutoAttack
    {
        [Tooltip("Start auto firing at the start of the game."), SerializeField]
        
        bool m_AutoFireOnStart = true;

        [Tooltip("The time elapsed between each shot."), SerializeField]
        
        float m_FirePeriod = 1;

        protected Coroutine m_FireCoroutine;
        protected WaitForSeconds m_WaitPeriod;

        /// <summary>
        ///     Start auto firing.
        /// </summary>
        void Start()
        {
            m_WaitPeriod = new WaitForSeconds(m_FirePeriod);
            if (m_AutoFireOnStart)
            {
                StartAutoAttack();
            }
        }

        /// <summary>
        ///     Stop attacking on disable
        /// </summary>
        void OnDisable()
        {
            IsAttacking = false;
        }

        public bool IsAttacking { get; private set; }

        /// <summary>
        ///     Start the coroutine.
        /// </summary>
        public void StartAutoAttack()
        {
            if (m_FireCoroutine != null)
            {
                return;
            }

            IsAttacking = true;

            m_FireCoroutine = StartCoroutine(AutomaticFire());
        }

        /// <summary>
        ///     Stop the coroutine.
        /// </summary>
        public void StopAutoAttack()
        {
            StopCoroutine(m_FireCoroutine);
            m_FireCoroutine = null;
            IsAttacking = false;
        }

        /// <summary>
        ///     The coroutine.
        /// </summary>
        /// <returns>The IEnumerator.</returns>
        IEnumerator AutomaticFire()
        {
            while (true)
            {
                yield return m_WaitPeriod;
                Shoot();
            }
        }
    }
}