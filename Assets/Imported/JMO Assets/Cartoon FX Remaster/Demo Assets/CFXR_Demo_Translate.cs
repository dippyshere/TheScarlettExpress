#region

using UnityEngine;

#endregion

namespace CartoonFX
{
    public class CFXR_Demo_Translate : MonoBehaviour
    {
        public Vector3 direction = new(0, 1, 0);
        public bool randomRotation;


        bool initialized;
        Vector3 initialPosition;

        void Awake()
        {
            if (!initialized)
            {
                initialized = true;
                initialPosition = transform.position;
            }
        }

        void Update()
        {
            transform.Translate(direction * Time.deltaTime);
        }

        void OnEnable()
        {
            transform.position = initialPosition;
            if (randomRotation)
            {
                transform.eulerAngles = Vector3.Lerp(Vector3.zero, Vector3.up * 360, Random.value);
            }
        }
    }
}