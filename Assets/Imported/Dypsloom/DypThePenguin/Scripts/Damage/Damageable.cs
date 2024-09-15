#region

using System;
using UnityEngine;

#endregion

namespace Dypsloom.DypThePenguin.Scripts.Damage
{
    /// <summary>
    ///     Damageable component, used to take damage, heal and die.
    /// </summary>
    public class Damageable : MonoBehaviour, IDamageable
    {
        [Tooltip("The Max amount of Hp."), SerializeField]
        
        protected int m_MaxHp = 100;

        [Tooltip("The starting HP amount."), SerializeField]
        
        protected int m_CurrentHp = 50;

        [Tooltip("The time in the which the damageable is invincible after getting hit."), SerializeField]
        
        protected float m_InvincibilityTime;

        [Tooltip("Disable the object on death?."), SerializeField]
        
        protected bool m_DisableOnDeath;

        protected double m_LastHitTime;

        public virtual bool CanTakDamage
        {
            get { return m_LastHitTime + m_InvincibilityTime <= Time.timeSinceLevelLoad; }
        }

        /// <summary>
        ///     Initialize.
        /// </summary>
        void Start()
        {
            m_CurrentHp = Mathf.Clamp(m_CurrentHp, 1, m_MaxHp);
            OnHpChanged?.Invoke();
        }

        public event Action OnHpChanged;
        public event Action<Damage> OnTakeDamage;
        public event Action<int> OnHeal;
        public event Action OnDie;

        public virtual int MaxHp
        {
            get { return m_MaxHp; }
        }

        public virtual int CurrentHp
        {
            get { return m_CurrentHp; }
        }

        /// <summary>
        ///     Take Damage.
        /// </summary>
        /// <param name="amount">The amount.</param>
        public void TakeDamage(int amount)
        {
            TakeDamage((amount, Vector3.zero));
        }

        /// <summary>
        ///     Take damage.
        /// </summary>
        /// <param name="damage">The damage information.</param>
        public virtual void TakeDamage(Damage damage)
        {
            if (CanTakDamage == false)
            {
                return;
            }

            int amount = damage.Amount;
            if (amount < 0)
            {
                amount = 0;
            }

            m_CurrentHp = Mathf.Clamp(CurrentHp - amount, 0, MaxHp);

            m_LastHitTime = Time.timeSinceLevelLoad;

            OnTakeDamage?.Invoke((amount, damage));
            OnHpChanged?.Invoke();

            if (m_CurrentHp == 0)
            {
                Die();
            }
        }

        /// <summary>
        ///     Heal amount.
        /// </summary>
        /// <param name="amount">The amount.</param>
        public virtual void Heal(int amount)
        {
            amount = Mathf.Clamp(amount, 0, MaxHp - CurrentHp);
            m_CurrentHp = Mathf.Clamp(CurrentHp + amount, 0, MaxHp);

            OnHeal?.Invoke(amount);
            OnHpChanged?.Invoke();
        }

        /// <summary>
        ///     Die.
        /// </summary>
        public virtual void Die()
        {
            if (m_DisableOnDeath)
            {
                gameObject.SetActive(false);
            }

            OnDie?.Invoke();
        }
    }
}