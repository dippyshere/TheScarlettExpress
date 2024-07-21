//using Dypsloom.DypThePenguin.Scripts.Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decorations : MonoBehaviour
{
    public GameObject shopUI;
    public float money;

    //[SerializeField, Tooltip("Reference to the player script.")]
    //private Character m_Player;

    private void Awake()
    {
        money = ProfileSystem.Get<float>(ProfileSystem.Variable.PlayerMoney);
    }

    public void ExitShop()
    {
        shopUI.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        //m_Player.m_MovementMode = MovementMode.Free;
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
        if (money >= 25)
        {
            money -= 25;

            ProfileSystem.Set(ProfileSystem.Variable.Deco2, 1);
        }
    }

    public void BuyDeco3()
    {
        if (money >= 50)
        {
            money -= 50;

            ProfileSystem.Set(ProfileSystem.Variable.Deco3, 1);
        }
    }

    public void BuyDeco4()
    {
        if (money >= 75)
        {
            money -= 75;

            ProfileSystem.Set(ProfileSystem.Variable.Deco4, 1);
        }
    }

    public void BuyDeco5()
    {
        if (money >= 150)
        {
            money -= 150;

            ProfileSystem.Set(ProfileSystem.Variable.Deco5, 1);
        }
    }
}
