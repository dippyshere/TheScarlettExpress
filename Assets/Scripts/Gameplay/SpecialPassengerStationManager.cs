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

    public AudioSource specialPassengerMusic;
    public AudioSource stationMusic;

    // Start is called before the first frame update
    void Start()
    {
        //ProfileSystem.ClearProfile();

        if (SceneManager.GetActiveScene().name == "_FurrowoodStation")
        {
            Debug.Log("The current scene is '_FurrowoodStation'.");
            ProfileSystem.Set(ProfileSystem.Variable.HasBeenToFurrowood, true);

            if (ProfileSystem.Get<bool>(ProfileSystem.Variable.EveQuestFinished) && ProfileSystem.Get<bool>(ProfileSystem.Variable.HasBeenToFernValley)
                && !ProfileSystem.Get<bool>(ProfileSystem.Variable.AcquiredPaints) && ProfileSystem.Get<bool>(ProfileSystem.Variable.EveQuest2Started))
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
                specialPassengerMusic.Play();
            }

            if (ProfileSystem.Get<bool>(ProfileSystem.Variable.AcquiredTheBanks))
            {
                specialPassenger.SetActive(false);
                stationMusic.Play();
            }

            if (!ProfileSystem.Get<bool>(ProfileSystem.Variable.HasBeenToFurrowood) && !ProfileSystem.Get<bool>(ProfileSystem.Variable.AcquiredTheBanks))
            {
                stationMusic.Play();
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

    public IEnumerator FadeOut(AudioSource audioSource, float duration)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / duration;
            yield return null;
        }

        audioSource.volume = 0;
    }

    public IEnumerator FadeIn(AudioSource audioSource, float duration, float targetVolume = 0.1f)
    {
        audioSource.volume = 0; // Ensure volume starts at 0.
        //audioSource.Play();     // Start playing the audio.

        while (audioSource.volume < targetVolume)
        {
            audioSource.volume += targetVolume * Time.deltaTime / duration;
            yield return null;
        }

        audioSource.volume = targetVolume; // Ensure it reaches the target volume.
    }

    public void StartSpecialPassengerMusic()
    {
        StartCoroutine(FadeOut(stationMusic, 1f));
        specialPassengerMusic.Play();
    }

    public void StopSpecialPassengerMusic()
    {
        StartCoroutine(FadeOut(specialPassengerMusic, 1f));
        StartCoroutine(FadeIn(stationMusic, 2f));
    }
}
