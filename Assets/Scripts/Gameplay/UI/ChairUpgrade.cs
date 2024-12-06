#region

using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

#endregion

public class ChairUpgrade : MonoBehaviour
{
    [SerializeField] GameObject baseUpgrade;

    [SerializeField] ProfileSystem.Variable chairSaveKey = ProfileSystem.Variable.Restraunt1Table1;
    public float moneys;

    public AudioSource music;
    [SerializeField] GameObject upgrade1;
    [SerializeField] GameObject upgrade2;
    [SerializeField] GameObject upgrade3;

    [SerializeField] GameObject upgradeButton;
    public int upgradeCost;

    [FormerlySerializedAs("UpgradeCostText")] public TextMeshProUGUI upgradeCostText;

    public int upgradeLvl;

    [SerializeField] GameObject upgradeStar1;
    [SerializeField] GameObject upgradeStar2;
    [SerializeField] GameObject upgradeStar3;

    //public PassengerManager passengerManager;

    //[SerializeField] bool canPay;
    //[SerializeField] bool canPay2;
    //[SerializeField] bool canPay3;

    void Start()
    {
        baseUpgrade.SetActive(true);
        upgrade1.SetActive(false);
        upgrade2.SetActive(false);
        upgrade3.SetActive(false);
        upgradeButton.SetActive(true);
        upgradeLvl = 0;
        upgradeCost = 25;

        moneys = ProfileSystem.Get<float>(ProfileSystem.Variable.PlayerMoney);

        int chairUpgrade = ProfileSystem.Get<int>(chairSaveKey);
        switch (chairUpgrade)
        {
            case 1:
                baseUpgrade.SetActive(false);
                upgrade1.SetActive(true);
                upgradeCost = 75;
                upgradeLvl = 1;
                upgradeStar1.SetActive(true);
                break;
            case 2:
                baseUpgrade.SetActive(false);
                upgrade1.SetActive(false);
                upgrade2.SetActive(true);
                upgradeCost = 150;
                upgradeLvl = 2;
                upgradeStar1.SetActive(true);
                upgradeStar2.SetActive(true);
                break;
            case 3:
                baseUpgrade.SetActive(false);
                upgrade1.SetActive(false);
                upgrade2.SetActive(false);
                upgrade3.SetActive(true);
                upgradeCost = 0;
                upgradeLvl = 3;
                upgradeStar1.SetActive(true);
                upgradeStar2.SetActive(true);
                upgradeStar3.SetActive(true);
                break;
        }
    }

    void Update()
    {
        upgradeCostText.text = "$ " + upgradeCost;
        moneys = ProfileSystem.Get<float>(ProfileSystem.Variable.PlayerMoney);

        //if (upgrade1.activeSelf && this.gameObject.tag == "Bunk")
        //{
        //    canPay = true;
        //}

        //if (upgrade2.activeSelf && this.gameObject.tag == "Bunk")
        //{
        //    canPay2 = true;
        //}

        //if (upgrade3.activeSelf && this.gameObject.tag == "Bunk")
        //{
        //    canPay3 = true;
        //}
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

                ProfileSystem.Set(chairSaveKey, 3);
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

                ProfileSystem.Set(chairSaveKey, 2);
            }
        }

        if (upgradeLvl == 0)
        {
            if (moneys >= 25)
            {
                moneys -= 25;
                ProfileSystem.Set(ProfileSystem.Variable.PlayerMoney, moneys);
                upgradeLvl++;
                music.Play();

                baseUpgrade.SetActive(false);
                upgrade1.SetActive(true);
                upgradeCost = 75;
                upgradeStar1.SetActive(true);

                ProfileSystem.Set(chairSaveKey, 1);
            }
        }
    }

    //public void UpgradedBedroomReward()
    //{
    //    if (ProfileSystem.Get<int>(ProfileSystem.Variable.StationDistance) == 0)
    //    {
    //        if (canPay)
    //        {
    //            Debug.Log("upgrade 1 pay");
    //            moneys += 5;
    //            ProfileSystem.Set(ProfileSystem.Variable.PlayerMoney, moneys);
    //        }

    //        if (canPay2)
    //        {
    //            Debug.Log("upgrade 2 pay");
    //            moneys += 10;
    //            ProfileSystem.Set(ProfileSystem.Variable.PlayerMoney, moneys);
    //        }

    //        if (canPay3)
    //        {
    //            Debug.Log("upgrade 3 pay");
    //            moneys += 15;
    //            ProfileSystem.Set(ProfileSystem.Variable.PlayerMoney, moneys);
    //        }
    //    }
    //}
}