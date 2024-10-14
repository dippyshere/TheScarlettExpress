#region

using UnityEngine;

#endregion

namespace Dypsloom.DypThePenguin.Scripts.Damage
{
    /// <summary>
    ///     Type Damageable, only items with the same damage type can hit this damageable.
    /// </summary>
    public class TypeDamageable : Damageable
    {
        [SerializeField] protected int m_DamageTypeIndex = -1;

        /// <summary>
        ///     Take damage.
        /// </summary>
        /// <param name="damage">The damage information.</param>
        public override void TakeDamage(Damage damage)
        {
            if (CanTakDamage == false)
            {
                return;
            }

            if (damage.Damager == null)
            {
                if (m_DamageTypeIndex == -1)
                {
                    base.TakeDamage(damage);
                }

                return;
            }

            if (damage.Damager.DamageTypeIndex != m_DamageTypeIndex)
            {
                return;
            }

            base.TakeDamage(damage);
        }
    }
}