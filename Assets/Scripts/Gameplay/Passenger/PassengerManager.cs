using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassengerManager : MonoBehaviour
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
    [SerializeField] private GameObject UIPrompt;
    public Transform plateTransform;
    public bool hasBeenFed = false;

    [SerializeField] private GameObject moneyUi;

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

        moneyUi.GetComponent<AddedMoneyUI>().MoneyAnimation();
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
