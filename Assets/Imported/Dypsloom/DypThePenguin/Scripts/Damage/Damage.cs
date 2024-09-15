#region

using System;
using UnityEngine;

#endregion

namespace Dypsloom.DypThePenguin.Scripts.Damage
{
    /// <summary>
    ///     The damage object contains information about the damager, damageable, etc...
    /// </summary>
    [Serializable]
    public struct Damage
    {
        [Tooltip("The damage amount."), SerializeField]
        
        int m_Amount;

        [Tooltip("The force in the the damager hit the damageable."), SerializeField]
        
        Vector3 m_Force;

        [Tooltip("The damageable being hit."), SerializeField]
        
        IDamageable m_Damageable;

        [Tooltip("The damager hitting the damageable."), SerializeField]
        
        IDamager m_Damager;

        /// <summary>
        ///     The damage constructor.
        /// </summary>
        /// <param name="amount">The damage amount.</param>
        /// <param name="force">The damage force.</param>
        /// <param name="damageable">The object receiving the damage.</param>
        /// <param name="damager">The object dealing the damage.</param>
        public Damage(int amount, Vector3 force, IDamageable damageable, IDamager damager)
        {
            m_Amount = amount;
            m_Force = force;
            m_Damageable = damageable;
            m_Damager = damager;
        }

        /// <summary>
        ///     The damage constructor.
        /// </summary>
        /// <param name="amount">The damage amount.</param>
        /// <param name="force">The damage force.</param>
        public Damage(int amount, Vector3 force)
        {
            m_Amount = amount;
            m_Force = force;
            m_Damageable = null;
            m_Damager = null;
        }

        /// <summary>
        ///     The damage constructor.
        /// </summary>
        /// <param name="amount">The damage amount.</param>
        /// <param name="force">The damage force.</param>
        /// <param name="damager">The object dealing the damage.</param>
        public Damage(int amount, Vector3 force, IDamager damager)
        {
            m_Amount = amount;
            m_Force = force;
            m_Damageable = null;
            m_Damager = damager;
        }

        /// <summary>
        ///     Copy the damage information and change the damage amount.
        /// </summary>
        /// <param name="amount">The damage amount.</param>
        /// <param name="damage">The damage information.</param>
        public Damage(int amount, Damage damage)
        {
            m_Amount = amount;
            m_Force = damage.Force;
            m_Damageable = damage.Damageable;
            m_Damager = damage.Damager;
        }

        public IDamager Damager
        {
            get { return m_Damager; }
        }

        public IDamageable Damageable
        {
            get { return m_Damageable; }
        }

        public Vector3 Force
        {
            get { return m_Force; }
        }

        public int Amount
        {
            get { return m_Amount; }
        }

        public static implicit operator Damage((int, Vector3) x)
        {
            return new Damage(x.Item1, x.Item2);
        }

        public static implicit operator Damage((int, Vector3, IDamager) x)
        {
            return new Damage(x.Item1, x.Item2, x.Item3);
        }

        public static implicit operator Damage((int, Damage) x)
        {
            return new Damage(x.Item1, x.Item2);
        }
    }
}