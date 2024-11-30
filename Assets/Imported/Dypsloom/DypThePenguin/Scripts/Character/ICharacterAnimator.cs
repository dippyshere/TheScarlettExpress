#region

using Dypsloom.DypThePenguin.Scripts.Interactions;

#endregion

namespace Dypsloom.DypThePenguin.Scripts.Character
{
    /// <summary>
    ///     Interface for the character animator.
    /// </summary>
    public interface ICharacterAnimator
    {
        void Tick();
        void ItemAction(int item, int itemAction);
        void EquipWeapon(int item);
        void UnequipWeapon();
        void Die(bool dead);
        void Damaged(Damage.Damage damage);
        void Interact(IInteractable interactable);
    }
}