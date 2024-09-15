#region

using System.Collections.Generic;
using UnityEngine;

#endregion

namespace Dypsloom.DypThePenguin.Scripts.Character
{
    /// <summary>
    ///     Character Mover used to move a character using a character controller.
    /// </summary>
    public class CharacterMover : ICharacterMover
    {
        protected const float c_StartFallGravity = -5f;
        protected const float c_StickyGravity = -0.3f;
        protected const float c_NoVerticalMovementOffset = 0.01f;
        protected readonly Character m_Character;
        protected readonly CharacterController m_CharacterController;
        protected readonly List<IMover> m_ExternalMovers;

        float analyticsTick;
        protected Vector3 m_CharacterInputMovement;
        Vector3 m_GravityMovement;
        protected bool m_IsJumping;
        protected JumpForceMover m_JumpForceMover;

        protected Vector3 m_Movement;
        bool m_WasGrounded;
        Vector3 previousSpeed;

        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="character"></param>
        public CharacterMover(Character character)
        {
            m_Character = character;
            m_CharacterController = m_Character.CharacterController;
            m_ExternalMovers = new List<IMover>();
            m_JumpForceMover = new JumpForceMover();

            m_Character.OnDie += () => { m_ExternalMovers.Clear(); };
        }

        public Character Character
        {
            get { return m_Character; }
        }

        public Vector3 Movement
        {
            get { return m_Movement; }
        }

        public Vector3 CharacterInputMovement
        {
            get { return m_CharacterInputMovement; }
        }

        public bool IsJumping
        {
            get { return m_IsJumping; }
        }

        /// <summary>
        ///     Update tick every frame.
        /// </summary>
        public void Tick()
        {
            if (m_Character.IsGrounded)
            {
                if (m_IsJumping)
                {
                    RemoveExternalMover(m_JumpForceMover);
                    m_IsJumping = false;
                }

                if (m_Character.CharacterInput.Jump)
                {
                    m_Character.IsGrounded = false;
                    m_IsJumping = true;
                    m_JumpForceMover.StartJump(m_Character.JumpForce, m_Character.JumpFallOff);
                    AddExternalMover(m_JumpForceMover);
                    if (TrainGameAnalytics.instance != null)
                    {
                        TrainGameAnalytics.instance.RecordGameEvent("jump", new Dictionary<string, object>
                        {
                            { "scene", m_Character.gameObject.scene.name },
                            { "position", m_Character.transform.position },
                            { "rotation", m_Character.transform.rotation }
                        });
                    }
                }

                if (m_Movement.y > c_NoVerticalMovementOffset)
                {
                    m_GravityMovement = new Vector3(0, 0f, 0);
                }
                else
                {
                    m_GravityMovement = new Vector3(0, c_StickyGravity, 0);
                }

                m_WasGrounded = true;
            }
            else
            {
                if (m_WasGrounded)
                {
                    m_GravityMovement = new Vector3(0, c_StartFallGravity, 0);
                }

                m_GravityMovement.y -= m_Character.Gravity * Time.deltaTime;
                m_WasGrounded = false;
            }


            Vector3 movementInput = m_Character.IsDead
                ? Vector3.zero
                : new Vector3(m_Character.CharacterInput.Horizontal, 0, m_Character.CharacterInput.Vertical);

            Vector3 movementRelativeCamera = Vector3.zero;

            switch (m_Character.m_MovementMode)
            {
                case MovementMode.Free:
                    movementRelativeCamera = m_Character.CharacterCamera.transform.TransformDirection(movementInput);
                    movementRelativeCamera =
                        new Vector3(movementRelativeCamera.x, 0, movementRelativeCamera.z).normalized;
                    break;
                case MovementMode.RailZ:
                    movementRelativeCamera = new Vector3(movementInput.x, 0, movementInput.z).normalized;
                    break;
                case MovementMode.RailX:
                    movementRelativeCamera = new Vector3(movementInput.z, 0, movementInput.x).normalized;
                    break;
                case MovementMode.Decorating:
                    movementRelativeCamera = new Vector3(0, 0, 0);
                    break;
            }

            m_CharacterInputMovement = Vector3.SmoothDamp(m_CharacterInputMovement,
                m_Character.Speed * movementRelativeCamera, ref previousSpeed, 0.1f);

            Vector3 externalMovement = Vector3.zero;
            for (int i = m_ExternalMovers.Count - 1; i >= 0; i--)
            {
                externalMovement += m_ExternalMovers[i].Movement;
                m_ExternalMovers[i].Tick();
            }

            m_Movement = m_CharacterInputMovement + externalMovement + m_GravityMovement;

            if (m_CharacterController.enabled)
            {
                m_CharacterController.Move(m_Movement * Time.deltaTime);
            }

            if (analyticsTick >= 0.1f)
            {
                if (TrainGameAnalytics.instance != null)
                {
                    TrainGameAnalytics.instance.RecordGameEvent("character_movement", new Dictionary<string, object>
                    {
                        { "scene", m_Character.gameObject.scene.name },
                        { "position", m_Character.transform.position }
                    });
                }

                analyticsTick = 0;
            }

            analyticsTick += Time.deltaTime;
        }

        /// <summary>
        ///     Add an external mover.
        /// </summary>
        /// <param name="mover">The external mover to add.</param>
        public void AddExternalMover(IMover mover)
        {
            if (mover == null || m_ExternalMovers.Contains(mover))
            {
                return;
            }

            mover.SetParentMover(this);
            m_ExternalMovers.Add(mover);
        }

        /// <summary>
        ///     Remove an external mover.
        /// </summary>
        /// <param name="mover">The external mover to remove.</param>
        public void RemoveExternalMover(IMover mover)
        {
            if (mover == null || !m_ExternalMovers.Contains(mover))
            {
                return;
            }

            mover.SetParentMover(null);
            m_ExternalMovers.Remove(mover);
        }

        /// <summary>
        ///     Do not do anything.
        /// </summary>
        /// <param name="parent">The parent mover.</param>
        public void SetParentMover(IParentMover parent)
        {
            //Nothing
        }
    }
}