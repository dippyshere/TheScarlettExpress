using DialogueEditor;
using Dypsloom.DypThePenguin.Scripts.Character;
using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class Cash : MonoBehaviour
{
    public bool isEarning;
    public float money;

    public NPCConversation conversation;

    [SerializeField, Tooltip("Reference to the player script.")]
    private Character m_Player;

    [SerializeField, Tooltip("Reference to the cinemachine input manager.")]
    private CinemachineInputAxisController m_CinemachineInputAxisController;

    public GameObject cashDialogue;

    // Start is called before the first frame update
    void Start()
    {
        money = ProfileSystem.Get<float>(ProfileSystem.Variable.PlayerMoney);
    }

    // Update is called once per frame
    void Update()
    {
        if (isEarning && Input.GetKeyDown(KeyCode.E))
        {
            this.gameObject.SetActive(false);
            isEarning = false;

            money += 5;
            ProfileSystem.Set(ProfileSystem.Variable.PlayerMoney, money);

            BeginConversation();

            cashDialogue.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isEarning = true;
        }
    }

    private void BeginConversation()
    {
        ConversationManager.Instance.StartConversation(conversation);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        //m_CinemachineInputAxisController.enabled = false;

        //m_Player.m_MovementMode = MovementMode.Decorating;
    }
}
