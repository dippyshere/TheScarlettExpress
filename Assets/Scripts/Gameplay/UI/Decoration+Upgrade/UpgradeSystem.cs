using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeSystem : MonoBehaviour
{
    public GameObject bed1;
    public GameObject bed2;

    public GameObject upgradeUI;
    public GameObject hammer;

    public GameObject exitButton;

    [SerializeField] private ProfileSystem.Variable bedSaveKey = ProfileSystem.Variable.Bed1Upgrade;

    private void Start()
    {
        int bedUpgrade = ProfileSystem.Get<int>(bedSaveKey);
        switch (bedUpgrade)
        {
            case 1:
                Upgrade();
                break;
        }
    }

    private void Update()
    {
        if (bed2.activeSelf)
        {
            hammer.SetActive(false);
        }
    }

    public void UpgradeUI()
    {
        upgradeUI.SetActive(true);
        hammer.SetActive(false);
        exitButton.SetActive(false);
    }

    public void Upgrade()
    {
        ProfileSystem.Set(bedSaveKey, 1);
        bed1.SetActive(false);
        bed2.SetActive(true);
    }

    public void ExitUpgradeUI()
    {
        upgradeUI.SetActive(false);
        hammer.SetActive(true);
        exitButton.SetActive(true);
    }
}
