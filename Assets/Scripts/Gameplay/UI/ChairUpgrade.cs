using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChairUpgrade : MonoBehaviour
{

    public int upgradeLvl;
    public int upgradeCost;
    public float moneys;

    public AudioSource music;

    [SerializeField] GameObject baseUpgrade;
    [SerializeField] GameObject upgrade1;
    [SerializeField] GameObject upgrade2;
    [SerializeField] GameObject upgrade3;

    [SerializeField] GameObject upgradeStar1;
    [SerializeField] GameObject upgradeStar2;
    [SerializeField] GameObject upgradeStar3;


    [SerializeField] GameObject upgradeButton;

    public TextMeshProUGUI UpgradeCostText;

    private void Start()
    {
        baseUpgrade.SetActive(true);
        upgrade1.SetActive(false);
        upgrade2.SetActive(false);
        upgrade3.SetActive(false);
        upgradeButton.SetActive(true);
        upgradeLvl = 0;
        upgradeCost = 25;

        moneys = ProfileSystem.Get<float>(ProfileSystem.Variable.PlayerMoney);
    }

    private void Update()
    {
        UpgradeCostText.text = "$ " + upgradeCost.ToString();


    }

    public void UpgradeChair()
    {
        moneys = ProfileSystem.Get<float>(ProfileSystem.Variable.PlayerMoney);

        if (upgradeLvl == 2)
        {
            if (moneys >= 150)
            {
                moneys -= 150;
                ProfileSystem.Set(ProfileSystem.Variable.PlayerMoney, moneys);
                upgradeLvl++;
                music.Play();

                upgrade2.SetActive(false);
                upgrade3.SetActive(true);
                upgradeButton.SetActive(false);
                upgradeCost = 0;
                upgradeStar3.SetActive(true);
            }

        }

        if (upgradeLvl == 1)
        {
            if (moneys >= 75)
            {
                moneys -= 75;
                ProfileSystem.Set(ProfileSystem.Variable.PlayerMoney, moneys);
                upgradeLvl++;
                music.Play();

                upgrade1.SetActive(false);
                upgrade2.SetActive(true);
                upgradeCost = 150;
                upgradeStar2.SetActive(true);
            }

        }

        if (upgradeLvl == 0)
        {
            if(moneys >= 25)
            {
                moneys -= 25;
                ProfileSystem.Set(ProfileSystem.Variable.PlayerMoney, moneys);
                upgradeLvl++;
                music.Play();

                baseUpgrade.SetActive(false);
                upgrade1.SetActive(true);
                upgradeCost = 75;
                upgradeStar1.SetActive(true);
            }

        }


        
    }


}
