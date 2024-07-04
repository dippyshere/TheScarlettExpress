using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Economy : MonoBehaviour
{
    public float money = 0;
    public AddedMoneyUI addedMoneyUI;

    public void AddMoney(float money)
    {
        addedMoneyUI.MoneyAnimation(money);
        this.money += money;
    }
}
