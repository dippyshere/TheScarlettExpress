using DialogueEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestaurantEve : MonoBehaviour
{
    public NPCConversation eveConversation;

    [SerializeField] private bool talkToEve = false;
    [SerializeField] private bool canTalkToEve = true;

    public SphereCollider cookingStove;
    public SphereCollider saladStove;
    public SphereCollider jellyStove;


    // Update is called once per frame
    void Update()
    {
        if (canTalkToEve)
        {
            if (talkToEve && Input.GetKeyDown(KeyCode.E))
            {
                BeginEveConversation();
                talkToEve = false;
            }
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Pickup"))
        {
            talkToEve = true;
        }
    }

    private void BeginEveConversation()
    {
        ConversationManager.Instance.StartConversation(eveConversation);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        canTalkToEve = false;
        cookingStove.enabled = true;
        saladStove.enabled = true;
        jellyStove.enabled = true;
    }
}
