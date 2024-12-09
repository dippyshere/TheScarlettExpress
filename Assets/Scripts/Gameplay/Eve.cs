#region

using DialogueEditor;
using UnityEngine;
using System.Collections;

#endregion

public class Eve : MonoBehaviour
{
    public GameObject eveDrawing;
    public GameObject panelDialogue;
    public GameObject panelOptions;
    public GameObject shopEve;
    public GameObject tutorialEve;
    //public GameObject placeholderEve;

    public NPCConversation chihuahuaDialogue;
    public NPCConversation chihuahuaDialogue2;

    public AudioSource stationMusic;
    public AudioSource eveMusic;

    private Coroutine eveMusicFade;
    private Coroutine stationMusicFade;

    public GameObject shopMenuUI;

    public bool hasBeenReminded;

    private void Start()
    {
        ConversationManager.Instance.StartConversation(chihuahuaDialogue);
    }

    public void ShowPainting()
    {
        eveDrawing.SetActive(true);
    }

    public void HidePainting()
    {
        eveDrawing.SetActive(false);
    }

    //public void PlaceholderEve()
    //{
    //    tutorialEve.SetActive(false);
    //    placeholderEve.SetActive(true);
    //}

    public void ActivateShopEve()
    {
        //placeholderEve.SetActive(false);
        shopEve.SetActive(true);
        tutorialEve.SetActive(false);
        //panelDialogue.SetActive(false);
        //panelOptions.SetActive(false);
        MapTest.Instance.AbleToLeaveStation();
    }

    public void OpenShop()
    {
        shopMenuUI.SetActive(true);
        Invoke(nameof(VisibleClicker), 0.25f);
    }

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

    public void StartEveMusic()
    {
        //StartCoroutine(FadeOut(stationMusic, 1f));
        //StartCoroutine(FadeIn(eveMusic, 2f));

        StartFadeIn(ref eveMusicFade, eveMusic, 2f);
        StartFadeOut(ref stationMusicFade, stationMusic, 1f);
    }

    public void StopEveMusic()
    {
        //panelOptions.SetActive(false);

        //StartCoroutine(FadeOut(eveMusic, 1f));
        //StartCoroutine(FadeIn(stationMusic, 2f));

        StartFadeIn(ref stationMusicFade, stationMusic, 2f);
        StartFadeOut(ref eveMusicFade, eveMusic, 1f);

        //ConversationManager.Instance.StartConversation(chihuahuaDialogue2);
        if (!hasBeenReminded)
        {
            Invoke(nameof(ChihuahuaReminder), 2f);
        }
        //Invoke(nameof(ChihuahuaReminder), 2f);

        //CameraManager.Instance.SetInputModeUI(false);
    }

    public void ChihuahuaReminder()
    {
        if (!shopMenuUI.activeSelf)
        {
            ConversationManager.Instance.StartConversation(chihuahuaDialogue2);
            hasBeenReminded = true;
        }
    }

    public void VisibleClicker()
    {
        CameraManager.Instance.SetInputModeUI(true);
    }
}