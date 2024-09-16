#region

using UnityEngine;

#endregion

namespace Beautify.Universal
{
    public class SphereAnimator : MonoBehaviour
    {
        Rigidbody rb;

        void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        void FixedUpdate()
        {
            switch (transform.position.z)
            {
                case < 2.5f:
                    rb.AddForce(Vector3.forward * 200f * Time.fixedDeltaTime);
                    break;
                case > 8f:
                    rb.AddForce(Vector3.back * 200f * Time.fixedDeltaTime);
                    break;
            }
        }
    }
}