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

    private void Awake()
    {
        money = ProfileSystem.Get<float>(ProfileSystem.Variable.PlayerMoney);
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
        }
    }

    public void BuyDeco2()
    {
        if (money >= 15)
        {
            money -= 15;

            ProfileSystem.Set(ProfileSystem.Variable.Deco2, 1);
        }
    }

    public void BuyDeco3()
    {
        if (money >= 20)
        {
            money -= 20;

            ProfileSystem.Set(ProfileSystem.Variable.Deco3, 1);
        }
    }

    public void BuyDeco4()
    {
        if (money >= 25)
        {
            money -= 25;

            ProfileSystem.Set(ProfileSystem.Variable.Deco4, 1);
        }
    }

    public void BuyDeco5()
    {
        if (money >= 30)
        {
            money -= 30;

            ProfileSystem.Set(ProfileSystem.Variable.Deco5, 1);
        }
    }

    public void BuyDeco6()
    {
        if (money >= 10)
        {
            money -= 10;

            ProfileSystem.Set(ProfileSystem.Variable.Deco6, 1);
        }
    }

    public void BuyDeco7()
    {
        if (money >= 10)
        {
            money -= 10;

            ProfileSystem.Set(ProfileSystem.Variable.Deco7, 1);
        }
    }

    public void BuyDeco8()
    {
        if (money >= 10)
        {
            money -= 10;

            ProfileSystem.Set(ProfileSystem.Variable.Deco8, 1);
        }
    }

    public void BuyDeco9()
    {
        if (money >= 10)
        {
            money -= 10;

            ProfileSystem.Set(ProfileSystem.Variable.Deco9, 1);
        }
    }

    public void BuyDeco10()
    {
        if (money >= 10)
        {
            money -= 10;

            ProfileSystem.Set(ProfileSystem.Variable.Deco10, 1);
        }
    }
}
