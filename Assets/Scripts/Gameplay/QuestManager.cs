#region

using DialogueEditor;
using UnityEngine;
using Dypsloom.DypThePenguin.Scripts.Character;
using UnityEngine.SceneManagement;

#endregion

public class QuestManager : MonoBehaviour
{
    public NPCConversation beginTutorial;
    public NPCConversation earnedConversation;
    bool _hasCheckedMoney;

    bool _hasRenovated;
    public float money;
    public NPCConversation renovatedConversation;

    bool isConversing;
    public bool deactivateOnBegin = true;

    public NPCConversation hintConversation;
    public NPCConversation hint2Conversation;
    public NPCConversation hint3Conversation;

    void Start()
    {
        //ProfileSystem.ClearProfile();
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

        if (isConversing && Input.GetKeyDown(KeyCode.E))
        {
            CheckMoney();
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

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isConversing = true;
            Character.Instance.promptUI.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isConversing = false;
            Character.Instance.promptUI.SetActive(false);
        }
    }

    void CheckMoney()
    {
        if (money <= 75)
        {
            ConversationManager.Instance.StartConversation(hintConversation);
            isConversing = false;
            Character.Instance.promptUI.SetActive(false);
        }

        if (money >= 76)
        {
            if (money <= 99)
            {
                ConversationManager.Instance.StartConversation(hint2Conversation);
                isConversing = false;
                Character.Instance.promptUI.SetActive(false);
            }
            //ConversationManager.Instance.StartConversation(hint2Conversation);
        }

        if (money >= 100 && !_hasRenovated)
        {
            ConversationManager.Instance.StartConversation(hint3Conversation);
            isConversing = false;
            Character.Instance.promptUI.SetActive(false);
        }
    }

    public void EnterFirstDay()
    {
        SceneManager.LoadScene("_RestaurantTutorial");
    }
}