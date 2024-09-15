#region

using System;
using UnityEngine;

#endregion

namespace Dypsloom.DypThePenguin.Scripts.Damage
{
    /// <summary>
    ///     A damageable that changes the active gameobject depending on its HP.
    /// </summary>
    public class MultiStateDamageable : Damageable
    {
        [Tooltip("The object states."), SerializeField]
        
        protected State[] m_States;

        /// <summary>
        ///     Set the active object and listen to the damageable events.
        /// </summary>
        void Awake()
        {
            m_States[0].Object.SetActive(true);
            for (int i = 1; i < m_States.Length; i++)
            {
                m_States[i].Object.SetActive(false);
            }

            OnHpChanged += UpdateState;
        }

        /// <summary>
        ///     Update the active object depending on the HP.
        /// </summary>
        void UpdateState()
        {
            for (int j = 0; j < m_States.Length; j++)
            {
                m_States[j].Object.SetActive(false);
            }

            for (int i = 0; i < m_States.Length; i++)
            {
                if (CurrentHp > m_States[i].HPUpperLimit)
                {
                    m_States[i].Object.SetActive(true);
                    break;
                }
            }
        }

        /// <summary>
        ///     Damageable state.
        /// </summary>
        [Serializable]
        public struct State
        {
            public GameObject Object;
            public float HPUpperLimit;
        }
    }
}