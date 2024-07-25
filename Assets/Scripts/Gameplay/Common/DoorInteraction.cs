using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dypsloom.DypThePenguin.Scripts.Character;
using Unity.Cinemachine;

public class DoorInteraction : MonoBehaviour
{
    [SerializeField, Tooltip("The camera to enable/disable when the door is interacted with.")]
    private GameObject m_Camera;
    [SerializeField, Tooltip("The UI Prompt to show when the player is near the door.")]
    private GameObject m_Prompt;
    [SerializeField, Tooltip("The Exit UI Prompt to show when inside the room.")]
    private GameObject m_ExitPrompt;
    [SerializeField, Tooltip("Reference to the player script.")]
    private Character m_Player;
    private bool m_IsPlayerNear;
    [SerializeField, Tooltip("Reference to the cinemachine input manager.")]
    private CinemachineInputAxisController m_CinemachineInputAxisController;

    public GameObject decorationUpgradeCanvas;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            m_Prompt.SetActive(true);
            m_IsPlayerNear = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            m_Prompt.SetActive(false);
            m_IsPlayerNear = false;
        }
    }

    void Update()
    {
        if (m_Prompt.activeSelf && Input.GetKeyDown(KeyCode.E) && m_IsPlayerNear)
        {
            m_Camera.SetActive(true);
            m_Prompt.SetActive(false);
            m_ExitPrompt.SetActive(true);
            m_Player.m_MovementMode = MovementMode.Decorating;
            m_IsPlayerNear = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            m_CinemachineInputAxisController.enabled = false;
        }
    }

    public void ExitRoom()
    {
        m_Camera.SetActive(false);
        m_ExitPrompt.SetActive(false);
        m_Player.m_MovementMode = MovementMode.Free;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        decorationUpgradeCanvas.SetActive(false);
        m_CinemachineInputAxisController.enabled = true;
    }
}
