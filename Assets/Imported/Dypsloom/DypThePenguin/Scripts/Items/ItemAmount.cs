#region

using System;
using UnityEngine;

#endregion

namespace Dypsloom.DypThePenguin.Scripts.Items
{
    /// <summary>
    ///     An item amount.
    /// </summary>
    [Serializable]
    public struct ItemAmount
    {
        [Tooltip("The item definition."), SerializeField]
        
        ItemDefinition m_ItemDefinition;

        [Tooltip("The item."), SerializeField]
        
        Item m_ItemComponent;

        [Tooltip("The amount."), SerializeField]
        
        int m_Amount;

        IItem m_Item;

        public int Amount
        {
            get { return m_Amount; }
        }

        public IItem Item
        {
            get
            {
                if (m_Item != null)
                {
                    return m_Item;
                }

                if (m_ItemComponent != null)
                {
                    m_Item = m_ItemComponent;
                    return m_Item;
                }

                if (m_ItemDefinition == null)
                {
                    return null;
                }

                return m_ItemDefinition.DefaultItem;
            }
        }

        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="amount">The amount.</param>
        public ItemAmount(IItem item, int amount)
        {
            m_Amount = amount;
            m_Item = item;
            m_ItemComponent = item as Item;
            m_ItemDefinition = item?.ItemDefinition;
        }

        public static implicit operator ItemAmount((int, IItem) x)
        {
            return new ItemAmount(x.Item2, x.Item1);
        }

        public static implicit operator ItemAmount((IItem, int) x)
        {
            return new ItemAmount(x.Item1, x.Item2);
        }
    }
}