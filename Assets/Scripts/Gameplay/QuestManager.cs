#region

using DialogueEditor;
using UnityEngine;

#endregion

public class QuestManager : MonoBehaviour
{
    public NPCConversation beginTutorial;
    public NPCConversation earnedConversation;
    bool _hasCheckedMoney;

    bool _hasRenovated;
    public float money;
    public NPCConversation renovatedConversation;

    void Start()
    {
        money = ProfileSystem.Get<float>(ProfileSystem.Variable.PlayerMoney);
        BeginTutorial();
    }

    // Update is called once per frame
    void Update()
    {
        money = ProfileSystem.Get<float>(ProfileSystem.Variable.PlayerMoney);

        if (money >= 100f && !_hasCheckedMoney)
        {
            _hasCheckedMoney = true;
            BeginEarnedConversation();
        }

        if (money <= 0f && !_hasRenovated)
        {
            InitiateConversation();
        }
    }

    void BeginTutorial()
    {
        ConversationManager.Instance.StartConversation(beginTutorial);
    }

    public void InitiateConversation()
    {
        Invoke(nameof(BeginRenovatedConversation), 4f);
        _hasRenovated = true;
    }

    void BeginEarnedConversation()
    {
        ConversationManager.Instance.StartConversation(earnedConversation);
    }

    public void BeginRenovatedConversation()
    {
        _hasRenovated = true;
        ConversationManager.Instance.StartConversation(renovatedConversation);
    }
}