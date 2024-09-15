using System.Collections;
using System.Collections.Generic;
using Dypsloom.DypThePenguin.Scripts.Character;
using UnityEngine;
using Unity.Cinemachine;

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
    
    public void SetInputModeUI(bool canClipboard = false)
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        cinemachineInputAxisController.enabled = false;
        if (Character.Instance != null)
            Character.Instance.m_MovementMode = MovementMode.Decorating;
        if (ClipboardManager.Instance != null)
            ClipboardManager.Instance.canClipboard = canClipboard;
    }
    
    public void SetInputModeGameplay()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        cinemachineInputAxisController.enabled = true;
        if (Character.Instance != null)
            Character.Instance.m_MovementMode = MovementMode.Free;
        if (ClipboardManager.Instance != null)
            ClipboardManager.Instance.canClipboard = true;
    }
}
