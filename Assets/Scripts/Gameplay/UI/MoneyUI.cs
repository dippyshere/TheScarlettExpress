using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MoneyUI : MonoBehaviour
{
    public TextMeshProUGUI moneyText;

    public GameObject player;
    Economy economy;

    private void Start()
    {
        economy = player.GetComponent<Economy>();
    }

    // Update is called once per frame
    void Update()
    {
        moneyText.text = ": $" + economy.money;
    }
}
