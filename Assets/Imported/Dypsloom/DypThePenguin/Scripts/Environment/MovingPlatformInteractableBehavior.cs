#region

using Dypsloom.DypThePenguin.Scripts.Interactions;
using UnityEngine;

#endregion

namespace Dypsloom.DypThePenguin.Scripts.Environment
{
    /// <summary>
    ///     Moving platform interactable behavior.
    /// </summary>
    public class MovingPlatformInteractableBehavior : InteractableBehavior
    {
        [SerializeField] protected MovingPlatform m_MovingPlatform;

        /// <summary>
        ///     Only allow to interact if th platform is not moving.
        /// </summary>
        void Update()
        {
            if (m_MovingPlatform.IsMoving)
            {
                m_Interactable.SetIsInteractable(false);
            }

            m_Interactable.SetIsInteractable(true);
        }

        /// <summary>
        ///     Interact to move the platform to the next destination.
        /// </summary>
        /// <param name="interactor">The interactor.</param>
        public override void OnInteract(IInteractor interactor)
        {
            m_MovingPlatform.SetTargetIndex(m_MovingPlatform.GetNextIndex());
            base.OnInteract(interactor);
        }
    }
}