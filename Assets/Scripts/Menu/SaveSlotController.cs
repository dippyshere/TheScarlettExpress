using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SaveSlotController : MonoBehaviour
{
    [SerializeField, Tooltip("The slot number this slot corresponds to"), Range(0,3)]
    int slotNumber;
    [SerializeField, Tooltip("The slot number text")]
    TextMeshProUGUI slotNumberText;
    [SerializeField, Tooltip("The slot name text")]
    TextMeshProUGUI slotNameText;
    [SerializeField, Tooltip("The slot date text")]
    TextMeshProUGUI slotDateText;
    [SerializeField, Tooltip("The slot money text")]
    TextMeshProUGUI slotMoneyText;
    [SerializeField, Tooltip("The slot destination text")]
    TextMeshProUGUI slotDestinationText;
    [SerializeField, Tooltip("The delete button")]
    Button deleteButton;
    
    public void PlayThisSlot()
    {
        Debug.Log("Playing slot " + slotNumber);
        ProfileSystem.CurrentSaveSlot = slotNumber;
        deleteButton.interactable = false;
        GetComponent<Button>().interactable = false;
        SceneManager.LoadSceneAsync(ProfileSystem.Get<string>(ProfileSystem.Variable.LastScene));
    }
    
    public void DeleteThisSlot()
    {
        Debug.Log("Deleting slot " + slotNumber);
        ProfileSystem.ClearProfile(slotNumber);
        PopulateData();
    }

    public void PopulateData()
    {
        slotNumberText.text = $"Slot {slotNumber + 1}";
        if (ProfileSystem.Get<string>(ProfileSystem.Variable.LastPlayed, slotNumber) == "")
        {
            slotNameText.text = "New";
            slotDateText.text = "";
            slotMoneyText.text = "$0";
            slotDestinationText.text = "";
            deleteButton.GetComponent<Image>().enabled = false;
            deleteButton.interactable = false;
        }
        else
        {
            deleteButton.GetComponent<Image>().enabled = true;
            deleteButton.interactable = true;
            slotNameText.text = ProfileSystem.Get<string>(ProfileSystem.Variable.PlayerName, slotNumber);
            slotDateText.text = $"Last played: {ProfileSystem.Get<string>(ProfileSystem.Variable.LastPlayed, slotNumber)}";
            slotMoneyText.text = $"${ProfileSystem.Get<float>(ProfileSystem.Variable.PlayerMoney, slotNumber)}";
            int stationDestination = ProfileSystem.Get<int>(ProfileSystem.Variable.StationDestination, slotNumber);
            switch (stationDestination)
            {
                case 1:
                    slotDestinationText.text = "Next Stop: Riverside";
                    break;
                case 2:
                    slotDestinationText.text = "Next Stop: Furrowood";
                    break;
                case 3:
                    slotDestinationText.text = "Next Stop: Thamp";
                    break;
                case 4:
                    slotDestinationText.text = "Next Stop: Branchview";
                    break;
                case 5:
                    slotDestinationText.text = "Next Stop: Fern Valley";
                    break;
                default:
                    slotDestinationText.text = "Next Stop: Adventure";
                    break;
            }
        }
    }
}
