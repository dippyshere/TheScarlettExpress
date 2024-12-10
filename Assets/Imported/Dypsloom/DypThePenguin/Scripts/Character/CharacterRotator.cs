#region

using UnityEngine;

#endregion

namespace Dypsloom.DypThePenguin.Scripts.Character
{
    /// <summary>
    ///     script used to rotate a character.
    /// </summary>
    public class CharacterRotator
    {
        protected readonly Character m_Character;
        float previousRotation;
        float previousTarget;

        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="character">The character.</param>
        public CharacterRotator(Character character)
        {
            m_Character = character;
        }

        /// <summary>
        ///     Rotate the character in respect to the camera.
        /// </summary>
        public virtual void Tick()
        {
            Vector2 charVelocity = m_Character.IsDead
                ? Vector2.zero
                : new Vector2(m_Character.CharacterInput.Horizontal, m_Character.CharacterInput.Vertical);

            float targetRotation = 0;

            switch (m_Character.m_MovementMode)
            {
                case MovementMode.RailZ:
                case MovementMode.RailX:
                    targetRotation = Mathf.Atan2(charVelocity.x, charVelocity.y) * Mathf.Rad2Deg;
                    break;
                case MovementMode.Free:
                    targetRotation = Mathf.Atan2(charVelocity.x, charVelocity.y) * Mathf.Rad2Deg +
                                     m_Character.CharacterCamera.transform.eulerAngles.y;
                    if (charVelocity.magnitude < 0.1f)
                    {
                        targetRotation = previousTarget;
                    }
                    else
                    {
                        previousTarget = targetRotation;
                    }
                    break;
                default:
                    targetRotation = m_Character.transform.eulerAngles.y;
                    break;
            }

            float rotation = Mathf.SmoothDampAngle(m_Character.transform.eulerAngles.y, targetRotation,
                ref previousRotation, 0.025f);

            Quaternion lookAt = Quaternion.Slerp(m_Character.transform.rotation,
                Quaternion.Euler(0, rotation, 0),
                0.5f);

            m_Character.transform.rotation = lookAt;
        }
    }
}