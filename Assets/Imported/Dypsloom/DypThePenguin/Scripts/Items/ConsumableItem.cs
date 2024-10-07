#region

using Dypsloom.DypThePenguin.Scripts.Damage;
using UnityEngine;

#endregion

namespace Dypsloom.DypThePenguin.Scripts.Items
{
    /// <summary>
    ///     A consumable item.
    /// </summary>
    public class ConsumableItem : Item
    {
        [Tooltip("The amount of health to restore."), SerializeField]
        protected int m_HealthRestore = 1;

        /// <summary>
        ///     Use the item.
        /// </summary>
        /// <param name="itemInventory">The inventory where the item resides.</param>
        public override void Use(Inventory itemInventory)
        {
            base.Use(itemInventory);

            if (!(itemInventory is CharacterInventory characterInventory))
            {
                return;
            }

            IDamageable characterDamageable = characterInventory.Character.CharacterDamageable;
            if (characterDamageable.CurrentHp == characterDamageable.MaxHp)
            {
                return;
            }

            if (characterInventory.Remove(this) == 1)
            {
                characterDamageable.Heal(m_HealthRestore);
            }
        }
    }
}