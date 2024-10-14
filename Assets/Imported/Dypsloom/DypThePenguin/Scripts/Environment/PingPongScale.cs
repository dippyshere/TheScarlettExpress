#region

using UnityEngine;

#endregion

namespace Dypsloom.DypThePenguin.Scripts.Environment
{
    /// <summary>
    ///     Ping Pong Scale.
    /// </summary>
    public class PingPongScale : MonoBehaviour
    {
        [Tooltip("Offset translation."), SerializeField]
        protected Vector3 m_Offset = new(0, 1, 0);

        [Tooltip("Offset translation."), SerializeField]
        protected float m_Speed = 1;

        [Tooltip("The object to translate."), SerializeField]
        protected Transform m_ObjectTransform;

        protected Vector3 m_StarScale;

        /// <summary>
        ///     Cache components.
        /// </summary>
        public void Awake()
        {
            if (m_ObjectTransform == null)
            {
                m_ObjectTransform = transform;
            }

            m_StarScale = m_ObjectTransform.localScale;
        }

        /// <summary>
        ///     Set the scale.
        /// </summary>
        void Update()
        {
            m_ObjectTransform.localScale = m_StarScale + m_Offset * Mathf.Abs(Mathf.Sin(m_Speed * Time.time));
        }

        /// <summary>
        ///     Disable the component.
        /// </summary>
        void OnDisable()
        {
            if (m_ObjectTransform == null)
            {
                return;
            }

            m_ObjectTransform.localScale = m_StarScale;
        }
    }
}