#region

using System.Collections;
using Dypsloom.DypThePenguin.Scripts.Character;
using Dypsloom.DypThePenguin.Scripts.Damage;
using UnityEngine;

#endregion

namespace Dypsloom.DypThePenguin.Scripts.Items
{
    /// <summary>
    ///     Swing attrack.
    /// </summary>
    public class SwingAttack : ItemActionComponent, IDamager
    {
        [Tooltip("The cooldown between each hits."), SerializeField]
        protected float m_Cooldown;

        [Tooltip("The damage dealt by the attack."), SerializeField]
        protected int m_Damage;

        [Tooltip(
             "The knock back dealt to the object being hit. (KnockBack works on RigidBodies or Damageables with Movers)."),
         SerializeField]
        protected int m_KnockBack = 10;

        [Tooltip("The delay before the attack starts taking effect."), SerializeField]
        protected float m_HitStartTime = 0.1f;

        [Tooltip("The dealy where the attack stops taking effect."), SerializeField]
        protected float m_HitEndTime = 0.6f;

        [Tooltip("The damage type index to differentiate it with from other attack types."), SerializeField]
        protected int m_DamageTypeIndex = 1;

        protected bool m_IsAttacking;
        protected IItemUser m_ItemUser;

        /// <summary>
        ///     Check if the attack hit something.
        /// </summary>
        /// <param name="other">The other collider.</param>
        void OnTriggerEnter(Collider other)
        {
            if (m_IsAttacking == false)
            {
                return;
            }

            Vector3 hitDirection = (other.transform.position - transform.position).normalized;

            IDamageable damageable = other.attachedRigidbody?.GetComponent<IDamageable>() ??
                                     other.GetComponent<IDamageable>();
            if (damageable == m_ItemUser.Character.CharacterDamageable)
            {
                return;
            }

            if (damageable != null)
            {
                damageable.TakeDamage((m_Damage, m_KnockBack * hitDirection, this));
            }
            else if (other.attachedRigidbody != null)
            {
                other.attachedRigidbody.AddForce(hitDirection * m_KnockBack, ForceMode.Impulse);
            }
        }

        public int DamageTypeIndex
        {
            get { return m_DamageTypeIndex; }
        }

        /// <summary>
        ///     Use the item object.
        /// </summary>
        /// <param name="item">The item object.</param>
        /// <param name="itemUser">The item user.</param>
        public override void Use(IItem item, IItemUser itemUser)
        {
            m_NextUseTime = Time.time + m_Cooldown;

            m_ItemUser = itemUser;

            m_ItemUser.Character.CharacterAnimator.ItemAction(CharacterAnimator.PickAxeItemAnimID,
                CharacterAnimator.PickAxeSwingAnimID);
            StartCoroutine(AttackIE());
        }

        /// <summary>
        ///     The attack coroutine.
        /// </summary>
        /// <returns>The IEnumerator.</returns>
        protected IEnumerator AttackIE()
        {
            yield return new WaitForSeconds(m_HitStartTime);
            m_IsAttacking = true;
            yield return new WaitForSeconds(m_HitEndTime);
            m_IsAttacking = false;
        }
    }
}