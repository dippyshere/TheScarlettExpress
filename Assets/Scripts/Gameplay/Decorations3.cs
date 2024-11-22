#region

using Dypsloom.DypThePenguin.Scripts.Character;
using TMPro;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;

#endregion

public class Decorations3 : MonoBehaviour
{
    public GameObject deco13Lock;
    public GameObject deco13Lock2;
    public GameObject deco14Lock;
    public GameObject deco14Lock2;
    public GameObject deco15Lock;
    public GameObject deco15Lock2;
    public GameObject deco16Lock;
    public GameObject deco16Lock2;
    public GameObject deco17Lock;
    public GameObject deco17Lock2;
    public GameObject deco18Lock;
    public GameObject deco18Lock2;

    public float money;

    public TextMeshProUGUI moneyText;

    public AudioSource music;
    public GameObject shopUI;

    // Start is called before the first frame update
    void Awake()
    {
        money = ProfileSystem.Get<float>(ProfileSystem.Variable.PlayerMoney);
        moneyText.text = "$ " + money;

        int deco7 = ProfileSystem.Get<int>(ProfileSystem.Variable.Deco7);
        if (deco7 == 1)
        {
            deco13Lock.SetActive(true);
            deco13Lock2.SetActive(true);
        }

        int deco8 = ProfileSystem.Get<int>(ProfileSystem.Variable.Deco8);
        if (deco8 == 1)
        {
            deco14Lock.SetActive(true);
            deco14Lock2.SetActive(true);
        }

        int deco9 = ProfileSystem.Get<int>(ProfileSystem.Variable.Deco9);
        if (deco9 == 1)
        {
            deco15Lock.SetActive(true);
            deco15Lock2.SetActive(true);
        }

        int deco22 = ProfileSystem.Get<int>(ProfileSystem.Variable.Deco22);
        if (deco22 == 1)
        {
            deco16Lock.SetActive(true);
            deco16Lock2.SetActive(true);
        }

        int deco23 = ProfileSystem.Get<int>(ProfileSystem.Variable.Deco23);
        if (deco23 == 1)
        {
            deco17Lock.SetActive(true);
            deco17Lock2.SetActive(true);
        }

        int deco24 = ProfileSystem.Get<int>(ProfileSystem.Variable.Deco24);
        if (deco24 == 1)
        {
            deco18Lock.SetActive(true);
            deco18Lock2.SetActive(true);
        }
    }

    public void ExitShop()
    {
        shopUI.SetActive(false);
        CameraManager.Instance.SetInputModeGameplay();
    }
    public void BuyDeco13()
    {
        if (money >= 10)
        {
            money -= 10;
            moneyText.text = "$ " + money;
            music.Play();

            ProfileSystem.Set(ProfileSystem.Variable.Deco7, 1);
            ProfileSystem.Set(ProfileSystem.Variable.PlayerMoney, money);
            deco13Lock.SetActive(true);
            deco13Lock2.SetActive(true);
        }
    }

    public void BuyDeco14()
    {
        if (money >= 15)
        {
            money -= 15;
            moneyText.text = "$ " + money;
            music.Play();

            ProfileSystem.Set(ProfileSystem.Variable.Deco8, 1);
            ProfileSystem.Set(ProfileSystem.Variable.PlayerMoney, money);
            deco14Lock.SetActive(true);
            deco14Lock2.SetActive(true);
        }
    }

    public void BuyDeco15()
    {
        if (money >= 20)
        {
            money -= 20;
            moneyText.text = "$ " + money;
            music.Play();

            ProfileSystem.Set(ProfileSystem.Variable.Deco9, 1);
            ProfileSystem.Set(ProfileSystem.Variable.PlayerMoney, money);
            deco15Lock.SetActive(true);
            deco15Lock2.SetActive(true);
        }
    }
    public void BuyDeco16()
    {
        if (money >= 10)
        {
            money -= 10;
            moneyText.text = "$ " + money;
            music.Play();

            ProfileSystem.Set(ProfileSystem.Variable.Deco22, 1);
            ProfileSystem.Set(ProfileSystem.Variable.PlayerMoney, money);
            deco16Lock.SetActive(true);
            deco16Lock2.SetActive(true);
        }
    }

    public void BuyDeco17()
    {
        if (money >= 10)
        {
            money -= 10;
            moneyText.text = "$ " + money;
            music.Play();

            ProfileSystem.Set(ProfileSystem.Variable.Deco23, 1);
            ProfileSystem.Set(ProfileSystem.Variable.PlayerMoney, money);
            deco17Lock.SetActive(true);
            deco17Lock2.SetActive(true);
        }
    }

    public void BuyDeco18()
    {
        if (money >= 10)
        {
            money -= 10;
            moneyText.text = "$ " + money;
            music.Play();

            ProfileSystem.Set(ProfileSystem.Variable.Deco24, 1);
            ProfileSystem.Set(ProfileSystem.Variable.PlayerMoney, money);
            deco18Lock.SetActive(true);
            deco18Lock2.SetActive(true);
        }
    }


}
