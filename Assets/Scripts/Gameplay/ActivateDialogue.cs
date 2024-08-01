using DialogueEditor;
using Dypsloom.DypThePenguin.Scripts.Character;
using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ActivateDialogue : MonoBehaviour
{
    public NPCConversation conversation;
    public GameObject DialoguePanel;
    public bool isConversing;

    [SerializeField, Tooltip("Reference to the player script.")]
    private Character m_Player;

    [SerializeField, Tooltip("Reference to the cinemachine input manager.")]
    private CinemachineInputAxisController m_CinemachineInputAxisController;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked; 
    }

    // Update is called once per frame
    void Update()
    {
        if (isConversing && Input.GetKeyDown(KeyCode.E))
        {
            BeginConversation();
        }

        //if (!DialoguePanel.activeSelf)
        //{
        //    Cursor.visible = false;
        //    Cursor.lockState = CursorLockMode.Locked;

        //    m_CinemachineInputAxisController.enabled = true;

        //    m_Player.m_MovementMode = MovementMode.Free;
        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //StartConversation();
            //ConversationManager.Instance.StartConversation(conversation);
            //Cursor.visible = true;
            //Cursor.lockState = CursorLockMode.None;
            isConversing = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isConversing = false;
        }
    }

    private void BeginConversation()
    {
        ConversationManager.Instance.StartConversation(conversation);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        //m_CinemachineInputAxisController.enabled = false;

        //m_Player.m_MovementMode = MovementMode.Decorating;
    }

    public void EnterTutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void ResetMouseAndMovement()
    {
        //m_CinemachineInputAxisController.enabled = true;

        //m_Player.m_MovementMode = MovementMode.Free;

        //if (!DialoguePanel.activeSelf)
        //{
        //    Cursor.visible = false;
        //    Cursor.lockState = CursorLockMode.Locked;
        //}
    }

    public void EnterFirstDay()
    {
        SceneManager.LoadScene("PlayerTesting");
    }

    //private void StartConversation()
    //{
    //    // Check if DialoguePanel is active
    //    if (ConversationManager.Instance.DialoguePanel.gameObject.activeInHierarchy)
    //    {
    //        Debug.LogWarning("Cannot start a new conversation while the dialogue panel is active.");
    //        return;
    //    }

    //    // Start the conversation
    //    ConversationManager.Instance.StartConversation(conversation);
    //}
}
