namespace Dypsloom.DypThePenguin.Scripts.Interactions
{
    /// <summary>
    ///     The interactor allows you to interact with interactables.
    /// </summary>
    public interface IInteractor
    {
        void AddInteractable(IInteractable interactable);

        void RemoveInteractable(IInteractable interactable);
    }

    /// <summary>
    ///     The character interactor has a reference to a character.
    /// </summary>
    public interface ICharacterInteractor : IInteractor
    {
        Character.Character Character { get; }
    }
}