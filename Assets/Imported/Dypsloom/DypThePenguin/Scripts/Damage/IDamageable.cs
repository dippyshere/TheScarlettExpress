#region

using System;
using UnityEngine;

#endregion

namespace Dypsloom.DypThePenguin.Scripts.Damage
{
    /// <summary>
    ///     The damageable interface.
    /// </summary>
    public interface IDamageable
    {
        GameObject gameObject { get; }

        int MaxHp { get; }
        int CurrentHp { get; }
        event Action OnHpChanged;
        event Action<Damage> OnTakeDamage;
        event Action<int> OnHeal;
        event Action OnDie;

        void TakeDamage(int amount);
        void TakeDamage(Damage damage);

        void Heal(int amount);
        void Die();
    }
}