using DialogueEditor;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public float money;
    public NPCConversation earnedConversation;
    public NPCConversation renovatedConversation;
    public GameObject sideviewCamera;
    public GameObject carriage2Camera;
    bool hasCheckedMoney = false;

    // Start is called before the first frame update
    void Start()
    {
        money = ProfileSystem.Get<float>(ProfileSystem.Variable.PlayerMoney);
    }

    // Update is called once per frame
    void Update()
    {
        money = ProfileSystem.Get<float>(ProfileSystem.Variable.PlayerMoney);

        if (money >= 100f && !hasCheckedMoney)
        {
            hasCheckedMoney = true;
            BeginEarnedConversation();
            Debug.Log("ahhhhhhh");
        }
    }

    public void InitiateConversation()
    {
        Invoke(nameof(BeginRenovatedConversation), 4f);
    }

    private void BeginEarnedConversation()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        ConversationManager.Instance.StartConversation(earnedConversation);
    }

    private void BeginRenovatedConversation()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        ConversationManager.Instance.StartConversation(renovatedConversation);
    }
}
