using Dypsloom.DypThePenguin.Scripts.Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Cinemachine;

public class Decorations : MonoBehaviour
{
    public GameObject shopUI;
    public float money;

    [SerializeField, Tooltip("Reference to the player script.")]
    private Character m_Player;

    [SerializeField, Tooltip("Reference to the cinemachine input manager.")]
    private CinemachineInputAxisController m_CinemachineInputAxisController;

    public GameObject deco1Lock;
    public GameObject deco2Lock;
    public GameObject deco3Lock;
    public GameObject deco4Lock;
    public GameObject deco5Lock;

    public GameObject deco6Lock;
    public GameObject deco7Lock;
    public GameObject deco8Lock;
    public GameObject deco9Lock;
    public GameObject deco10Lock;

    private void Awake()
    {
        money = ProfileSystem.Get<float>(ProfileSystem.Variable.PlayerMoney);

        int deco1 = ProfileSystem.Get<int>(ProfileSystem.Variable.Deco1);
        if (deco1 == 1)
        {
            deco1Lock.SetActive(true);
        }

        int deco2 = ProfileSystem.Get<int>(ProfileSystem.Variable.Deco2);
        if (deco2 == 1)
        {
            deco2Lock.SetActive(true);
        }

        int deco3 = ProfileSystem.Get<int>(ProfileSystem.Variable.Deco3);
        if (deco3 == 1)
        {
            deco3Lock.SetActive(true);
        }

        int deco4 = ProfileSystem.Get<int>(ProfileSystem.Variable.Deco4);
        if (deco4 == 1)
        {
            deco4Lock.SetActive(true);
        }

        int deco5 = ProfileSystem.Get<int>(ProfileSystem.Variable.Deco5);
        if (deco5 == 1)
        {
            deco5Lock.SetActive(true);
        }

        int deco6 = ProfileSystem.Get<int>(ProfileSystem.Variable.Deco6);
        if (deco6 == 1)
        {
            deco6Lock.SetActive(true);
        }

        int deco7 = ProfileSystem.Get<int>(ProfileSystem.Variable.Deco7);
        if (deco7 == 1)
        {
            deco7Lock.SetActive(true);
        }

        int deco8 = ProfileSystem.Get<int>(ProfileSystem.Variable.Deco8);
        if (deco8 == 1)
        {
            deco8Lock.SetActive(true);
        }

        int deco9 = ProfileSystem.Get<int>(ProfileSystem.Variable.Deco9);
        if (deco9 == 1)
        {
            deco9Lock.SetActive(true);
        }

        int deco10 = ProfileSystem.Get<int>(ProfileSystem.Variable.Deco10);
        if (deco10 == 1)
        {
            deco10Lock.SetActive(true);
        }
    }


    public void ExitShop()
    {
        shopUI.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        m_CinemachineInputAxisController.enabled = true;

        m_Player.m_MovementMode = MovementMode.Free;
    }

    public void BuyDeco1()
    {
        if (money >= 10)
        {
            money -= 10;

            ProfileSystem.Set(ProfileSystem.Variable.Deco1, 1);
            ProfileSystem.Set(ProfileSystem.Variable.PlayerMoney, money);
            deco1Lock.SetActive(true);
        }
    }

    public void BuyDeco2()
    {
        if (money >= 15)
        {
            money -= 15;

            ProfileSystem.Set(ProfileSystem.Variable.Deco2, 1);
            ProfileSystem.Set(ProfileSystem.Variable.PlayerMoney, money);
            deco2Lock.SetActive(true);
        }
    }

    public void BuyDeco3()
    {
        if (money >= 20)
        {
            money -= 20;

            ProfileSystem.Set(ProfileSystem.Variable.Deco3, 1);
            ProfileSystem.Set(ProfileSystem.Variable.PlayerMoney, money);
            deco3Lock.SetActive(true);
        }
    }

    public void BuyDeco4()
    {
        if (money >= 25)
        {
            money -= 25;

            ProfileSystem.Set(ProfileSystem.Variable.Deco4, 1);
            ProfileSystem.Set(ProfileSystem.Variable.PlayerMoney, money);
            deco4Lock.SetActive(true);
        }
    }

    public void BuyDeco5()
    {
        if (money >= 30)
        {
            money -= 30;

            ProfileSystem.Set(ProfileSystem.Variable.Deco5, 1);
            ProfileSystem.Set(ProfileSystem.Variable.PlayerMoney, money);
            deco5Lock.SetActive(true);
        }
    }

    public void BuyDeco6()
    {
        if (money >= 10)
        {
            money -= 10;

            ProfileSystem.Set(ProfileSystem.Variable.Deco6, 1);
            ProfileSystem.Set(ProfileSystem.Variable.PlayerMoney, money);
            deco6Lock.SetActive(true);
        }
    }

    public void BuyDeco7()
    {
        if (money >= 10)
        {
            money -= 10;

            ProfileSystem.Set(ProfileSystem.Variable.Deco7, 1);
            ProfileSystem.Set(ProfileSystem.Variable.PlayerMoney, money);
            deco7Lock.SetActive(true);
        }
    }

    public void BuyDeco8()
    {
        if (money >= 10)
        {
            money -= 10;

            ProfileSystem.Set(ProfileSystem.Variable.Deco8, 1);
            ProfileSystem.Set(ProfileSystem.Variable.PlayerMoney, money);
            deco8Lock.SetActive(true);
        }
    }

    public void BuyDeco9()
    {
        if (money >= 10)
        {
            money -= 10;

            ProfileSystem.Set(ProfileSystem.Variable.Deco9, 1);
            ProfileSystem.Set(ProfileSystem.Variable.PlayerMoney, money);
            deco9Lock.SetActive(true);
        }
    }

    public void BuyDeco10()
    {
        if (money >= 10)
        {
            money -= 10;

            ProfileSystem.Set(ProfileSystem.Variable.Deco10, 1);
            ProfileSystem.Set(ProfileSystem.Variable.PlayerMoney, money);
            deco10Lock.SetActive(true);
        }
    }
}
