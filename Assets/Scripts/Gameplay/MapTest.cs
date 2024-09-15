#region

using DialogueEditor;
using Dypsloom.DypThePenguin.Scripts.Character;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;

#endregion

public class MapTest : MonoBehaviour
{
    [SerializeField] GameObject canvas;
    public NPCConversation eveReminder;

    bool _hasTalkedToEve;

    public bool isEve;
    public bool isMap;

    public AudioSource music;
    [SerializeField] GameObject shopUI;

    void Start()
    {
        canvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isEve && Input.GetKeyDown(KeyCode.E))
        {
            shopUI.SetActive(true);
            CameraManager.Instance.SetInputModeUI();
        }

        if (isMap && Input.GetKeyDown(KeyCode.E) && _hasTalkedToEve)
        {
            music.Play();
            canvas.SetActive(true);
            CameraManager.Instance.SetInputModeUI();
        }

        if (isMap && Input.GetKeyDown(KeyCode.E) && !_hasTalkedToEve)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            ConversationManager.Instance.StartConversation(eveReminder);
        }

        _hasTalkedToEve = ProfileSystem.Get<bool>(ProfileSystem.Variable.EveTutorialDone);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Map"))
        {
            isMap = true;
        }

        if (other.CompareTag("Eve"))
        {
            isEve = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Eve"))
        {
            isEve = false;
        }

        if (other.CompareTag("Map"))
        {
            isMap = false;
        }
    }

    public void AbleToLeaveStation()
    {
        ProfileSystem.Set(ProfileSystem.Variable.EveTutorialDone, true);
    }
}