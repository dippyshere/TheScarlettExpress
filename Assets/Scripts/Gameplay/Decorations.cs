#region

using Dypsloom.DypThePenguin.Scripts.Character;
using TMPro;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;

#endregion

public class Decorations : MonoBehaviour
{

    public GameObject deco1Lock;
    public GameObject deco1Lock2;
    public GameObject deco2Lock;
    public GameObject deco2Lock2;
    public GameObject deco3Lock;
    public GameObject deco3Lock2;
    public GameObject deco4Lock;
    public GameObject deco4Lock2;
    public GameObject deco5Lock;
    public GameObject deco5Lock2;
    public GameObject deco6Lock;
    public GameObject deco6Lock2;

    public float money;

    public TextMeshProUGUI moneyText;

    public AudioSource music;
    public GameObject shopUI;

    void Awake()
    {
        money = ProfileSystem.Get<float>(ProfileSystem.Variable.PlayerMoney);
        moneyText.text = "$ " + money;

        int deco1 = ProfileSystem.Get<int>(ProfileSystem.Variable.Deco1);
        if (deco1 == 1)
        {
            deco1Lock.SetActive(true);
            deco1Lock2.SetActive(true);
        }

        int deco2 = ProfileSystem.Get<int>(ProfileSystem.Variable.Deco2);
        if (deco2 == 1)
        {
            deco2Lock.SetActive(true);
            deco2Lock2.SetActive(true);
        }

        int deco3 = ProfileSystem.Get<int>(ProfileSystem.Variable.Deco3);
        if (deco3 == 1)
        {
            deco3Lock.SetActive(true);
            deco3Lock2.SetActive(true);
        }

        int deco4 = ProfileSystem.Get<int>(ProfileSystem.Variable.Deco4);
        if (deco4 == 1)
        {
            deco4Lock.SetActive(true);
            deco4Lock2.SetActive(true);
        }

        int deco5 = ProfileSystem.Get<int>(ProfileSystem.Variable.Deco5);
        if (deco5 == 1)
        {
            deco5Lock.SetActive(true);
            deco5Lock2.SetActive(true);
        }

        int deco6 = ProfileSystem.Get<int>(ProfileSystem.Variable.Deco6);
        if (deco6 == 1)
        {
            deco6Lock.SetActive(true);
            deco6Lock2.SetActive(true);
        }

       

       
    }


    public void ExitShop()
    {
        shopUI.SetActive(false);
        CameraManager.Instance.SetInputModeGameplay();
    }

    public void BuyDeco1()
    {
        if (money >= 10)
        {
            money -= 10;
            moneyText.text = "$ " + money;
            music.Play();

            ProfileSystem.Set(ProfileSystem.Variable.Deco1, 1);
            ProfileSystem.Set(ProfileSystem.Variable.PlayerMoney, money);
            deco1Lock.SetActive(true);
            deco1Lock2.SetActive(true);
        }
    }

    public void BuyDeco2()
    {
        if (money >= 15)
        {
            money -= 15;
            moneyText.text = "$ " + money;
            music.Play();

            ProfileSystem.Set(ProfileSystem.Variable.Deco2, 1);
            ProfileSystem.Set(ProfileSystem.Variable.PlayerMoney, money);
            deco2Lock.SetActive(true);
            deco2Lock2.SetActive(true);
        }
    }

    public void BuyDeco3()
    {
        if (money >= 20)
        {
            money -= 20;
            moneyText.text = "$ " + money;
            music.Play();

            ProfileSystem.Set(ProfileSystem.Variable.Deco3, 1);
            ProfileSystem.Set(ProfileSystem.Variable.PlayerMoney, money);
            deco3Lock.SetActive(true);
            deco3Lock2.SetActive(true);
        }
    }

    public void BuyDeco4()
    {
        if (money >= 25)
        {
            money -= 25;
            moneyText.text = "$ " + money;
            music.Play();

            ProfileSystem.Set(ProfileSystem.Variable.Deco4, 1);
            ProfileSystem.Set(ProfileSystem.Variable.PlayerMoney, money);
            deco4Lock.SetActive(true);
            deco4Lock2.SetActive(true);
        }
    }

    public void BuyDeco5()
    {
        if (money >= 30)
        {
            money -= 30;
            moneyText.text = "$ " + money;
            music.Play();

            ProfileSystem.Set(ProfileSystem.Variable.Deco5, 1);
            ProfileSystem.Set(ProfileSystem.Variable.PlayerMoney, money);
            deco5Lock.SetActive(true);
            deco5Lock2.SetActive(true);
        }
    }

    public void BuyDeco6()
    {
        if (money >= 10)
        {
            money -= 10;
            moneyText.text = "$ " + money;
            music.Play();

            ProfileSystem.Set(ProfileSystem.Variable.Deco6, 1);
            ProfileSystem.Set(ProfileSystem.Variable.PlayerMoney, money);
            deco6Lock.SetActive(true);
            deco6Lock2.SetActive(true);
        }
    }

   

   

   

}