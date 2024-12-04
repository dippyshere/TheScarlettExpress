#region

using System.Collections.Generic;
using System.Globalization;
using Dypsloom.DypThePenguin.Scripts.Character;
using TMPro;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;

#endregion

public class ClipboardManager : MonoBehaviour
{
    [HideInInspector, Tooltip("Singleton instance of the ClipboardManager.")]
    public static ClipboardManager Instance;
    public bool canClipboard;
    [FormerlySerializedAs("Carriage1"),SerializeField] GameObject carriage1;
    [FormerlySerializedAs("Carriage2"),SerializeField] GameObject carriage2;
    public GameObject clipboardUI;

    public int daysLeft;

    public TextMeshProUGUI daysLeftText;
    public bool _isClipboardActive;

    [SerializeField] GameObject mainMenuUI;

    [SerializeField] public GameObject tabButton;

    [SerializeField] AudioSource music;
    [SerializeField] GameObject passengerUI;
    [SerializeField] GameObject passengerUIPrefab;

    [SerializeField] GameObject passUI;
    [FormerlySerializedAs("UpgradeUI"),SerializeField] GameObject upgradeUI;

    public SideviewManager sideviewManager;

    void Awake()
    {
        Instance = this;
    }
    
    void Start()
    {
        canClipboard = true;
        clipboardUI.SetActive(false);

        daysLeft = ProfileSystem.Get<int>(ProfileSystem.Variable.StationDistance);
    }
    
    void Update()
    {
        if (daysLeft == 1)
        {
            daysLeftText.text = daysLeft + " Day Until Arrival";
        }
        else
        {
            daysLeftText.text = daysLeft + " Days Until Arrival";
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (canClipboard)
            {
                if (!_isClipboardActive)
                {
                    music.Play();
                    _isClipboardActive = true;
                    clipboardUI.SetActive(true);
                    tabButton.SetActive(false);
                    //PopulatePassengersUI();
                    CameraManager.Instance.SetInputModeUI(true);
                    if (TrainGameAnalytics.instance != null)
                    {
                        TrainGameAnalytics.instance.RecordGameEvent("clipboard_menu",
                            new Dictionary<string, object> { { "menuOpened", "clipboard" } });
                    }
                }
                else
                {
                    music.Play();
                    _isClipboardActive = false;
                    clipboardUI.SetActive(false);
                    tabButton.SetActive(true);
                    CameraManager.Instance.SetInputModeGameplay();
                    if (TrainGameAnalytics.instance != null)
                    {
                        TrainGameAnalytics.instance.RecordGameEvent("clipboard_menu",
                            new Dictionary<string, object> { { "menuClosed", "clipboard" } });
                    }

                    sideviewManager.CloseClipboard();
                }
            }
        }
    }

    void PopulatePassengersUI()
    {
        foreach (Transform child in passengerUI.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (PassengerController passenger in PassengerManager.Instance.passengers)
        {
            GameObject newPassengerUI = Instantiate(passengerUIPrefab, passengerUI.transform);
            PassengerEntry passengerEntry = newPassengerUI.GetComponent<PassengerEntry>();
            passengerEntry.SetPassengerInfo(passenger.portrait, passenger.passengerName, passenger.species,
                Mathf.Clamp(passenger.CalculateSimpleFoodValue(), 0, 5).ToString(), passenger.hungerLevel.ToString(CultureInfo.InvariantCulture),
                passenger.comfortLevel.ToString(CultureInfo.InvariantCulture), passenger.destinationId);
        }

        RectTransform rectTransform = passengerUI.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, PassengerManager.Instance.passengers.Count * 82);
    }

    public void PassengerRosterTime()
    {
        music.Play();
        passUI.SetActive(true);
        mainMenuUI.SetActive(false);
        PopulatePassengersUI();
        if (TrainGameAnalytics.instance != null)
        {
            TrainGameAnalytics.instance.RecordGameEvent("clipboard_menu",
                new Dictionary<string, object> { { "menuOpened", "passengerRoster" } });
        }
    }

    public void UpgradeMenu()
    {
        music.Play();
        //upgradeUI.SetActive(true);
        //mainMenuUI.SetActive(false);
        if (TrainGameAnalytics.instance != null)
        {
            TrainGameAnalytics.instance.RecordGameEvent("clipboard_menu",
                new Dictionary<string, object> { { "menuOpened", "upgradeMenu" } });
        }

        if (sideviewManager.isSideviewUpgrade)
        {
            sideviewManager.OpenCarriage1Upgrades();
        }
        else
        {
            upgradeUI.SetActive(true);
            mainMenuUI.SetActive(false);
        }
    }

    public void CarriageUI1()
    {
        carriage1.SetActive(true);
        carriage2.SetActive(false);
        if (TrainGameAnalytics.instance != null)
        {
            TrainGameAnalytics.instance.RecordGameEvent("carriage_menu",
                new Dictionary<string, object> { { "menuOpened", "carriage1" } });
        }
    }

    public void CarriageUI2()
    {
        carriage1.SetActive(false);
        carriage2.SetActive(true);
        if (TrainGameAnalytics.instance != null)
        {
            TrainGameAnalytics.instance.RecordGameEvent("carriage_menu",
                new Dictionary<string, object> { { "menuOpened", "carriage2" } });
        }
    }

    public void Back()
    {
        passUI.SetActive(false);
        upgradeUI.SetActive(false);
        mainMenuUI.SetActive(true);
        if (TrainGameAnalytics.instance != null)
        {
            TrainGameAnalytics.instance.RecordGameEvent("clipboard_menu",
                new Dictionary<string, object> { { "menuOpened", "mainMenuBack" } });
        }
    }

    public void NextDay()
    {
        int dist = ProfileSystem.Get<int>(ProfileSystem.Variable.StationDistance);
        ProfileSystem.Set(ProfileSystem.Variable.StationDistance, dist - 1);
        PassengerManager.Instance.DayAdvanceCleanup();

        daysLeft--;

        _isClipboardActive = false;
        clipboardUI.SetActive(false);
        CameraManager.Instance.SetInputModeGameplay();

        if (TrainGameAnalytics.instance != null)
        {
            TrainGameAnalytics.instance.RecordGameEvent("next_day",
                new Dictionary<string, object>
                {
                    { "daysLeft", daysLeft },
                    { "currentStation", ProfileSystem.Get<string>(ProfileSystem.Variable.CurrentStation) },
                    { "stationDestination", ProfileSystem.Get<int>(ProfileSystem.Variable.StationDestination) }
                });
        }
    }
}