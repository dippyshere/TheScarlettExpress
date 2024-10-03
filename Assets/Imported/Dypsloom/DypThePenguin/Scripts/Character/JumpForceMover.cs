#region

using UnityEngine;

#endregion

namespace Dypsloom.DypThePenguin.Scripts.Character
{
    /// <summary>
    ///     Impulse Force mover used to create knock back effects for example.
    /// </summary>
    public class JumpForceMover : IMover
    {
        protected Character m_Character;
        protected float m_FallOff;
        protected float m_Force;
        protected Vector3 m_Movement;
        protected IParentMover m_Parent;

        public Vector3 Movement
        {
            get { return m_Movement; }
        }

        /// <summary>
        ///     Update every frame.
        /// </summary>
        public virtual void Tick()
        {
            if (m_Movement.y > 0)
            {
                m_Movement = new Vector3(0, m_Movement.y - m_FallOff * Time.deltaTime, 0);
            }
            else if (m_Character.CharacterController.isGrounded)
            {
                m_Movement = Vector3.zero;
            }
        }

        /// <summary>
        ///     Set the parent mover, if it is a character it will use its rigidbody mass as resistance.
        /// </summary>
        /// <param name="parent">The parent mover.</param>
        public virtual void SetParentMover(IParentMover parent)
        {
            m_Parent = parent;
            m_Character = (m_Parent as CharacterMover)?.Character;
        }

        /// <summary>
        ///     The constructor.
        /// </summary>
        /// <param name="force">The force to apply.</param>
        /// <param name="resistance">The base resistance amount, must be above 1.01f.</param>
        public void StartJump(float force, float fallOff = 1.01f)
        {
            m_Force = force * 5f;
            m_FallOff = fallOff;
            m_Movement = new Vector3(0, m_Force, 0);
            ;
        }
    }
}