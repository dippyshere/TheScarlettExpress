using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dypsloom.DypThePenguin.Scripts.Character;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class SpawnPosition : MonoBehaviour
{
    void Start()
    {
        if (Character.Instance != null)
        {
            Character.Instance.transform.position = transform.position;
            if (CameraManager.Instance != null)
            {
                CameraManager.Instance.ResetCameraPosition();
            }
        }
    }
#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        // icon
        Gizmos.DrawIcon(transform.position + Vector3.up * 1f, "spawn icon", true);
        // capsule
        Gizmos.color = Color.red;
        Vector3 point1 = transform.position + Vector3.up * 0.65f;
        Vector3 point2 = transform.position + Vector3.up * 1.3f;
        float radius = 0.65f;
        Vector3 upOffset = point2 - point1;
        Vector3 up = upOffset.Equals(default) ? Vector3.up : upOffset.normalized;
        Quaternion orientation = Quaternion.FromToRotation(Vector3.up, up);
        Vector3 forward = orientation * Vector3.forward;
        Vector3 right = orientation * Vector3.right;
        Handles.DrawWireArc(point2, forward, right, 180, radius);
        Handles.DrawWireArc(point1, forward, right, -180, radius);
        Handles.DrawLine(point1 + right * radius, point2 + right * radius);
        Handles.DrawLine(point1 - right * radius, point2 - right * radius);
        Handles.DrawWireArc(point2, right, forward, -180, radius);
        Handles.DrawWireArc(point1, right, forward, 180, radius);
        Handles.DrawLine(point1 + forward * radius, point2 + forward * radius);
        Handles.DrawLine(point1 - forward * radius, point2 - forward * radius);
        Handles.DrawWireDisc(point2, up, radius);
        Handles.DrawWireDisc(point1, up, radius);
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position + Vector3.up * 1.3f, transform.forward * 0.5f);
    }
#endif
}
