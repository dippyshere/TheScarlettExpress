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

    public NPCConversation chihuahuaDialogue;
    public NPCConversation chihuahuaDialogue2;

    public AudioSource stationMusic;
    public AudioSource eveMusic;

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

    public void ActivateShopEve()
    {
        shopEve.SetActive(true);
        panelDialogue.SetActive(false);
        panelOptions.SetActive(false);
        MapTest.Instance.AbleToLeaveStation();
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

    public void StartEveMusic()
    {
        StartCoroutine(FadeOut(stationMusic, 1f));
        eveMusic.Play();
    }

    public void StopEveMusic()
    {
        StartCoroutine(FadeOut(eveMusic, 1f));
        StartCoroutine(FadeIn(stationMusic, 2f));

        //ConversationManager.Instance.StartConversation(chihuahuaDialogue2);
        Invoke(nameof(ChihuahuaReminder), 4f);
    }

    public void ChihuahuaReminder()
    {
        ConversationManager.Instance.StartConversation(chihuahuaDialogue2);
    }
}