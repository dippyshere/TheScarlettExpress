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

    public float nomini;

    private void Start()
    {
        
        

    }

    // Update is called once per frame
    void Update()
    {
        nomini = ProfileSystem.Get<float>(ProfileSystem.Variable.PlayerMoney);
        moneyText.text = ": $" + nomini;
    }
}
