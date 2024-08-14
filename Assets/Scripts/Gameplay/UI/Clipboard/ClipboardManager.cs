using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dypsloom.DypThePenguin.Scripts.Character;
using Unity.Cinemachine;
using TMPro;

public class ClipboardManager : MonoBehaviour
{
    [SerializeField, Tooltip("Reference to the player script.")]
    private Character m_Player;
    [SerializeField, Tooltip("Reference to the passenger manager script.")]
    private PassengerManager m_PassengerManager;
    [SerializeField, Tooltip("Reference to the cinemachine input manager.")]
    private CinemachineInputAxisController m_CinemachineInputAxisController;
    private bool isClipboardActive = false;
    [SerializeField] private GameObject clipboardUI;
    [SerializeField] private GameObject passengerUI;
    [SerializeField] private GameObject passengerUIPrefab;
    [SerializeField] private GameObject UpgradeUI;
    [SerializeField] private GameObject Carriage1;
    [SerializeField] private GameObject Carriage2;

    [SerializeField] AudioSource music;


    [SerializeField] private GameObject passUI;
    [SerializeField] private GameObject mainMenuUI;

    public TextMeshProUGUI daysLeftText;

    public int daysLeft;

    // Start is called before the first frame update
    void Start()
    {
        clipboardUI.SetActive(false);

        daysLeft = ProfileSystem.Get<int>(ProfileSystem.Variable.StationDistance);
      
        Debug.Log(ProfileSystem.Get<int>(ProfileSystem.Variable.StationDistance));
    }

    // Update is called once per frame
    void Update()
    {
        if (daysLeft == 1)
        {
            daysLeftText.text = daysLeft.ToString() + " Day Until Arrival";
        }
        else
        {
            daysLeftText.text = daysLeft.ToString() + " Days Until Arrival";
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (!isClipboardActive)
            {
                music.Play();
                isClipboardActive = true;
                clipboardUI.SetActive(true);
                m_Player.m_MovementMode = MovementMode.Decorating;
                //PopulatePassengersUI();
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                m_CinemachineInputAxisController.enabled = false;
                if (TrainGameAnalytics.instance != null)
                {
                    TrainGameAnalytics.instance.RecordGameEvent("clipboard_menu", new Dictionary<string, object>() { { "menuOpened", "clipboard" } });
                }
            }
            else
            {
                music.Play();
                isClipboardActive = false;
                clipboardUI.SetActive(false);
                m_Player.m_MovementMode = MovementMode.Free;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                m_CinemachineInputAxisController.enabled = true;
                if (TrainGameAnalytics.instance != null)
                {
                    TrainGameAnalytics.instance.RecordGameEvent("clipboard_menu", new Dictionary<string, object>() { { "menuClosed", "clipboard" } });
                }
            }
        }
    }

    private void PopulatePassengersUI()
    {
        foreach (Transform child in passengerUI.transform)
        {
            Destroy(child.gameObject);
        }
        foreach (PassengerController passenger in m_PassengerManager.passengers)
        {
            GameObject newPassengerUI = Instantiate(passengerUIPrefab, passengerUI.transform);
            PassengerEntry passengerEntry = newPassengerUI.GetComponent<PassengerEntry>();
            passengerEntry.SetPassengerInfo(passenger.portrait, passenger.passengerName, passenger.species, passenger.CalculateSimpleFoodValue().ToString(), passenger.hungerLevel.ToString(), passenger.comfortLevel.ToString());
        }
        RectTransform rectTransform = passengerUI.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, m_PassengerManager.passengers.Count * 82);
    }

    public void PassengerRosterTime()
    {
        music.Play();
        passUI.SetActive(true);
        mainMenuUI.SetActive(false);
        PopulatePassengersUI();
        if (TrainGameAnalytics.instance != null)
        {
            TrainGameAnalytics.instance.RecordGameEvent("clipboard_menu", new Dictionary<string, object>() { { "menuOpened", "passengerRoster" } });
        }
    }

    public void UpgradeMenu()
    {
        music.Play();
        UpgradeUI.SetActive(true);
        mainMenuUI.SetActive(false);
        if (TrainGameAnalytics.instance != null)
        {
            TrainGameAnalytics.instance.RecordGameEvent("clipboard_menu", new Dictionary<string, object>() { { "menuOpened", "upgradeMenu" } });
        }
    }

    public void CarriageUI1()
    {
        Carriage1.SetActive(true);
        Carriage2.SetActive(false);
        if (TrainGameAnalytics.instance != null)
        {
            TrainGameAnalytics.instance.RecordGameEvent("carriage_menu", new Dictionary<string, object>() { { "menuOpened", "carriage1" } });
        }
    }

    public void CarriageUI2()
    {
        Carriage1.SetActive(false);
        Carriage2.SetActive(true);
        if (TrainGameAnalytics.instance != null)
        {
            TrainGameAnalytics.instance.RecordGameEvent("carriage_menu", new Dictionary<string, object>() { { "menuOpened", "carriage2" } });
        }
    }

    public void Back()
    {
        passUI.SetActive(false);
        UpgradeUI.SetActive(false);
        mainMenuUI.SetActive(true);
        if (TrainGameAnalytics.instance != null)
        {
            TrainGameAnalytics.instance.RecordGameEvent("clipboard_menu", new Dictionary<string, object>() { { "menuOpened", "mainMenuBack" } });
        }
    }

    public void NextDay()
    {
        int dist = ProfileSystem.Get<int>(ProfileSystem.Variable.StationDistance);
        ProfileSystem.Set(ProfileSystem.Variable.StationDistance, dist - 1);
        m_PassengerManager.DayAdvanceCleanup();
        Debug.Log(dist);

        daysLeft--;
   
       
        isClipboardActive = false;
        clipboardUI.SetActive(false);
        m_Player.m_MovementMode = MovementMode.Free;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        m_CinemachineInputAxisController.enabled = true;

        if (TrainGameAnalytics.instance != null)
        {
            TrainGameAnalytics.instance.RecordGameEvent("next_day", new Dictionary<string, object>() { { "daysLeft", daysLeft }, { "currentStation", ProfileSystem.Get<string>(ProfileSystem.Variable.CurrentStation) }, { "stationDestination", ProfileSystem.Get<int>(ProfileSystem.Variable.StationDestination) } });
        }
    }

}
