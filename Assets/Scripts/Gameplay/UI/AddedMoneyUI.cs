using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class AddedMoneyUI : MonoBehaviour
{
    public TextMeshProUGUI moneyTxt;
    public GameObject moneys;

    public float moneyy = 0;

    void Start()
    {
        moneys.SetActive(false);
    }

    void Update()
    {
        moneyTxt.text = "+ $" + moneyy;
    }

    public void AddMoney(float money)
    {
        moneyy = money;
    }

    public void MoneyAnimation()
    {
        StartCoroutine(MoneyAnim());
    }
    public IEnumerator MoneyAnim()
    {
        moneys.SetActive(true);
        yield return new WaitForSeconds(1);
        //moneys.GetComponent<Animation>().Play();  ANIMATION NOT CURRENTLY WORKING YET :P
        //yield return new WaitForSeconds(2);
        moneys.SetActive(false);
        moneyy = 0;
    }


}
