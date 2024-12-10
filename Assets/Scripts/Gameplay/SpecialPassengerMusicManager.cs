using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpecialPassengerMusicManager : MonoBehaviour
{
    public AudioSource specialPassengerMusic;
    public AudioSource trainMusic;
    public AudioSource banksMusic;

    private Coroutine specialPassengerMusicFade;
    private Coroutine trainMusicFade;
    private Coroutine banksMusicFade;

    private IEnumerator FadeOut(AudioSource audioSource, float duration)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / duration;
            if (audioSource.volume < 0) audioSource.volume = 0;
            yield return null;
        }

        audioSource.volume = 0;
    }

    private IEnumerator FadeIn(AudioSource audioSource, float duration, float targetVolume = 0.1f)
    {
        float startVolume = audioSource.volume; // Ensure volume starts at 0.
        //audioSource.Play();     // Start playing the audio.

        while (audioSource.volume < targetVolume)
        {
            audioSource.volume += targetVolume * Time.deltaTime / duration;
            if (audioSource.volume > targetVolume) audioSource.volume = targetVolume;
            yield return null;
        }

        audioSource.volume = targetVolume; // Ensure it reaches the target volume.
    }

    private void StartFadeIn(ref Coroutine fadeCoroutine, AudioSource audioSource, float duration, float targetVolume = 0.1f)
    {
        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
        }
        fadeCoroutine = StartCoroutine(FadeIn(audioSource, duration, targetVolume));
    }

    private void StartFadeOut(ref Coroutine fadeCoroutine, AudioSource audioSource, float duration)
    {
        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
        }
        fadeCoroutine = StartCoroutine(FadeOut(audioSource, duration));
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartFadeIn(ref specialPassengerMusicFade, specialPassengerMusic, 2f);
            StartFadeOut(ref trainMusicFade, trainMusic, 1f);

            if (SceneManager.GetActiveScene().name == "_ThampStation" && ProfileSystem.Get<bool>(ProfileSystem.Variable.HasBeenToFurrowood) && !ProfileSystem.Get<bool>(ProfileSystem.Variable.AcquiredTheBanks))
            {
                trainMusic.mute = true;
                StartFadeIn(ref specialPassengerMusicFade, specialPassengerMusic, 2f);
                StartFadeOut(ref banksMusicFade, banksMusic, 1f);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartFadeIn(ref trainMusicFade, trainMusic, 2f);
            StartFadeOut(ref specialPassengerMusicFade, specialPassengerMusic, 1f);

            if (SceneManager.GetActiveScene().name == "_ThampStation" && ProfileSystem.Get<bool>(ProfileSystem.Variable.HasBeenToFurrowood) && !ProfileSystem.Get<bool>(ProfileSystem.Variable.AcquiredTheBanks))
            {
                trainMusic.mute = true;
                StartFadeIn(ref banksMusicFade, banksMusic, 2f);
                StartFadeOut(ref specialPassengerMusicFade, specialPassengerMusic, 1f);
            }
        }
    }
}
