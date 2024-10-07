#region

using Dypsloom.DypThePenguin.Scripts.Character;
using Dypsloom.DypThePenguin.Scripts.Damage;
using Dypsloom.Shared.Utility;
using UnityEngine;

#endregion

namespace Dypsloom.DypThePenguin.Scripts.Items
{
    /// <summary>
    ///     The throw attack
    /// </summary>
    public class ThrowAttack : ItemActionComponent
    {
        [Tooltip("Cooldown between each throw."), SerializeField]
        protected float m_Cooldown;

        [Tooltip("The projectile prefab."), SerializeField]
        protected GameObject m_ProjectilePrefab;

        /// <summary>
        ///     Use the item object.
        /// </summary>
        /// <param name="item">The item object.</param>
        /// <param name="itemUser">The item user.</param>
        public override void Use(IItem item, IItemUser itemUser)
        {
            m_NextUseTime = Time.time + m_Cooldown;

            itemUser.Character.CharacterAnimator.ItemAction(CharacterAnimator.SnowBallAnimID,
                CharacterAnimator.ThrowSnowballAnimID);

            Transform characterTransform = itemUser.Character.transform;
            Transform projectileSpawnPoint = itemUser.Character.ProjectilesSpawnPoint;

            Vector3 projectileSpawnPosition = projectileSpawnPoint?.position ?? characterTransform.position;

            Quaternion projectileRotation = projectileSpawnPoint?.rotation ?? characterTransform.rotation;

            GameObject projectileObject = PoolManager.Instantiate(m_ProjectilePrefab,
                projectileSpawnPosition, projectileRotation);

            Projectile projectile = projectileObject.GetComponent<Projectile>();
            if (projectile != null)
            {
                projectile.SetItemUser(itemUser);
            }

            itemUser.Character.Inventory.Remove(item);
        }
    }
}