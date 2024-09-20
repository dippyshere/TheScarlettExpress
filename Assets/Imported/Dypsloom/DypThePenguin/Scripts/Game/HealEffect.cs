#region

using Dypsloom.DypThePenguin.Scripts.Damage;
using UnityEngine;

#endregion

namespace Dypsloom.DypThePenguin.Scripts.Game
{
    /// <summary>
    ///     Heal effect when the damageable heals.
    /// </summary>
    public class HealEffect : MonoBehaviour
    {
        static readonly int s_Heal = Animator.StringToHash("Heal");

        [Tooltip("The damageable."), SerializeField]
        protected Damageable m_Damageable;

        [Tooltip("The animator."), SerializeField]
        protected Animator m_Animator;

        [Tooltip("The effect.."), SerializeField]
        protected ParticleSystem m_Effect;

        /// <summary>
        ///     Cache components.
        /// </summary>
        void Awake()
        {
            if (m_Damageable == null)
            {
                m_Damageable = GetComponent<Damageable>();
            }

            if (m_Animator == null)
            {
                m_Animator = GetComponent<Animator>();
            }

            m_Damageable.OnHeal += OnHeal;
        }

        /// <summary>
        ///     Play effects and animation when healing.
        /// </summary>
        /// <param name="healAmount">The heal amount.</param>
        void OnHeal(int healAmount)
        {
            if (m_Animator != null)
            {
                m_Animator.SetTrigger(s_Heal);
            }

            if (m_Effect != null)
            {
                m_Effect.Play();
            }
        }
    }
}