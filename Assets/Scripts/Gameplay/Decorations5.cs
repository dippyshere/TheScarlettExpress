#region

using Dypsloom.DypThePenguin.Scripts.Character;
using TMPro;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;

#endregion

public class Decorations5 : MonoBehaviour
{

    public GameObject deco25Lock;
    public GameObject deco25Lock2;
    public GameObject deco26Lock;
    public GameObject deco26Lock2;
    public GameObject deco27Lock;
    public GameObject deco27Lock2;
    public GameObject deco28Lock;
    public GameObject deco28Lock2;
    public GameObject deco29Lock;
    public GameObject deco29Lock2;
    public GameObject deco30Lock;
    public GameObject deco30Lock2;

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
            deco25Lock.SetActive(true);
            deco25Lock2.SetActive(true);
        }

        int deco14 = ProfileSystem.Get<int>(ProfileSystem.Variable.Deco14);
        if (deco14 == 1)
        {
            deco26Lock.SetActive(true);
            deco26Lock2.SetActive(true);
        }

        int deco15 = ProfileSystem.Get<int>(ProfileSystem.Variable.Deco15);
        if (deco15 == 1)
        {
            deco27Lock.SetActive(true);
            deco27Lock2.SetActive(true);
        }

        int deco28 = ProfileSystem.Get<int>(ProfileSystem.Variable.Deco28);
        if (deco28 == 1)
        {
            deco28Lock.SetActive(true);
            deco28Lock2.SetActive(true);
        }

        int deco29 = ProfileSystem.Get<int>(ProfileSystem.Variable.Deco29);
        if (deco29 == 1)
        {
            deco29Lock.SetActive(true);
            deco29Lock2.SetActive(true);
        }

        int deco30 = ProfileSystem.Get<int>(ProfileSystem.Variable.Deco30);
        if (deco30 == 1)
        {
            deco30Lock.SetActive(true);
            deco30Lock2.SetActive(true);
        }
    }


    public void ExitShop()
    {
        shopUI.SetActive(false);
        CameraManager.Instance.SetInputModeGameplay();
    }
    public void BuyDeco25()
    {
        if (money >= 15)
        {
            money -= 15;
            moneyText.text = "$ " + money;
            music.Play();

            ProfileSystem.Set(ProfileSystem.Variable.Deco13, 1);
            ProfileSystem.Set(ProfileSystem.Variable.PlayerMoney, money);
            deco25Lock.SetActive(true);
            deco25Lock2.SetActive(true);
        }
    }

    public void BuyDeco26()
    {
        if (money >= 15)
        {
            money -= 15;
            moneyText.text = "$ " + money;
            music.Play();

            ProfileSystem.Set(ProfileSystem.Variable.Deco14, 1);
            ProfileSystem.Set(ProfileSystem.Variable.PlayerMoney, money);
            deco26Lock.SetActive(true);
            deco26Lock2.SetActive(true);
        }
    }

    public void BuyDeco27()
    {
        if (money >= 15)
        {
            money -= 15;
            moneyText.text = "$ " + money;
            music.Play();

            ProfileSystem.Set(ProfileSystem.Variable.Deco15, 1);
            ProfileSystem.Set(ProfileSystem.Variable.PlayerMoney, money);
            deco27Lock.SetActive(true);
            deco27Lock2.SetActive(true);
        }
    }

    public void BuyDeco28()
    {
        if (money >= 15)
        {
            money -= 15;
            moneyText.text = "$ " + money;
            music.Play();

            ProfileSystem.Set(ProfileSystem.Variable.Deco28, 1);
            ProfileSystem.Set(ProfileSystem.Variable.PlayerMoney, money);
            deco28Lock.SetActive(true);
            deco28Lock2.SetActive(true);
        }
    }

    public void BuyDeco29()
    {
        if (money >= 15)
        {
            money -= 15;
            moneyText.text = "$ " + money;
            music.Play();

            ProfileSystem.Set(ProfileSystem.Variable.Deco29, 1);
            ProfileSystem.Set(ProfileSystem.Variable.PlayerMoney, money);
            deco29Lock.SetActive(true);
            deco29Lock2.SetActive(true);
        }
    }


    public void BuyDeco30()
    {
        if (money >= 15)
        {
            money -= 15;
            moneyText.text = "$ " + money;
            music.Play();

            ProfileSystem.Set(ProfileSystem.Variable.Deco30, 1);
            ProfileSystem.Set(ProfileSystem.Variable.PlayerMoney, money);
            deco30Lock.SetActive(true);
            deco30Lock2.SetActive(true);
        }
    }

}
