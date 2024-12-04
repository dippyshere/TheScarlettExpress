using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpecialPassengerStationManager : MonoBehaviour
{
    public GameObject specialPassenger;
    public SphereCollider shopEve;

    public GameObject panelDialogue;
    public GameObject panelOptions;

    // Start is called before the first frame update
    void Start()
    {
        //ProfileSystem.ClearProfile();

        if (SceneManager.GetActiveScene().name == "_FurrowoodStation")
        {
            Debug.Log("The current scene is '_FurrowoodStation'.");
            ProfileSystem.Set(ProfileSystem.Variable.HasBeenToFurrowood, true);

            if (ProfileSystem.Get<bool>(ProfileSystem.Variable.EveQuestFinished) && ProfileSystem.Get<bool>(ProfileSystem.Variable.HasBeenToFernValley)
                && !ProfileSystem.Get<bool>(ProfileSystem.Variable.AcquiredPaints) && !ProfileSystem.Get<bool>(ProfileSystem.Variable.EveQuest2Started))
            {
                specialPassenger.SetActive(true);
                shopEve.enabled = false;
            }
            if (ProfileSystem.Get<bool>(ProfileSystem.Variable.AcquiredPaints))
            {
                specialPassenger.SetActive(false);
                shopEve.enabled = true;
            }
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

            if (ProfileSystem.Get<bool>(ProfileSystem.Variable.EveQuestFinished))
            {
                ProfileSystem.Set(ProfileSystem.Variable.HasBeenToFernValley, true);
                specialPassenger.SetActive(true);
                shopEve.enabled = false;
            }
            if (ProfileSystem.Get<bool>(ProfileSystem.Variable.AcquiredPaints))
            {
                specialPassenger.SetActive(false);
                shopEve.enabled = true;
            }
        }

        if (SceneManager.GetActiveScene().name == "_BranchviewStation")
        {
            Debug.Log("The current scene is '_BranchviewStation'.");
        }
    }

    public void AcquirePaints()
    {
        ProfileSystem.Set(ProfileSystem.Variable.AcquiredPaints, true);
    }

    public void StartEveQuest2()
    {
        ProfileSystem.Set(ProfileSystem.Variable.EveQuest2Started, true);
    }

    public void ActivateShopEve()
    {
        shopEve.enabled = true;
        panelDialogue.SetActive(false);
        panelOptions.SetActive(false);
    }
}
