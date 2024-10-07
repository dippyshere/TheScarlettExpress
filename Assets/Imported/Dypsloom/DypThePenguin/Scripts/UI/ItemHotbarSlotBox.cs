#region

using Dypsloom.DypThePenguin.Scripts.Items;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

#endregion

namespace Dypsloom.DypThePenguin.Scripts.UI
{
    /// <summary>
    ///     A hot bat slot box.
    /// </summary>
    public class ItemHotbarSlotBox : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] protected TextMeshProUGUI m_IndexText;
        [SerializeField] protected Image m_Icon;
        [SerializeField] protected TextMeshProUGUI m_ItemAmountText;
        [SerializeField] protected GameObject m_ItemDescriptionObject;
        [SerializeField] protected TextMeshProUGUI m_ItemDescription;
        protected int m_Index;

        protected ItemAmount m_ItemAmount;

        public ItemAmount ItemAmount
        {
            get { return m_ItemAmount; }
        }

        /// <summary>
        ///     Check for the mouse input.
        /// </summary>
        /// <param name="eventData">The enter event.</param>
        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            if (m_ItemAmount.Item == null)
            {
                m_ItemDescriptionObject?.SetActive(false);
                return;
            }

            m_ItemDescriptionObject?.SetActive(true);
        }

        /// <summary>
        ///     Check for the mouse input.
        /// </summary>
        /// <param name="eventData">The exit event.</param>
        public virtual void OnPointerExit(PointerEventData eventData)
        {
            m_ItemDescriptionObject?.SetActive(false);
        }

        /// <summary>
        ///     Set the index of the slot.
        /// </summary>
        /// <param name="index"></param>
        public virtual void SetIndex(int index)
        {
            m_Index = index;
            m_IndexText.text = (m_Index + 1).ToString();
        }

        /// <summary>
        ///     Draw the item amount.
        /// </summary>
        /// <param name="itemAmount"></param>
        public virtual void Draw(ItemAmount itemAmount)
        {
            if (itemAmount.Item == null || itemAmount.Item.ItemDefinition == null)
            {
                DrawEmpty();
                return;
            }

            m_ItemAmount = itemAmount;
            m_ItemAmountText.text = "x" + itemAmount.Amount;
            m_Icon.gameObject.SetActive(true);
            m_Icon.sprite = itemAmount.Item.ItemDefinition.Icon;
            if (m_ItemDescription != null)
            {
                m_ItemDescription.text = itemAmount.Item.ItemDefinition.Description;
            }
        }

        /// <summary>
        ///     Draw the empty.
        /// </summary>
        public virtual void DrawEmpty()
        {
            m_ItemAmount = (null, 0);
            m_ItemAmountText.text = "";
            m_Icon.gameObject.SetActive(false);
            m_ItemDescriptionObject?.SetActive(false);
            if (m_ItemDescription != null)
            {
                m_ItemDescription.text = "";
            }
        }
    }
}