#region

using DialogueEditor;
using Dypsloom.DypThePenguin.Scripts.Character;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;

#endregion

public class MapTest : MonoBehaviour
{
    [HideInInspector, Tooltip("Singleton instance of the MapTest.")]
    public static MapTest Instance;
    [SerializeField] GameObject canvas;
    public NPCConversation eveReminder;

    bool _hasTalkedToEve;

    [HideInInspector] public bool isEve;
    [HideInInspector] public bool isMap;

    public AudioSource music;
    [SerializeField] GameObject shopUI;

    void Awake()
    {
        Instance = this;
    }
    
    void Start()
    {
        canvas.SetActive(false);
    }

    void Update()
    {
        if (isEve && Input.GetKeyDown(KeyCode.E))
        {
            shopUI.SetActive(true);
            CameraManager.Instance.SetInputModeUI();
        }

        if (isMap && Input.GetKeyDown(KeyCode.E))
        {
            if ((_hasTalkedToEve && eveReminder) || !eveReminder)
            {
                canvas.SetActive(true);
                CameraManager.Instance.SetInputModeUI();
            }
            else
            {
                ConversationManager.Instance.StartConversation(eveReminder);
            }
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