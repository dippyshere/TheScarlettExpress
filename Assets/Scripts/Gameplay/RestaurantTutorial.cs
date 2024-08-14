using DialogueEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestaurantTutorial : MonoBehaviour
{
    bool hasCompletedRTutorial;
    public GameObject tutorialChihuahua;
    public GameObject exclamation;
    public GameObject foodSelection;
    public NPCConversation stoveTutorial;
    public NPCConversation nowWeWait;
    public NPCConversation beginRestaurantTutorial;

    [SerializeField] private bool stoveTime;
    [SerializeField] private bool canStoveTutorial = true;
    //[SerializeField] private bool waitingTime;

    public GameObject panelDialogue;

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
        
        if (!panelDialogue.activeSelf && !foodSelection.activeSelf)
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
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            stoveTime = true;
        }
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
}
