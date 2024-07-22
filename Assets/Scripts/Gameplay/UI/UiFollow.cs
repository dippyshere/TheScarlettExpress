using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiFollow : MonoBehaviour
{
    public Transform playerPos;
    public Transform lookAtCam;
    public float smoothing = 5f;
    public Vector3 offset = new Vector3(0f, 10f, 0f);

    private void Update()
    {
        Vector3 targetPosition = playerPos.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing);

        transform.LookAt(lookAtCam);
    }
}
