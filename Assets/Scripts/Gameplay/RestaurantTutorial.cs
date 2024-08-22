using DialogueEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestaurantTutorial : MonoBehaviour
{
    bool hasCompletedRTutorial;
    public GameObject tutorialChihuahua;
    public GameObject exclamation;
    public GameObject drinkStoveSelection;
    public GameObject cookingStoveSelection;
    public GameObject saladStoveSelection;
    public GameObject jellyStoveSelection;
    public NPCConversation stoveTutorial;
    public NPCConversation nowWeWait;
    public NPCConversation beginRestaurantTutorial;
    //public NPCConversation eveConversation;

    [SerializeField] private bool stoveTime;
    [SerializeField] private bool canStoveTutorial = true;
    //[SerializeField] private bool talkToEve = false;
    //[SerializeField] private bool waitingTime;

    public GameObject panelDialogue;

    public GameObject clipboard;

    // Start is called before the first frame update
    void Start()
    {
        hasCompletedRTutorial = ProfileSystem.Get<bool>(ProfileSystem.Variable.RestaurantTutorialDone);
        ConversationManager.Instance.StartConversation(beginRestaurantTutorial);
    }

    // Update is called once per frame
    void Update()
    {
        if (panelDialogue.activeSelf)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        
        if (!panelDialogue.activeSelf && !drinkStoveSelection.activeSelf && !cookingStoveSelection.activeSelf && !saladStoveSelection.activeSelf && !jellyStoveSelection.activeSelf && !clipboard.activeSelf)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        hasCompletedRTutorial = ProfileSystem.Get<bool>(ProfileSystem.Variable.RestaurantTutorialDone);

        if (!hasCompletedRTutorial)
        {
            tutorialChihuahua.SetActive(true);
        }

        if (hasCompletedRTutorial)
        {
            tutorialChihuahua.SetActive(false);
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

    private void OnTriggerEnter(Collider collision)
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

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            stoveTime = false;
        }
    }

    public void RestaurantTutorialCompleted()
    {
        hasCompletedRTutorial = true;
    }

    public void StartTutorial()
    {
        exclamation.SetActive(true);
        canStoveTutorial = true;
    }

    private void BeginStoveTutorial()
    {
        ConversationManager.Instance.StartConversation(stoveTutorial);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        canStoveTutorial = false;
        stoveTime = false;
    }

    public void WaitConversation()
    {
        exclamation.SetActive(false);
        ConversationManager.Instance.StartConversation(nowWeWait);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    //private void BeginEveConversation()
    //{
    //    ConversationManager.Instance.StartConversation(eveConversation);
    //    Cursor.visible = true;
    //    Cursor.lockState = CursorLockMode.None;
    //}
}
