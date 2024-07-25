using Dypsloom.DypThePenguin.Scripts.Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Cinemachine;

public class MapTest : MonoBehaviour
{

    [SerializeField] GameObject canvas;
    [SerializeField] GameObject shopUI;

    [SerializeField, Tooltip("Reference to the player script.")]
    private Character m_Player;
    [SerializeField, Tooltip("Reference to the cinemachine input manager.")]
    private CinemachineInputAxisController m_CinemachineInputAxisController;

    public bool isEve;

    private void Start()
    {
        canvas.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        m_CinemachineInputAxisController.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
       if (isEve && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("EVE! ACCTIVATE!");
            shopUI.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            m_Player.m_MovementMode = MovementMode.Decorating;
            m_CinemachineInputAxisController.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Map"))
        {
            Debug.Log("MAP! ACTIVATE!");
            canvas.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            m_Player.m_MovementMode = MovementMode.Decorating;
            m_CinemachineInputAxisController.enabled = false;
        }

        if (other.CompareTag("Eve"))
        {
            isEve = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Eve"))
        {
            isEve = false;
        }
    }
}
