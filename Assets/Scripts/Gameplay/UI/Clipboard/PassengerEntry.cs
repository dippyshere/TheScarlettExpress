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

    public void SetPassengerInfo(Sprite portrait, string passengerName, string species, string happinessRating,
        string hungerRating, string comfortRating)
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
    }
}