#region

using UnityEngine;

#endregion

namespace Dypsloom.DypThePenguin.Scripts.Environment
{
    /// <summary>
    ///     Ping Pong Translate.
    /// </summary>
    public class PingPongTranslate : MonoBehaviour
    {
        [Tooltip("Offset translation."), SerializeField]
        protected Vector3 m_Offset = new(0, 1, 0);

        [Tooltip("The object to translate."), SerializeField]
        protected Transform m_ObjectTransform;

        protected Vector3 m_StarPosition;

        /// <summary>
        ///     Cache components.
        /// </summary>
        public void Awake()
        {
            if (m_ObjectTransform == null)
            {
                m_ObjectTransform = transform;
            }

            m_StarPosition = m_ObjectTransform.localPosition;
        }

        /// <summary>
        ///     Update position.
        /// </summary>
        void Update()
        {
            m_ObjectTransform.localPosition = m_StarPosition + m_Offset * Mathf.Abs(Mathf.Sin(Time.time));
        }

        /// <summary>
        ///     Go back to the start on disable.
        /// </summary>
        void OnDisable()
        {
            m_ObjectTransform.localPosition = m_StarPosition;
        }
    }
}