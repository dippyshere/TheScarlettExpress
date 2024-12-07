#region

using DialogueEditor;
using UnityEngine;

#endregion

public class RestaurantEve : MonoBehaviour
{
    [SerializeField] bool canTalkToEve = true;

    public SphereCollider cookingStove;
    public NPCConversation eveConversation;
    public SphereCollider jellyStove;
    public SphereCollider saladStove;

    [SerializeField] bool talkToEve;


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

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Pickup"))
        {
            talkToEve = true;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Pickup"))
        {
            talkToEve = false;
        }
    }

    void BeginEveConversation()
    {
        ConversationManager.Instance.StartConversation(eveConversation);
        canTalkToEve = false;
        cookingStove.enabled = true;
        saladStove.enabled = true;
        jellyStove.enabled = true;
    }
}