namespace Dypsloom.DypThePenguin.Scripts.Items
{
    public interface IItem
    {
        ItemDefinition ItemDefinition { get; }
        void Use(Inventory itemInventory);
        void Drop(Inventory itemInventory, int amount);
    }
}