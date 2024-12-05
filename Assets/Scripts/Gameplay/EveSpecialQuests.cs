#region

using DialogueEditor;
using Dypsloom.DypThePenguin.Scripts.Character;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

#endregion

public class EveSpecialQuests : MonoBehaviour
{
    public static EveSpecialQuests Instance;
    public FoodManager.FoodType lastFoodPickedUp;

    public NPCConversation conversation;
    public NPCConversation[] conversations;
    [FormerlySerializedAs("DialoguePanel")] public GameObject dialoguePanel;
    public bool isConversing;
    public bool deactivateOnBegin = true;
    
    bool hasRetrievedSoup;
    bool eveQuestStarted;
    bool eveQuestFinished;
    public NPCConversation soupConversation;
    public NPCConversation giveSoupConversation;
    public NPCConversation wrongOrderConversation;

    public GameObject randyPainting;
    public AudioSource paintingSound;
    public LoadingManager loadingManager;
    public GameObject paintingParticles;

    void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        //ProfileSystem.ClearProfile();

        hasRetrievedSoup = ProfileSystem.Get<bool>(ProfileSystem.Variable.RetrievedBroccoliSoup);
        eveQuestStarted = ProfileSystem.Get<bool>(ProfileSystem.Variable.EveQuestStarted);
        eveQuestFinished = ProfileSystem.Get<bool>(ProfileSystem.Variable.EveQuestFinished);

        if (eveQuestFinished)
        {
            randyPainting.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        hasRetrievedSoup = ProfileSystem.Get<bool>(ProfileSystem.Variable.RetrievedBroccoliSoup);
        eveQuestStarted = ProfileSystem.Get<bool>(ProfileSystem.Variable.EveQuestStarted);
        eveQuestFinished = ProfileSystem.Get<bool>(ProfileSystem.Variable.EveQuestFinished);

        if (isConversing && Input.GetKeyDown(KeyCode.E) && !eveQuestFinished)
        {
            BeginConversation();
        }

        if (isConversing && Input.GetKeyDown(KeyCode.E) && eveQuestFinished)
        {
            BeginSpeaking();
        }
    }

    public void GiveSoup()
    {
        hasRetrievedSoup = true;
    }

    void BeginConversation()
    {
        if (!hasRetrievedSoup)
        {
            if (!eveQuestStarted)
            {
                ConversationManager.Instance.StartConversation(soupConversation);
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

        if (hasRetrievedSoup)
        {
            if (lastFoodPickedUp == FoodManager.FoodType.Green && !eveQuestFinished)
            {
                ConversationManager.Instance.StartConversation(giveSoupConversation);
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

    public void StartEveQuest()
    {
        eveQuestStarted = true;
        ProfileSystem.Set(ProfileSystem.Variable.EveQuestStarted, true);
    }
    public void RetrieveBroccoliSoup()
    {
        if (!hasRetrievedSoup && eveQuestStarted)
        {
            Debug.Log("helloo!!");
        }
    }

    public void DrawRandy()
    {
        //randyPainting.SetActive(true);
        paintingSound.Play();
        Invoke(nameof(HideLoad), 1f);
    }

    public void HideLoad()
    {
        //paintingParticles.SetActive(true);
        randyPainting.SetActive(true);
        loadingManager.HideLoadingScreen();
        Invoke(nameof(Sparkles), 1f);
    }

    public void Sparkles()
    {
        paintingParticles.SetActive(true);
    }

    public void FinishEveQuest()
    {
        eveQuestFinished = true;
        ProfileSystem.Set(ProfileSystem.Variable.EveQuestFinished, true);
    }
}
