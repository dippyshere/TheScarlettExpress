#region

using System;
using System.Collections.Generic;
using UnityEngine;

#endregion

namespace Dypsloom.DypThePenguin.Scripts.Items
{
    /// <summary>
    ///     The inventory component.
    /// </summary>
    public class Inventory : MonoBehaviour
    {
        [Tooltip("The max number of stacks."), SerializeField]
        
        protected int m_MaxSize = 9;

        [Tooltip("The max size per stack."), SerializeField]
        
        protected int m_MaxStackSize = 99;

        [Tooltip("The items."), SerializeField]
        
        protected List<ItemAmount> m_Items;

        public IReadOnlyList<ItemAmount> Items
        {
            get { return m_Items; }
        }

        public int MaxSize
        {
            get { return m_MaxSize; }
        }

        public event Action OnInventoryUpdate;

        /// <summary>
        ///     Add an item.
        /// </summary>
        /// <param name="item">The item to add.</param>
        /// <returns></returns>
        public int Add(IItem item, int amount = 1)
        {
            return Add((item, amount));
        }

        /// <summary>
        ///     Add an item
        /// </summary>
        /// <param name="itemAmount"></param>
        /// <returns>The amount added.</returns>
        public int Add(ItemAmount itemAmount)
        {
            IItem item = itemAmount.Item;
            int amount = itemAmount.Amount;

            if (item == null || amount < 1)
            {
                return 0;
            }

            if (item.ItemDefinition.Unique)
            {
                m_Items.Add((item, 1));
                ItemAdded((item, 1));
                return 1;
            }

            int index = GetItemIndex(item);
            if (index == -1)
            {
                m_Items.Add(itemAmount);
                ItemAdded(itemAmount);
                return amount;
            }

            m_Items[index] = (m_Items[index].Item, m_Items[index].Amount + amount);
            ItemAdded((m_Items[index].Item, amount));

            return amount;
        }

        /// <summary>
        ///     Remove an item.
        /// </summary>
        /// <param name="item">The item to remove.</param>
        /// <param name="amount">The amount to remove.</param>
        /// <returns>The amount removed.</returns>
        public int Remove(IItem item, int amount = 1)
        {
            return Remove((amount, item));
        }

        /// <summary>
        ///     Remove an item.
        /// </summary>
        /// <param name="itemAmount">The item amount to remove.</param>
        /// <returns>The amount removed.</returns>
        public int Remove(ItemAmount itemAmount)
        {
            int itemIndex = GetItemIndex(itemAmount.Item);

            if (itemIndex == -1)
            {
                return 0;
            }

            int currentAmount = m_Items[itemIndex].Amount;
            int newAmount = currentAmount - itemAmount.Amount;
            IItem itemRemoved = m_Items[itemIndex].Item;
            int amountRemoved = 0;

            if (newAmount < 1)
            {
                amountRemoved = currentAmount;
                m_Items.RemoveAt(itemIndex);
            }
            else
            {
                amountRemoved = itemAmount.Amount;
                m_Items[itemIndex] = (itemRemoved, newAmount);
            }

            ItemRemoved((itemRemoved, amountRemoved));
            return amountRemoved;
        }

        /// <summary>
        ///     An item was removed.
        /// </summary>
        /// <param name="itemAmount">The item amount removed.</param>
        protected virtual void ItemRemoved(ItemAmount itemAmount)
        {
            UpdateInventory();
        }

        /// <summary>
        ///     An item was added.
        /// </summary>
        /// <param name="itemAmount">The item amount added.</param>
        protected virtual void ItemAdded(ItemAmount itemAmount)
        {
            UpdateInventory();
        }

        /// <summary>
        ///     Send an event that the inventory was changed.
        /// </summary>
        protected void UpdateInventory()
        {
            OnInventoryUpdate?.Invoke();
        }

        /// <summary>
        ///     Get the item index.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>The index where the amount</returns>
        public int GetItemIndex(IItem item, bool exact = false)
        {
            if (item == null)
            {
                return -1;
            }

            for (int i = 0; i < m_Items.Count; i++)
            {
                if (m_Items[i].Item == null)
                {
                    continue;
                }

                if (item.ItemDefinition.Unique || exact)
                {
                    if (m_Items[i].Item == item)
                    {
                        return i;
                    }
                }
                else
                {
                    if (m_Items[i].Item.ItemDefinition == item.ItemDefinition)
                    {
                        return i;
                    }
                }
            }

            return -1;
        }

        /// <summary>
        ///     Get the item index.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>The index where the amount</returns>
        public int GetItemIndex(ItemDefinition itemDefinition)
        {
            for (int i = 0; i < m_Items.Count; i++)
            {
                if (m_Items[i].Item == null)
                {
                    continue;
                }

                if (m_Items[i].Item.ItemDefinition == itemDefinition)
                {
                    return i;
                }
            }

            return -1;
        }
    }
}