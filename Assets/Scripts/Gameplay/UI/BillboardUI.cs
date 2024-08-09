using System;
using UnityEngine;

public class BillboardUI : MonoBehaviour
{
    [SerializeField] private Camera _camera;

    private void Start()
    {
        if (_camera == null)
        {
            _camera = Camera.main;
        }
    }

    private void Update()
    {
        transform.LookAt(transform.position + _camera.transform.rotation * Vector3.forward, _camera.transform.rotation * Vector3.up);
    }
}
