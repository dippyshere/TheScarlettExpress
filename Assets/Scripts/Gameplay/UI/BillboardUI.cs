#region

using UnityEngine;
using UnityEngine.Serialization;

#endregion

public class BillboardUI : MonoBehaviour
{
    [FormerlySerializedAs("camera"),FormerlySerializedAs("_camera"),SerializeField] Camera cameraToFace;
    [SerializeField] bool faceUp;

    void Start()
    {
        if (cameraToFace == null)
        {
            cameraToFace = Camera.main;
        }
    }

    void Update()
    {
        if (faceUp)
        {
            // rotate to camera, but keep x rotation at 0
            transform.LookAt(transform.position + cameraToFace.transform.rotation * Vector3.forward,
                cameraToFace.transform.rotation * Vector3.up);
            transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
        }
        else
        {
            transform.LookAt(transform.position + cameraToFace.transform.rotation * Vector3.forward,
                cameraToFace.transform.rotation * Vector3.up);
        }
    }
}