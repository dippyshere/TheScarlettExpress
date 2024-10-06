namespace Dypsloom.DypThePenguin.Scripts.Items
{
    public interface IItemUser
    {
        Character.Character Character { get; }
        void TickUse(IUsableItem usableItem);
    }
}