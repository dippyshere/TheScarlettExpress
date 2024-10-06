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

        int deco7 = ProfileSystem.Get<int>(ProfileSystem.Variable.Deco7);
        if (deco7 == 1)
        {
            deco7Lock.SetActive(true);
            deco7Lock2.SetActive(true);
        }

        int deco8 = ProfileSystem.Get<int>(ProfileSystem.Variable.Deco8);
        if (deco8 == 1)
        {
            deco8Lock.SetActive(true);
            deco8Lock2.SetActive(true);
        }

        int deco9 = ProfileSystem.Get<int>(ProfileSystem.Variable.Deco9);
        if (deco9 == 1)
        {
            deco9Lock.SetActive(true);
            deco9Lock2.SetActive(true);
        }

        int deco10 = ProfileSystem.Get<int>(ProfileSystem.Variable.Deco10);
        if (deco10 == 1)
        {
            deco10Lock.SetActive(true);
            deco10Lock2.SetActive(true);
        }

        int deco11 = ProfileSystem.Get<int>(ProfileSystem.Variable.Deco11);
        if (deco11 == 1)
        {
            deco11Lock.SetActive(true);
            deco11Lock2.SetActive(true);
        }

        int deco12 = ProfileSystem.Get<int>(ProfileSystem.Variable.Deco12);
        if (deco12 == 1)
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

            ProfileSystem.Set(ProfileSystem.Variable.Deco7, 1);
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

            ProfileSystem.Set(ProfileSystem.Variable.Deco8, 1);
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

            ProfileSystem.Set(ProfileSystem.Variable.Deco9, 1);
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

            ProfileSystem.Set(ProfileSystem.Variable.Deco10, 1);
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

            ProfileSystem.Set(ProfileSystem.Variable.Deco11, 1);
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

            ProfileSystem.Set(ProfileSystem.Variable.Deco12, 1);
            ProfileSystem.Set(ProfileSystem.Variable.PlayerMoney, money);
            deco12Lock.SetActive(true);
            deco12Lock2.SetActive(true);
        }
    }

}
