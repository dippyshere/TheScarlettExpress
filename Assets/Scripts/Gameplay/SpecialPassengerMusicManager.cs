using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpecialPassengerMusicManager : MonoBehaviour
{
    public AudioSource specialPassengerMusic;
    public AudioSource trainMusic;
    public AudioSource banksMusic;

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

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(FadeOut(trainMusic, 1f));
            //specialPassengerMusic.Play();
            StartCoroutine(FadeIn(specialPassengerMusic, 2f));

            if (SceneManager.GetActiveScene().name == "_ThampStation" && ProfileSystem.Get<bool>(ProfileSystem.Variable.HasBeenToFurrowood) && !ProfileSystem.Get<bool>(ProfileSystem.Variable.AcquiredTheBanks))
            {
                trainMusic.mute = true;
                StartCoroutine(FadeOut(banksMusic, 1f));
                StartCoroutine(FadeIn(specialPassengerMusic, 2f));
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(FadeOut(specialPassengerMusic, 1f));
            StartCoroutine(FadeIn(trainMusic, 2f));

            if (SceneManager.GetActiveScene().name == "_ThampStation" && ProfileSystem.Get<bool>(ProfileSystem.Variable.HasBeenToFurrowood) && !ProfileSystem.Get<bool>(ProfileSystem.Variable.AcquiredTheBanks))
            {
                trainMusic.mute = true;
                StartCoroutine(FadeOut(specialPassengerMusic, 1f));
                StartCoroutine(FadeIn(trainMusic, 2f));
            }
        }
    }
}
