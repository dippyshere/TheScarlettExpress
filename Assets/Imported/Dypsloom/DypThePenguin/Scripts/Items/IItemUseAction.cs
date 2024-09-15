#region

using System;
using UnityEngine;

#endregion

namespace Dypsloom.DypThePenguin.Scripts.Items
{
    /// <summary>
    ///     Item use action.
    /// </summary>
    public interface IItemUseAction
    {
        bool UseInput { get; }
        IItemAction TargetAction { get; }
    }

    /// <summary>
    ///     The item use action
    /// </summary>
    [Serializable]
    public class ItemUseAction : IItemUseAction
    {
        [Tooltip("The keycode to use the action."), SerializeField]
        
        protected KeyCode m_KeyCode;

        [Tooltip("The button to sue the action."), SerializeField]
        
        protected string m_Button;

        [Tooltip("The action component."), SerializeField]
        
        protected ItemActionComponent m_ItemActionComponent;

        public bool UseInput
        {
            get
            {
                return Input.GetKeyDown(m_KeyCode) ||
                       string.IsNullOrWhiteSpace(m_Button)
                    ? false
                    : Input.GetButtonDown(m_Button);
            }
        }

        public IItemAction TargetAction
        {
            get { return m_ItemActionComponent; }
        }
    }
}