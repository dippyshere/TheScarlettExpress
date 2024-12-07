#region

using DialogueEditor;
using Dypsloom.DypThePenguin.Scripts.Character;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

#endregion

public class ActivateDialogue : MonoBehaviour
{
    public NPCConversation conversation;
    public NPCConversation[] conversations;
    [FormerlySerializedAs("DialoguePanel")] public GameObject dialoguePanel;
    public bool isConversing;
    public bool deactivateOnBegin = true;

    public AudioSource ebonySparkles;

    // Update is called once per frame
    void Update()
    {
        if (isConversing && Input.GetKeyDown(KeyCode.E))
        {
            BeginConversation();
        }
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

    void BeginConversation()
    {
        if (conversation == null)
        {
            ConversationManager.Instance.StartConversation(conversations[Random.Range(0, conversations.Length)]);
            Character.Instance.promptUI.SetActive(false);
        }
        else
        {
            ConversationManager.Instance.StartConversation(conversation);
            Character.Instance.promptUI.SetActive(false);
            if (this.gameObject.tag == "Ebony")
            {
                ebonySparkles.Play();
            }
        }
        isConversing = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

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

    public void ReEnablePromptOnEnd()
    {
        Character.Instance.promptUI.SetActive(true);
        ConversationManager.OnConversationEnded -= ReEnablePromptOnEnd;
        isConversing = true;
    }

    public void EnterTutorial()
    {
        LoadingManager.Instance.LoadScene("_TrainTutorial");
    }

    public void EnterFirstDay()
    {
        LoadingManager.Instance.LoadScene("_RestaurantTutorial");
    }
}