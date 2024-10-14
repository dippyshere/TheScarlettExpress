#region

using UnityEngine;

#endregion

namespace Dypsloom.DypThePenguin.Scripts.Damage
{
    /// <summary>
    ///     The damager interface.
    /// </summary>
    public interface IDamager
    {
        GameObject gameObject { get; }
        int DamageTypeIndex { get; }
    }
}