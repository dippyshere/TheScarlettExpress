using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dypsloom.DypThePenguin.Scripts.Character;

public class ChangeCameraMode : MonoBehaviour
{
    [SerializeField, Tooltip("The camera to enable/disable when triggered.")]
    private GameObject m_Camera;
    [SerializeField, Tooltip("Whether to enable or disable the camera")]
    private bool m_EnableCamera;
    [SerializeField, Tooltip("The movement mode to switch to when triggered.")]
    private MovementMode m_MovementMode;
    [SerializeField, Tooltip("Reference to the player")]
    private Character m_Player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            m_Camera.SetActive(m_EnableCamera);
            m_Player.m_MovementMode = m_MovementMode;
        }
    }
}
