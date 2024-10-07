#region

using UnityEngine;

#endregion

namespace Dypsloom.DypThePenguin.Scripts.Items
{
    /// <summary>
    ///     An abstract class used to create actions for item objects.
    /// </summary>
    public abstract class ItemActionComponent : MonoBehaviour, IItemAction
    {
        protected float m_NextUseTime;

        /// <summary>
        ///     Can the item object be used.
        /// </summary>
        public virtual bool CanUse
        {
            get { return Time.time >= m_NextUseTime; }
        }

        /// <summary>
        ///     Use the item object.
        /// </summary>
        /// <param name="item">The item object.</param>
        /// <param name="itemUser">The item user.</param>
        public abstract void Use(IItem item, IItemUser itemUser);
    }
}