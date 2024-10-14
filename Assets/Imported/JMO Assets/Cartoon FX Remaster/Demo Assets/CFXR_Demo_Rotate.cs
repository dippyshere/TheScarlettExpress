#region

using UnityEngine;

#endregion

namespace CartoonFX
{
    public class CFXR_Demo_Rotate : MonoBehaviour
    {
        public Vector3 axis = new(0, 1, 0);
        public Vector3 center;
        public float speed = 1.0f;

        void Update()
        {
            transform.RotateAround(center, axis, speed * Time.deltaTime);
        }
    }
}