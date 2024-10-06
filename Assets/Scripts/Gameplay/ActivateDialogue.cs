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
    [FormerlySerializedAs("DialoguePanel")] public GameObject dialoguePanel;
    public bool isConversing;

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
        ConversationManager.Instance.StartConversation(conversation);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        gameObject.SetActive(false);
        Character.Instance.promptUI.SetActive(false);
    }

    public void EnterTutorial()
    {
        SceneManager.LoadScene("_TrainTutorial");
    }

    public void EnterFirstDay()
    {
        SceneManager.LoadScene("_RestaurantTutorial");
    }
}