#region

using TMPro;
using UnityEngine;
using UnityEngine.UI;

#endregion

public class PassengerEntry : MonoBehaviour
{
    [Tooltip("Reference to placeholder comfort rating text")]
    public TextMeshProUGUI comfortRating;

    [Tooltip("Reference to placeholder happiness rating text")]
    public TextMeshProUGUI happinessRating;

    [Tooltip("Reference to placeholder hunger rating text")]
    public TextMeshProUGUI hungerRating;

    [Tooltip("Reference to the passenger's name text")]
    public TextMeshProUGUI passengerName;

    [Header("Passenger Info"), Tooltip("Reference to the passenger's portrait")]
    
    public Image portrait;

    [Tooltip("Reference to the passenger's species text")]
    public TextMeshProUGUI species;
    
    [Tooltip("Reference to the passenger's destination text")]
    public TextMeshProUGUI destination;

    public void SetPassengerInfo(Sprite portrait, string passengerName, string species, string happinessRating,
        string hungerRating, string comfortRating, int destinationId)
    {
        if (portrait != null)
        {
            this.portrait.sprite = portrait;
        }

        this.passengerName.text = passengerName;
        this.species.text = species;
        this.happinessRating.text = "happiness: " + happinessRating + "/5";
        this.hungerRating.text = "hunger: " + hungerRating + "/3";
        this.comfortRating.text = "comfort: " + comfortRating + "/3";
        switch (destinationId)
        {
            case 1:
                destination.text = "Destination: Riverside";
                break;
            case 2:
                destination.text = "Destination: Furrowood";
                break;
            case 3:
                destination.text = "Destination: Thamp";
                break;
            case 4:
                destination.text = "Destination: Branchview";
                break;
            case 5:
                destination.text = "Destination: Fern Valley";
                break;
            default:
                destination.text = "";
                break;
        }
    }
}