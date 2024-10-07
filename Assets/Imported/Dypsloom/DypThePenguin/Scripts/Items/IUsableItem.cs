namespace Dypsloom.DypThePenguin.Scripts.Items
{
    /// <summary>
    ///     Interface for a usable item object.
    /// </summary>
    public interface IUsableItem
    {
        IItemUseAction[] Actions { get; }
        IItem Item { get; }
    }
}