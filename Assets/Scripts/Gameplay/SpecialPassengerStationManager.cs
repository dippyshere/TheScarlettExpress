using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpecialPassengerStationManager : MonoBehaviour
{
    public GameObject specialPassenger;

    // Start is called before the first frame update
    void Start()
    {
        //ProfileSystem.ClearProfile();

        if (SceneManager.GetActiveScene().name == "_FurrowoodStation")
        {
            Debug.Log("The current scene is '_FurrowoodStation'.");
            ProfileSystem.Set(ProfileSystem.Variable.HasBeenToFurrowood, true);
        }

        if (SceneManager.GetActiveScene().name == "_ThampStation")
        {
            Debug.Log("The current scene is '_ThampStation'.");
            if (ProfileSystem.Get<bool>(ProfileSystem.Variable.HasBeenToFurrowood) && !ProfileSystem.Get<bool>(ProfileSystem.Variable.AcquiredTheBanks))
            {
                specialPassenger.SetActive(true);
            }
            if (ProfileSystem.Get<bool>(ProfileSystem.Variable.AcquiredTheBanks))
            {
                specialPassenger.SetActive(false);
            }
        }

        if (SceneManager.GetActiveScene().name == "_RiversideStation")
        {
            Debug.Log("The current scene is '_RiversideStation'.");
        }

        if (SceneManager.GetActiveScene().name == "_FernValleyStation")
        {
            Debug.Log("The current scene is '_FernValleyStation'.");
        }

        if (SceneManager.GetActiveScene().name == "_BranchviewStation")
        {
            Debug.Log("The current scene is '_BranchviewStation'.");
        }
    }
}
