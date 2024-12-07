#region

using UnityEngine;

#endregion

namespace Dypsloom.DypThePenguin.Scripts.Character
{
    /// <summary>
    ///     Interface for the character mover.
    /// </summary>
    public interface ICharacterMover : IParentMover
    {
        Vector3 CharacterInputMovement { get; }
        bool IsJumping { get; }
        Vector3 LastSpeed { get; }
    }

    /// <summary>
    ///     Interface for the parent mover.
    /// </summary>
    public interface IParentMover : IMover
    {
        void AddExternalMover(IMover mover);
        void RemoveExternalMover(IMover mover);
    }

    /// <summary>
    ///     Interface for the mover.
    /// </summary>
    public interface IMover
    {
        Vector3 Movement { get; }
        void Tick();
        void SetParentMover(IParentMover parent);
    }
}