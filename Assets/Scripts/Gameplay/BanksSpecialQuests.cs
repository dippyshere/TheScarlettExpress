using DialogueEditor;
using Dypsloom.DypThePenguin.Scripts.Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.SceneManagement;

public class BanksSpecialQuests : MonoBehaviour
{
    public static BanksSpecialQuests Instance;
    public FoodManager.FoodType lastFoodPickedUp;

    public NPCConversation conversation;
    public NPCConversation[] conversations;
    [FormerlySerializedAs("DialoguePanel")] public GameObject dialoguePanel;
    public bool isConversing;
    public bool deactivateOnBegin = true;

    bool hasRetrievedSalad;
    bool banksQuestStarted;
    bool banksQuestFinished;

    public NPCConversation giveSaladConversation;
    public NPCConversation saladConversation;
    public NPCConversation wrongOrderConversation;

    void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        //ProfileSystem.ClearProfile();

        hasRetrievedSalad = ProfileSystem.Get<bool>(ProfileSystem.Variable.RetrievedYellowSpringSalad);
        banksQuestStarted = ProfileSystem.Get<bool>(ProfileSystem.Variable.BanksQuestStarted);
        banksQuestFinished = ProfileSystem.Get<bool>(ProfileSystem.Variable.BanksQuestFinished);
    }

    // Update is called once per frame
    void Update()
    {
        hasRetrievedSalad = ProfileSystem.Get<bool>(ProfileSystem.Variable.RetrievedYellowSpringSalad);
        banksQuestStarted = ProfileSystem.Get<bool>(ProfileSystem.Variable.BanksQuestStarted);
        banksQuestFinished = ProfileSystem.Get<bool>(ProfileSystem.Variable.BanksQuestFinished);

        if (isConversing && Input.GetKeyDown(KeyCode.E) && !banksQuestFinished)
        {
            BeginConversation();
        }

        if (isConversing && Input.GetKeyDown(KeyCode.E) && banksQuestFinished)
        {
            BeginSpeaking();
        }
    }

    public void GiveSalad()
    {
        hasRetrievedSalad = true;
    }

    void BeginConversation()
    {
        if (SceneManager.GetActiveScene().name == "_ThampStation")
        {
            ConversationManager.Instance.StartConversation(conversation);

            if (deactivateOnBegin)
            {
                gameObject.SetActive(false);
            }
            else
            {
                ConversationManager.OnConversationEnded += ReEnablePromptOnEnd;
            }
            Character.Instance.promptUI.SetActive(false);
        }

        if (SceneManager.GetActiveScene().name == "_TrainInterior")
        {
            if (!hasRetrievedSalad)
            {
                if (!banksQuestStarted)
                {
                    ConversationManager.Instance.StartConversation(saladConversation);
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                }
                else
                {
                    if (conversation == null)
                    {
                        ConversationManager.Instance.StartConversation(conversations[Random.Range(0, conversations.Length)]);
                        Cursor.visible = true;
                        Cursor.lockState = CursorLockMode.None;
                    }
                }
            }

            if (hasRetrievedSalad)
            {
                if (lastFoodPickedUp == FoodManager.FoodType.Yellow && !banksQuestFinished)
                {
                    ConversationManager.Instance.StartConversation(giveSaladConversation);
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                }
                else
                {
                    ConversationManager.Instance.StartConversation(wrongOrderConversation);
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                }
            }

            if (deactivateOnBegin)
            {
                gameObject.SetActive(false);
            }
            else
            {
                ConversationManager.OnConversationEnded += ReEnablePromptOnEnd;
            }
            Character.Instance.promptUI.SetActive(false);
        }
    }

    void BeginSpeaking()
    {
        ConversationManager.Instance.StartConversation(conversations[Random.Range(0, conversations.Length)]);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
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

    public void ReEnablePromptOnEnd()
    {
        Character.Instance.promptUI.SetActive(true);
        ConversationManager.OnConversationEnded -= ReEnablePromptOnEnd;
    }

    public void StartBanksQuest()
    {
        banksQuestStarted = true;
        ProfileSystem.Set(ProfileSystem.Variable.BanksQuestStarted, true);
    }

    public void FinishBanksQuest()
    {
        banksQuestFinished = true;
        ProfileSystem.Set(ProfileSystem.Variable.BanksQuestFinished, true);
    }

    public void AcquireTheBanks()
    {
        ProfileSystem.Set(ProfileSystem.Variable.AcquiredTheBanks, true);
    }

    public void HomedTheBanks()
    {
        ProfileSystem.Set(ProfileSystem.Variable.BanksHomed, true);
    }
}
