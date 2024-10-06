namespace Dypsloom.DypThePenguin.Scripts.Damage
{
    /// <summary>
    ///     Interface for an object that auto attacks.
    /// </summary>
    public interface IAutoAttack
    {
        bool IsAttacking { get; }

        void StartAutoAttack();
        void StopAutoAttack();
    }
}