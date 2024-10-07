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

        int deco13 = ProfileSystem.Get<int>(ProfileSystem.Variable.Deco13);
        if (deco13 == 1)
        {
            deco13Lock.SetActive(true);
            deco13Lock2.SetActive(true);
        }

        int deco14 = ProfileSystem.Get<int>(ProfileSystem.Variable.Deco14);
        if (deco14 == 1)
        {
            deco14Lock.SetActive(true);
            deco14Lock2.SetActive(true);
        }

        int deco15 = ProfileSystem.Get<int>(ProfileSystem.Variable.Deco15);
        if (deco15 == 1)
        {
            deco15Lock.SetActive(true);
            deco15Lock2.SetActive(true);
        }

        int deco16 = ProfileSystem.Get<int>(ProfileSystem.Variable.Deco16);
        if (deco16 == 1)
        {
            deco16Lock.SetActive(true);
            deco16Lock2.SetActive(true);
        }

        int deco17 = ProfileSystem.Get<int>(ProfileSystem.Variable.Deco17);
        if (deco17 == 1)
        {
            deco17Lock.SetActive(true);
            deco17Lock2.SetActive(true);
        }

        int deco18 = ProfileSystem.Get<int>(ProfileSystem.Variable.Deco18);
        if (deco18 == 1)
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

            ProfileSystem.Set(ProfileSystem.Variable.Deco13, 1);
            ProfileSystem.Set(ProfileSystem.Variable.PlayerMoney, money);
            deco13Lock.SetActive(true);
            deco13Lock2.SetActive(true);
        }
    }

    public void BuyDeco14()
    {
        if (money >= 10)
        {
            money -= 10;
            moneyText.text = "$ " + money;
            music.Play();

            ProfileSystem.Set(ProfileSystem.Variable.Deco14, 1);
            ProfileSystem.Set(ProfileSystem.Variable.PlayerMoney, money);
            deco14Lock.SetActive(true);
            deco14Lock2.SetActive(true);
        }
    }

    public void BuyDeco15()
    {
        if (money >= 10)
        {
            money -= 10;
            moneyText.text = "$ " + money;
            music.Play();

            ProfileSystem.Set(ProfileSystem.Variable.Deco15, 1);
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

            ProfileSystem.Set(ProfileSystem.Variable.Deco16, 1);
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

            ProfileSystem.Set(ProfileSystem.Variable.Deco17, 1);
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

            ProfileSystem.Set(ProfileSystem.Variable.Deco18, 1);
            ProfileSystem.Set(ProfileSystem.Variable.PlayerMoney, money);
            deco18Lock.SetActive(true);
            deco18Lock2.SetActive(true);
        }
    }


}
