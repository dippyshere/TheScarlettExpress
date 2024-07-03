using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Economy : MonoBehaviour
{
    public float money = 0;

    public void AddMoney(float money)
    {
        this.money += money;
    }
}
