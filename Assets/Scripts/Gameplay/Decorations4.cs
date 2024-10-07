#region

using Dypsloom.DypThePenguin.Scripts.Character;
using TMPro;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;

#endregion

public class Decorations4 : MonoBehaviour
{
    public GameObject deco19Lock;
    public GameObject deco19Lock2;
    public GameObject deco20Lock;
    public GameObject deco20Lock2;
    public GameObject deco21Lock;
    public GameObject deco21Lock2;
    public GameObject deco22Lock;
    public GameObject deco22Lock2;
    public GameObject deco23Lock;
    public GameObject deco23Lock2;
    public GameObject deco24Lock;
    public GameObject deco24Lock2;

    public float money;

    public TextMeshProUGUI moneyText;

    public AudioSource music;
    public GameObject shopUI;

    // Start is called before the first frame update
    void Awake()
    {
        money = ProfileSystem.Get<float>(ProfileSystem.Variable.PlayerMoney);
        moneyText.text = "$ " + money;

        int deco19 = ProfileSystem.Get<int>(ProfileSystem.Variable.Deco19);
        if (deco19 == 1)
        {
            deco19Lock.SetActive(true);
            deco19Lock2.SetActive(true);
        }

        int deco20 = ProfileSystem.Get<int>(ProfileSystem.Variable.Deco20);
        if (deco20 == 1)
        {
            deco20Lock.SetActive(true);
            deco20Lock2.SetActive(true);
        }

        int deco21 = ProfileSystem.Get<int>(ProfileSystem.Variable.Deco21);
        if (deco21 == 1)
        {
            deco21Lock.SetActive(true);
            deco21Lock2.SetActive(true);
        }

        int deco22 = ProfileSystem.Get<int>(ProfileSystem.Variable.Deco22);
        if (deco22 == 1)
        {
            deco22Lock.SetActive(true);
            deco22Lock2.SetActive(true);
        }

        int deco23 = ProfileSystem.Get<int>(ProfileSystem.Variable.Deco23);
        if (deco23 == 1)
        {
            deco23Lock.SetActive(true);
            deco23Lock2.SetActive(true);
        }

        int deco24 = ProfileSystem.Get<int>(ProfileSystem.Variable.Deco24);
        if (deco24 == 1)
        {
            deco24Lock.SetActive(true);
            deco24Lock2.SetActive(true);
        }
    }

    public void ExitShop()
    {
        shopUI.SetActive(false);
        CameraManager.Instance.SetInputModeGameplay();
    }

     public void BuyDeco19()
    {
        if (money >= 10)
        {
            money -= 10;
            moneyText.text = "$ " + money;
            music.Play();

            ProfileSystem.Set(ProfileSystem.Variable.Deco19, 1);
            ProfileSystem.Set(ProfileSystem.Variable.PlayerMoney, money);
            deco19Lock.SetActive(true);
            deco19Lock2.SetActive(true);
        }
    }

    public void BuyDeco20()
    {
        if (money >= 15)
        {
            money -= 15;
            moneyText.text = "$ " + money;
            music.Play();

            ProfileSystem.Set(ProfileSystem.Variable.Deco20, 1);
            ProfileSystem.Set(ProfileSystem.Variable.PlayerMoney, money);
            deco20Lock.SetActive(true);
            deco20Lock2.SetActive(true);
        }
    }

    public void BuyDeco21()
    {
        if (money >= 15)
        {
            money -= 15;
            moneyText.text = "$ " + money;
            music.Play();

            ProfileSystem.Set(ProfileSystem.Variable.Deco21, 1);
            ProfileSystem.Set(ProfileSystem.Variable.PlayerMoney, money);
            deco21Lock.SetActive(true);
            deco21Lock2.SetActive(true);
        }
    }

    public void BuyDeco22()
    {
        if (money >= 15)
        {
            money -= 15;
            moneyText.text = "$ " + money;
            music.Play();

            ProfileSystem.Set(ProfileSystem.Variable.Deco22, 1);
            ProfileSystem.Set(ProfileSystem.Variable.PlayerMoney, money);
            deco22Lock.SetActive(true);
            deco22Lock2.SetActive(true);
        }
    }

    public void BuyDeco23()
    {
        if (money >= 15)
        {
            money -= 15;
            moneyText.text = "$ " + money;
            music.Play();

            ProfileSystem.Set(ProfileSystem.Variable.Deco23, 1);
            ProfileSystem.Set(ProfileSystem.Variable.PlayerMoney, money);
            deco23Lock.SetActive(true);
            deco23Lock2.SetActive(true);
        }
    }

    public void BuyDeco24()
    {
        if (money >= 15)
        {
            money -= 15;
            moneyText.text = "$ " + money;
            music.Play();

            ProfileSystem.Set(ProfileSystem.Variable.Deco24, 1);
            ProfileSystem.Set(ProfileSystem.Variable.PlayerMoney, money);
            deco24Lock.SetActive(true);
            deco24Lock2.SetActive(true);
        }
    }

}
