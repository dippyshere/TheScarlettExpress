using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dypsloom.DypThePenguin.Scripts.Character;
using DialogueEditor;

public class DialogueCallback : MonoBehaviour
{
    public static DialogueCallback Instance;
    public bool inDialogue;
    
    void Awake()
    {
        Instance = this;
    }
    
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
        
        Character.Instance.promptGroup.alpha = 0;
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
        
        Character.Instance.promptGroup.alpha = 1;
    }
}
