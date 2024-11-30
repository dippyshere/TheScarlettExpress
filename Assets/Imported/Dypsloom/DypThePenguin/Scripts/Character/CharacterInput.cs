#region

using Dypsloom.DypThePenguin.Scripts.Items;
using UnityEngine;

#endregion

namespace Dypsloom.DypThePenguin.Scripts.Character
{
    /// <summary>
    ///     The character Input.
    /// </summary>
    public class CharacterInput : ICharacterInput
    {
        protected Character m_Character;

        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="character"></param>
        public CharacterInput(Character character)
        {
            m_Character = character;
        }

        public float Horizontal
        {
            get { return Input.GetAxisRaw("Horizontal"); }
        }

        public float Vertical
        {
            get { return Input.GetAxisRaw("Vertical"); }
        }

        public bool Jump
        {
            get { return Input.GetButtonDown("Jump"); }
        }
        
        public bool Sprint
        {
            get { return Input.GetKey(KeyCode.LeftShift); }
        }

        public bool Interact
        {
            get { return Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("Fire2"); }
        }

        /// <summary>
        ///     The input to use an item action.
        /// </summary>
        /// <param name="usableItemObject">The usable ItemObject.</param>
        /// <param name="actionIndex">The action index.</param>
        /// <returns>True if the input is valid.</returns>
        public bool UseEquippedItemInput(IUsableItem usableItemObject, int actionIndex)
        {
            if (usableItemObject == null || usableItemObject.Item == null)
            {
                return false;
            }

            if (actionIndex == 0)
            {
                return Input.GetButtonDown("Fire1");
            }

            return true;
        }

        /// <summary>
        ///     Use the item hot bar button.
        /// </summary>
        /// <param name="slotIndex">The hot bar index</param>
        /// <returns>True if the item should be used.</returns>
        public bool UseItemHotbarInput(int slotIndex)
        {
            return !Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Alpha1 + slotIndex);
        }

        /// <summary>
        ///     Drop the item in the hot bar slot specified.
        /// </summary>
        /// <param name="slotIndex">The slot index.</param>
        /// <returns>True if the item should be dropped.</returns>
        public bool DropItemHotbarInput(int slotIndex)
        {
            return Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Alpha1 + slotIndex);
        }
    }
}