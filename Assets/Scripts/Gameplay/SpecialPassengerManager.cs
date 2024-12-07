using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialPassengerManager : MonoBehaviour
{
    public GameObject theRestaurantBanks;
    public GameObject roomBanks;

    // Start is called before the first frame update
    void Start()
    {
        //ProfileSystem.ClearProfile();

        if (ProfileSystem.Get<bool>(ProfileSystem.Variable.AcquiredTheBanks) && !ProfileSystem.Get<bool>(ProfileSystem.Variable.BanksHomed))
        {
            theRestaurantBanks.SetActive(true);
        }

        if (ProfileSystem.Get<bool>(ProfileSystem.Variable.AcquiredTheBanks) && ProfileSystem.Get<bool>(ProfileSystem.Variable.BanksHomed))
        {
            roomBanks.SetActive(true);
            theRestaurantBanks.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
