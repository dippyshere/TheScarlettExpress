using System;
using UnityEngine;

public class BillboardUI : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private bool faceUp = false;

    private void Start()
    {
        if (_camera == null)
        {
            _camera = Camera.main;
        }
    }

    private void Update()
    {
        if (faceUp)
        {
            // rotate to camera, but keep x rotation at 0
            transform.LookAt(transform.position + _camera.transform.rotation * Vector3.forward, _camera.transform.rotation * Vector3.up);
            transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
        }
        else
        {
            transform.LookAt(transform.position + _camera.transform.rotation * Vector3.forward, _camera.transform.rotation * Vector3.up);
        }
    }
}
