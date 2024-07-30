using DialogueEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateDialogue : MonoBehaviour
{
    NPCConversation conversation;
    public GameObject DialoguePanel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Chihuahua"))
        {
            StartConversation();
        }
    }

    private void StartConversation()
    {
        // Check if DialoguePanel is active
        if (ConversationManager.Instance.DialoguePanel.gameObject.activeInHierarchy)
        {
            Debug.LogWarning("Cannot start a new conversation while the dialogue panel is active.");
            return;
        }

        // Start the conversation
        ConversationManager.Instance.StartConversation(conversation);
    }
}
