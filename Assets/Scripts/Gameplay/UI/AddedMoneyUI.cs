#region

using System.Collections;
using TMPro;
using UnityEngine;

#endregion

public class AddedMoneyUI : MonoBehaviour
{
    public GameObject moneys;
    public TextMeshProUGUI moneyTxt;

    public float moneyy;

    void Start()
    {
        moneys.SetActive(false);
        UpdateAddedMoneyUI();
    }

    void UpdateAddedMoneyUI()
    {
        moneyTxt.text = "+ $" + moneyy;
    }

    public void MoneyAnimation(float addedMoney)
    {
        moneyy = addedMoney;
        UpdateAddedMoneyUI();
        StartCoroutine(MoneyAnim());
    }

    public IEnumerator MoneyAnim()
    {
        moneys.SetActive(true);
        yield return new WaitForSeconds(1);
        //moneys.GetComponent<Animation>().Play();  ANIMATION NOT CURRENTLY WORKING YET :P
        //yield return new WaitForSeconds(2);
        moneys.SetActive(false);
    }
}