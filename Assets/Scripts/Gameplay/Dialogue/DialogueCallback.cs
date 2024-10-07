using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class DialogueCallback : MonoBehaviour
{
    void OnEnable()
    {
        ConversationManager.OnConversationStarted += BeginDialogue;
        ConversationManager.OnConversationEnded += EndDialogue;
    }
    
    void OnDisable()
    {
        ConversationManager.OnConversationStarted -= BeginDialogue;
        ConversationManager.OnConversationEnded -= EndDialogue;
    }

    void BeginDialogue()
    {
        if (ClipboardManager.Instance)
        {
            ClipboardManager.Instance.canClipboard = false;
        }
        
        if (CameraManager.Instance)
        {
            CameraManager.Instance.SetInputModeUI();
        }
    }
    
    void EndDialogue()
    {
        if (ClipboardManager.Instance)
        {
            ClipboardManager.Instance.canClipboard = true;
        }
        
        if (CameraManager.Instance)
        {
            CameraManager.Instance.SetInputModeGameplay();
        }
    }
}
