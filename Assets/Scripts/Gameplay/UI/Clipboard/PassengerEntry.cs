using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PassengerEntry : MonoBehaviour
{
    [Header("Passenger Info")]
    [Tooltip("Reference to the passenger's portrait")] public Image portrait;
    [Tooltip("Reference to the passenger's name text")] public TextMeshProUGUI passengerName;
    [Tooltip("Reference to the passenger's species text")] public TextMeshProUGUI species;
    [Tooltip("reference to placeholder happiness rating text")] public TextMeshProUGUI happinessRating;
    [Tooltip("reference to placeholder hunger rating text")] public TextMeshProUGUI hungerRating;
    [Tooltip("reference to placeholder comfort rating text")] public TextMeshProUGUI comfortRating;

    public void SetPassengerInfo(Sprite portrait, string passengerName, string species, string happinessRating, string hungerRating, string comfortRating)
    {
        if (portrait != null)
        {
            this.portrait.sprite = portrait;
        }
        this.passengerName.text = passengerName;
        this.species.text = species;
        this.happinessRating.text = "overall happiness: " + happinessRating + "/5";
        this.hungerRating.text = "hunger: " + hungerRating + "/3";
        this.comfortRating.text = "comfort: " + comfortRating + "/3";
    }
}
