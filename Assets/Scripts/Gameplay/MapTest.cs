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
    public bool isMap;

    public AudioSource music;

    bool hasTalkedToEve;

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

       if (isMap && Input.GetKeyDown(KeyCode.E) && hasTalkedToEve)
        {
            music.Play();
            Debug.Log("MAP! ACTIVATE!");
            canvas.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            m_Player.m_MovementMode = MovementMode.Decorating;
            m_CinemachineInputAxisController.enabled = false;
        }

        hasTalkedToEve = ProfileSystem.Get<bool>(ProfileSystem.Variable.EveTutorialDone);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Map"))
        {
            isMap = true;
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

        if (other.CompareTag("Map"))
        {
            isMap = false;
        }
    }
    public void AbleToLeaveStation()
    {
        ProfileSystem.Set(ProfileSystem.Variable.EveTutorialDone, true);
    }
}
