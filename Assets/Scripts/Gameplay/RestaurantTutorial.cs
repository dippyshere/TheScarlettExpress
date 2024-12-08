#region

using DialogueEditor;
using UnityEngine;

#endregion

public class RestaurantTutorial : MonoBehaviour
{
    public static RestaurantTutorial Instance;
    public NPCConversation beginRestaurantTutorial;
    [SerializeField] bool canStoveTutorial = true;

    public GameObject cookingStoveSelection;
    public GameObject drinkStoveSelection;
    public GameObject exclamation;
    public GameObject exclamation2;
    bool _hasCompletedRTutorial;
    public GameObject jellyStoveSelection;

    //public NPCConversation nowWeWait;
    //[SerializeField] private bool talkToEve = false;
    //[SerializeField] private bool waitingTime;

    public GameObject panelDialogue;

    public GameObject saladStoveSelection;
    //public NPCConversation eveConversation;

    [SerializeField] bool stoveTime;
    public NPCConversation stoveTutorial;
    public GameObject tutorialChihuahua;
    public GameObject stoveBlocks;

    public GameObject brownPassenger;
    public GameObject greenPassenger;
    public GameObject redPassenger;
    public GameObject pinkPassenger;

    public GameObject endDayConfirmationUI;

    public ClipboardManager clipboardManager;

    void Awake()
    {
        Instance = this;
    }
    
    void Start()
    {
        _hasCompletedRTutorial = ProfileSystem.Get<bool>(ProfileSystem.Variable.RestaurantTutorialDone);
        ConversationManager.Instance.StartConversation(beginRestaurantTutorial);
    }

    // Update is called once per frame
    void Update()
    {
//        if (panelDialogue.activeSelf)
//        {
//            Cursor.visible = true;
//            Cursor.lockState = CursorLockMode.None;
//        }
//
//        if (!panelDialogue.activeSelf && !drinkStoveSelection.activeSelf && !cookingStoveSelection.activeSelf &&
//            !saladStoveSelection.activeSelf && !jellyStoveSelection.activeSelf && !ClipboardManager.Instance.clipboardUI.activeSelf)
//        {
//            Cursor.visible = false;
//            Cursor.lockState = CursorLockMode.Locked;
//        }

        _hasCompletedRTutorial = ProfileSystem.Get<bool>(ProfileSystem.Variable.RestaurantTutorialDone);

        //switch (_hasCompletedRTutorial)
        //{
        //    case false:
        //        tutorialChihuahua.SetActive(true);
        //        break;
        //    case true:
        //        tutorialChihuahua.SetActive(false);
        //        break;
        //}

        if (canStoveTutorial)
        {
            if (stoveTime && Input.GetKeyDown(KeyCode.E))
            {
                BeginStoveTutorial();
            }
        }
        //if (stoveTime && Input.GetKeyDown(KeyCode.E))
        //{
        //    BeginStoveTutorial();
        //}

        //if (talkToEve && Input.GetKeyDown(KeyCode.E))
        //{
        //    BeginEveConversation();
        //}

        if (endDayConfirmationUI.activeSelf && Input.GetKeyDown(KeyCode.Tab))
        {
            endDayConfirmationUI.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            stoveTime = true;
        }

        //if (collision.gameObject.CompareTag("Pickup") && collision.gameObject.CompareTag("Player"))
        //{
        //    talkToEve = true;
        //}
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            stoveTime = false;
        }
    }

    public void RestaurantTutorialCompleted()
    {
        _hasCompletedRTutorial = true;
        ProfileSystem.Set(ProfileSystem.Variable.RestaurantTutorialDone, true);
    }

    public void StartTutorial()
    {
        exclamation.SetActive(true);
        canStoveTutorial = true;
    }

    void BeginStoveTutorial()
    {
        ConversationManager.Instance.StartConversation(stoveTutorial);

        canStoveTutorial = false;
        stoveTime = false;
    }

    public void WaitConversation()
    {
        exclamation.SetActive(false);
        //ConversationManager.Instance.StartConversation(nowWeWait);
        exclamation2.SetActive(true);
    }

    public void TalkingToEve()
    {
        exclamation2.SetActive(false);
        stoveBlocks.SetActive(false);
        brownPassenger.tag = "Passenger";
        greenPassenger.tag = "Passenger";
        redPassenger.tag = "Passenger";
        pinkPassenger.tag = "Passenger";

        brownPassenger.GetComponent<ActivateDialogue>().enabled = false;
        greenPassenger.GetComponent<ActivateDialogue>().enabled = false;
        redPassenger.GetComponent<ActivateDialogue>().enabled = false;
        pinkPassenger.GetComponent<ActivateDialogue>().enabled = false;
        tutorialChihuahua.GetComponent<ActivateDialogue>().enabled = false;
    }

    public void InvokeActivateDialogue()
    {
        if (!_hasCompletedRTutorial)
        {
            brownPassenger.GetComponent<ActivateDialogue>().enabled = true;
            greenPassenger.GetComponent<ActivateDialogue>().enabled = true;
            redPassenger.GetComponent<ActivateDialogue>().enabled = true;
            pinkPassenger.GetComponent<ActivateDialogue>().enabled = true;
            tutorialChihuahua.GetComponent<ActivateDialogue>().enabled = true;
        }
    }

    //private void BeginEveConversation()
    //{
    //    ConversationManager.Instance.StartConversation(eveConversation);
    //}

    public void EndDayConfirmation()
    {
        if (!_hasCompletedRTutorial)
        {
            endDayConfirmationUI.SetActive(true);
        }
        if (_hasCompletedRTutorial)
        {
            clipboardManager.NextDay();
            endDayConfirmationUI.SetActive(false);
        }
    }

    public void GoBack()
    {
        endDayConfirmationUI.SetActive(false);
    }
}