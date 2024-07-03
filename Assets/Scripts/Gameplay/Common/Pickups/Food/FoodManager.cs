using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodManager : MonoBehaviour
{
    [SerializeField, Tooltip("The type of food")] public FoodType foodType;

    public enum FoodType
    {
        Generic,
        Green,
        Pink,
        Red,
        Yellow
    }
}
