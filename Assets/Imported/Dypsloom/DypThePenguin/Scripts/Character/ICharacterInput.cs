#region

using Dypsloom.DypThePenguin.Scripts.Items;

#endregion

namespace Dypsloom.DypThePenguin.Scripts.Character
{
    /// <summary>
    ///     Interface for the character input.
    /// </summary>
    public interface ICharacterInput : IItemInput
    {
        float Horizontal { get; }
        float Vertical { get; }
        bool Jump { get; }
        bool Interact { get; }
    }
}