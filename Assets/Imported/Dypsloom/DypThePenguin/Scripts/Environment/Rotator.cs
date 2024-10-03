#region

using UnityEngine;

#endregion

namespace Dypsloom.DypThePenguin.Scripts.Environment
{
    /// <summary>
    ///     Rotator allows you to rotate any game object.
    /// </summary>
    public class Rotator : MonoBehaviour
    {
        [Tooltip("If the target index is -1 the platform will continue moving."), SerializeField]
        protected Vector3 m_RotationSpeed = new(0, 1, 0);

        [Tooltip("The object to rotate."), SerializeField]
        protected Transform m_ObjectTransform;

        /// <summary>
        ///     Cache components.
        /// </summary>
        public void Awake()
        {
            if (m_ObjectTransform == null)
            {
                m_ObjectTransform = transform;
            }
        }

        /// <summary>
        ///     Rotate.
        /// </summary>
        void Update()
        {
            m_ObjectTransform.Rotate(m_RotationSpeed * Time.deltaTime);
        }
    }
}