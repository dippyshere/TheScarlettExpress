using DialogueEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public float money;
    public NPCConversation earnedConversation;
    public NPCConversation renovatedConversation;
    public GameObject decrepitObjects;
    public GameObject sideviewCamera;
    public GameObject carriage2Camera;

    // Start is called before the first frame update
    void Start()
    {
        money = ProfileSystem.Get<float>(ProfileSystem.Variable.PlayerMoney);
    }

    // Update is called once per frame
    void Update()
    {
        if (money == 100)
        {
            BeginEarnedConversation();
        }

        if (!decrepitObjects.activeSelf && !sideviewCamera && !carriage2Camera)
        {
            BeginRenovatedConversation();
        }
    }

    private void BeginEarnedConversation()
    {
        ConversationManager.Instance.StartConversation(earnedConversation);
    }

    private void BeginRenovatedConversation()
    {
        ConversationManager.Instance.StartConversation(renovatedConversation);
    }
}
