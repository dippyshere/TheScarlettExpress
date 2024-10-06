#region

using DialogueEditor;
using UnityEngine;

#endregion

public class RestaurantTutorial : MonoBehaviour
{
    public NPCConversation beginRestaurantTutorial;
    [SerializeField] bool canStoveTutorial = true;

    public GameObject cookingStoveSelection;
    public GameObject drinkStoveSelection;
    public GameObject exclamation;
    bool _hasCompletedRTutorial;
    public GameObject jellyStoveSelection;

    public NPCConversation nowWeWait;
    //[SerializeField] private bool talkToEve = false;
    //[SerializeField] private bool waitingTime;

    public GameObject panelDialogue;

    public GameObject saladStoveSelection;
    //public NPCConversation eveConversation;

    [SerializeField] bool stoveTime;
    public NPCConversation stoveTutorial;
    public GameObject tutorialChihuahua;
    
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

        switch (_hasCompletedRTutorial)
        {
            case false:
                tutorialChihuahua.SetActive(true);
                break;
            case true:
                tutorialChihuahua.SetActive(false);
                break;
        }

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
        ConversationManager.Instance.StartConversation(nowWeWait);
    }

    //private void BeginEveConversation()
    //{
    //    ConversationManager.Instance.StartConversation(eveConversation);
    //}
}