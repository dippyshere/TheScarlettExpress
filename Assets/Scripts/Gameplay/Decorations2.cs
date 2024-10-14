#region

using Dypsloom.DypThePenguin.Scripts.Character;
using TMPro;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;

#endregion

public class Decorations2 : MonoBehaviour
{
   
    public GameObject deco7Lock;
    public GameObject deco7Lock2;
    public GameObject deco8Lock;
    public GameObject deco8Lock2;
    public GameObject deco9Lock;
    public GameObject deco9Lock2;
    public GameObject deco10Lock;
    public GameObject deco10Lock2;
    public GameObject deco11Lock;
    public GameObject deco11Lock2;
    public GameObject deco12Lock;
    public GameObject deco12Lock2;

    public float money;

    public TextMeshProUGUI moneyText;

    public AudioSource music;
    public GameObject shopUI;

    void Awake()
    {
        money = ProfileSystem.Get<float>(ProfileSystem.Variable.PlayerMoney);
        moneyText.text = "$ " + money;

        int deco4 = ProfileSystem.Get<int>(ProfileSystem.Variable.Deco4);
        if (deco4 == 1)
        {
            deco7Lock.SetActive(true);
            deco7Lock2.SetActive(true);
        }

        int deco5 = ProfileSystem.Get<int>(ProfileSystem.Variable.Deco5);
        if (deco5 == 1)
        {
            deco8Lock.SetActive(true);
            deco8Lock2.SetActive(true);
        }

        int deco6 = ProfileSystem.Get<int>(ProfileSystem.Variable.Deco6);
        if (deco6 == 1)
        {
            deco9Lock.SetActive(true);
            deco9Lock2.SetActive(true);
        }

        int deco19 = ProfileSystem.Get<int>(ProfileSystem.Variable.Deco19);
        if (deco19 == 1)
        {
            deco10Lock.SetActive(true);
            deco10Lock2.SetActive(true);
        }

        int deco20 = ProfileSystem.Get<int>(ProfileSystem.Variable.Deco20);
        if (deco20 == 1)
        {
            deco11Lock.SetActive(true);
            deco11Lock2.SetActive(true);
        }

        int deco21 = ProfileSystem.Get<int>(ProfileSystem.Variable.Deco21);
        if (deco21 == 1)
        {
            deco12Lock.SetActive(true);
            deco12Lock2.SetActive(true);
        }

    }

    public void ExitShop()
    {
        shopUI.SetActive(false);
        CameraManager.Instance.SetInputModeGameplay();
    }

    public void BuyDeco7()
    {
        if (money >= 10)
        {
            money -= 10;
            moneyText.text = "$ " + money;
            music.Play();

            ProfileSystem.Set(ProfileSystem.Variable.Deco4, 1);
            ProfileSystem.Set(ProfileSystem.Variable.PlayerMoney, money);
            deco7Lock.SetActive(true);
            deco7Lock2.SetActive(true);
        }
    }

    public void BuyDeco8()
    {
        if (money >= 10)
        {
            money -= 10;
            moneyText.text = "$ " + money;
            music.Play();

            ProfileSystem.Set(ProfileSystem.Variable.Deco5, 1);
            ProfileSystem.Set(ProfileSystem.Variable.PlayerMoney, money);
            deco8Lock.SetActive(true);
            deco8Lock2.SetActive(true);
        }
    }

    public void BuyDeco9()
    {
        if (money >= 10)
        {
            money -= 10;
            moneyText.text = "$ " + money;
            music.Play();

            ProfileSystem.Set(ProfileSystem.Variable.Deco6, 1);
            ProfileSystem.Set(ProfileSystem.Variable.PlayerMoney, money);
            deco9Lock.SetActive(true);
            deco9Lock2.SetActive(true);
        }
    }


    public void BuyDeco10()
    {
        if (money >= 10)
        {
            money -= 10;
            moneyText.text = "$ " + money;
            music.Play();

            ProfileSystem.Set(ProfileSystem.Variable.Deco19, 1);
            ProfileSystem.Set(ProfileSystem.Variable.PlayerMoney, money);
            deco10Lock.SetActive(true);
            deco10Lock2.SetActive(true);
        }
    }

    public void BuyDeco11()
    {
        if (money >= 10)
        {
            money -= 10;
            moneyText.text = "$ " + money;
            music.Play();

            ProfileSystem.Set(ProfileSystem.Variable.Deco20, 1);
            ProfileSystem.Set(ProfileSystem.Variable.PlayerMoney, money);
            deco11Lock.SetActive(true);
            deco11Lock2.SetActive(true);
        }
    }

    public void BuyDeco12()
    {
        if (money >= 10)
        {
            money -= 10;
            moneyText.text = "$ " + money;
            music.Play();

            ProfileSystem.Set(ProfileSystem.Variable.Deco21, 1);
            ProfileSystem.Set(ProfileSystem.Variable.PlayerMoney, money);
            deco12Lock.SetActive(true);
            deco12Lock2.SetActive(true);
        }
    }

}
