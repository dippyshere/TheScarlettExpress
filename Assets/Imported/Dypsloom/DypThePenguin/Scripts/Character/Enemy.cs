#region

using Dypsloom.DypThePenguin.Scripts.Damage;
using Dypsloom.DypThePenguin.Scripts.Game;
using Dypsloom.Shared.Utility;
using UnityEngine;

#endregion

namespace Dypsloom.DypThePenguin.Scripts.Character
{
    public class Enemy : MonoBehaviour
    {
        static readonly int s_DeadAnimHash = Animator.StringToHash("Dead");

        [Tooltip("Look at the player when in view range."), SerializeField]
        protected bool m_LookAtThePlayer = true;

        [Tooltip("The distance at which the enemy will start seeing the character."), SerializeField]
        protected float m_ViewDistance = 15;

        [Tooltip("The distance at which the enemy will start attacking the player."), SerializeField]
        protected float m_AttackDistance = 8;

        [Tooltip("The Animator."), SerializeField]
        protected Animator m_Animator;

        [Tooltip("Death Effect."), SerializeField]
        protected GameObject m_DeathEffects;

        protected IAutoAttack m_AutoAttack;

        protected Damageable m_Damageable;

        protected bool m_Dead;
        protected Character m_PlayerCharacter;
        protected Transform m_PlayerTransform;

        /// <summary>
        ///     Cache the components.
        /// </summary>
        void Awake()
        {
            m_PlayerCharacter = FindObjectOfType<Character>();
            m_PlayerTransform = m_PlayerCharacter?.transform;

            m_Animator = GetComponent<Animator>();

            m_AutoAttack = GetComponent<IAutoAttack>();

            m_Damageable = GetComponent<Damageable>();
            m_Damageable.OnDie += Die;
        }

        /// <summary>
        ///     Check the distance to the character to see if it can attack it.
        /// </summary>
        void Update()
        {
            if (m_Dead)
            {
                return;
            }

            if (m_PlayerTransform == null)
            {
                return;
            }

            float distance = Vector3.Distance(transform.position, m_PlayerTransform.position);

            if (m_AutoAttack != null)
            {
                if (distance <= m_AttackDistance)
                {
                    if (m_AutoAttack.IsAttacking == false)
                    {
                        m_AutoAttack?.StartAutoAttack();
                    }
                }
                else if (m_AutoAttack.IsAttacking)
                {
                    m_AutoAttack?.StopAutoAttack();
                }
            }


            if (m_LookAtThePlayer == false || distance > m_ViewDistance)
            {
                return;
            }

            Vector3 playerXZPosition = new(m_PlayerCharacter.transform.position.x, transform.position.y,
                m_PlayerCharacter.transform.position.z);

            transform.LookAt(playerXZPosition);
        }

        /// <summary>
        ///     Reset the enemy if it was pooled.
        /// </summary>
        void OnEnable()
        {
            m_Dead = false;
            m_Animator.SetBool(s_DeadAnimHash, false);
            m_DeathEffects?.SetActive(false);
        }

        /// <summary>
        ///     Kill the enemy.
        /// </summary>
        void Die()
        {
            if (m_Dead)
            {
                return;
            }

            GameManager.Instance.EnemyDied();
            m_Dead = true;
            m_Animator.SetBool(s_DeadAnimHash, true);
            m_AutoAttack?.StopAutoAttack();

            SchedulerManager.Schedule(() => m_DeathEffects?.SetActive(true), 1.1f);
        }
    }
}