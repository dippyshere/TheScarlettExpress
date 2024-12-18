using System.Collections;
using System.Collections.Generic;
using Dypsloom.DypThePenguin.Scripts.Character;
using UnityEngine;
using Unity.Cinemachine;
using Unity.Cinemachine.TargetTracking;

public class CameraManager : MonoBehaviour
{
    [HideInInspector, Tooltip("Singleton instance of the CameraManager.")]
    public static CameraManager Instance;
    
    [SerializeField, Tooltip("The free look camera.")]
    GameObject freeLookCamera;
    
    [SerializeField, Tooltip("Reference to the cinemachine input manager.")]
    CinemachineInputAxisController cinemachineInputAxisController;
    
    void Awake()
    {
        Instance = this;
    }
    
    public void SetInputModeUI(bool canClipboard = false, bool affectCursor = true)
    {
        if (affectCursor)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        cinemachineInputAxisController.enabled = false;
        if (Character.Instance != null)
            Character.Instance.m_MovementMode = MovementMode.Decorating;
        if (ClipboardManager.Instance != null)
            ClipboardManager.Instance.canClipboard = canClipboard;
    }
    
    public void SetInputModeGameplay(bool affectCursor = true)
    {
        if (affectCursor)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        cinemachineInputAxisController.enabled = true;
        if (Character.Instance != null)
            Character.Instance.m_MovementMode = MovementMode.Free;
        if (ClipboardManager.Instance != null)
            ClipboardManager.Instance.canClipboard = true;
    }

    public void ResetCameraPosition()
    {
        cinemachineInputAxisController.GetComponent<CinemachineOrbitalFollow>().HorizontalAxis.Value = 0;
        cinemachineInputAxisController.GetComponent<CinemachineOrbitalFollow>().HorizontalAxis.Reset();
        cinemachineInputAxisController.GetComponent<CinemachineOrbitalFollow>().HorizontalAxis.TriggerRecentering();
        cinemachineInputAxisController.GetComponent<CinemachineOrbitalFollow>().VerticalAxis.Value = 45;
        cinemachineInputAxisController.GetComponent<CinemachineOrbitalFollow>().VerticalAxis.Reset();
        cinemachineInputAxisController.GetComponent<CinemachineOrbitalFollow>().VerticalAxis.TriggerRecentering();
        StartCoroutine(ResetCameraPositionCoroutine());
    }
    
    IEnumerator ResetCameraPositionCoroutine()
    {
        cinemachineInputAxisController.GetComponent<CinemachineOrbitalFollow>().TrackerSettings.BindingMode = BindingMode.LockToTargetOnAssign;
        yield return null;
        cinemachineInputAxisController.GetComponent<CinemachineOrbitalFollow>().TrackerSettings.BindingMode = BindingMode.LazyFollow;
    }
}
