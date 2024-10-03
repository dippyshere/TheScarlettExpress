#region

using DialogueEditor;
using Dypsloom.DypThePenguin.Scripts.Character;
using UnityEngine;

#endregion

public class Cash : MonoBehaviour
{
    public NPCConversation conversation;
    public bool isEarning;
    float _money;

    public AudioSource music;
    
    void Start()
    {
        _money = ProfileSystem.Get<float>(ProfileSystem.Variable.PlayerMoney);
    }

    // Update is called once per frame
    void Update()
    {
        if (isEarning && Input.GetKeyDown(KeyCode.E) && _money <= 75f)
        {
            music.Play();
            Invoke(nameof(DeleteCashGameObject), 0.01f);
            //this.gameObject.SetActive(false);
            isEarning = false;

            _money += 5;
            ProfileSystem.Set(ProfileSystem.Variable.PlayerMoney, _money);

            Character.Instance.promptUI.SetActive(false);
            BeginConversation();

            //cashDialogue.SetActive(false);
        }

        if (isEarning && Input.GetKeyDown(KeyCode.E) && _money >= 76f)
        {
            music.Play();
            Invoke(nameof(DeleteCashGameObject), 0.01f);
            //this.gameObject.SetActive(false);
            isEarning = false;

            _money += 5;
            ProfileSystem.Set(ProfileSystem.Variable.PlayerMoney, _money);

            Character.Instance.promptUI.SetActive(false);

            //cashDialogue.SetActive(false);
        }

        _money = ProfileSystem.Get<float>(ProfileSystem.Variable.PlayerMoney);
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isEarning = true;
            Character.Instance.promptUI.SetActive(true);
        }

        //if (this.gameObject.tag == "Cash1" && other.gameObject.tag == "Player")
        //{
        //    //BeginConversation();

        //    //cashDialogue.SetActive(false);
        //}
    }

    void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isEarning = false;
            Character.Instance.promptUI.SetActive(false);
        }
    }

    void DeleteCashGameObject()
    {
        gameObject.SetActive(false);
    }

    void BeginConversation()
    {
        ConversationManager.Instance.StartConversation(conversation);
    }
}