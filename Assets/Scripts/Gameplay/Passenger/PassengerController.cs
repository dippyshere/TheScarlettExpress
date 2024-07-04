using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassengerController : MonoBehaviour
{
    [SerializeField, Tooltip("The type of food the passenger wants")]
    private FoodManager.FoodType foodType;
    [Header("Happiness variables")]
    [SerializeField, Tooltip("The current hunger level of the passenger (higher is better)"), Range(1, 3)]
    private float hungerLevel = 3f;
    [SerializeField, Tooltip("The current comfort level of the passenger (higher is better)"), Range(1, 3)]
    private float comfortLevel = 3f;
    [SerializeField, Tooltip("The current entertainment? level of the passenger (higher is better; unused)"), Range(0, 3)]
    private float entertainmentLevel = 0f;
    [SerializeField, Tooltip("The number of days the passenger has left on the train"), Min(0)]
    public int daysLeft = 3;
    [SerializeField] private GameObject UIPrompt;
    public Transform plateTransform;
    public bool hasBeenFed = false;

    private void Start()
    {
        SetRandomStats();
    }

    public void SetRandomStats()
    {
        hungerLevel = Random.Range(0, 3);
        comfortLevel = Random.Range(0, 3);
        //entertainmentLevel = Random.Range(0, 4);
        daysLeft = Random.Range(1, 5);
    }

    public float CalculateHappinessValue()
    {
        if (entertainmentLevel == 0)
        {
            return (hungerLevel + comfortLevel) / 5;
        }
        else
        {
            return (hungerLevel + comfortLevel + entertainmentLevel) / 5;
        }
    }

    public int CalculateSimpleFoodValue()
    {
        return (int)(CalculateHappinessValue() * 5);
    }

    public int CalculateTripValue()
    {
        return (int)(CalculateHappinessValue() * 15);
    }

    public void FeedPassenger(FoodManager.FoodType food)
    {
        UIPrompt.SetActive(false);
        if (food == foodType)
        {
            hungerLevel += 2;
        }
        else
        {
            hungerLevel += 1;
        }
        GameObject.FindGameObjectWithTag("Player").GetComponent<Economy>().AddMoney(CalculateSimpleFoodValue());
        hasBeenFed = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasBeenFed)
        {
            if (other.GetComponent<Pickup>().hasItem == true)
            {
                UIPrompt.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UIPrompt.SetActive(false);
        }
    }
}
