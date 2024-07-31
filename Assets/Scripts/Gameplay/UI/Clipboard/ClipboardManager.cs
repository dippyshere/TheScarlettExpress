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
                isClipboardActive = true;
                clipboardUI.SetActive(true);
                m_Player.m_MovementMode = MovementMode.Decorating;
                //PopulatePassengersUI();
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                m_CinemachineInputAxisController.enabled = false;
            }
            else
            {
                isClipboardActive = false;
                clipboardUI.SetActive(false);
                m_Player.m_MovementMode = MovementMode.Free;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                m_CinemachineInputAxisController.enabled = true;
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
            passengerEntry.SetPassengerInfo(passenger.portrait, passenger.passengerName, passenger.species, passenger.CalculateHappinessValue().ToString(), passenger.hungerLevel.ToString(), passenger.comfortLevel.ToString());
        }
        RectTransform rectTransform = passengerUI.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, m_PassengerManager.passengers.Count * 82);
    }

    public void PassengerRosterTime()
    {
        passUI.SetActive(true);
        mainMenuUI.SetActive(false);
        PopulatePassengersUI();
        
    }

    public void Back()
    {
        passUI.SetActive(false);
        mainMenuUI.SetActive(true);
    }

    public void NextDay()
    {
        int dist = ProfileSystem.Get<int>(ProfileSystem.Variable.StationDistance);
        ProfileSystem.Set(ProfileSystem.Variable.StationDistance, dist - 1);
        Debug.Log(dist);

        daysLeft--;
   
       
        isClipboardActive = false;
        clipboardUI.SetActive(false);
        m_Player.m_MovementMode = MovementMode.Free;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        m_CinemachineInputAxisController.enabled = true;
    }

}
