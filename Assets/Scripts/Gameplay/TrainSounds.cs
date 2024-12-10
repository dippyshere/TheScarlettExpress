using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainSounds : MonoBehaviour
{
    public static TrainSounds Instance;

    public AudioSource brakeNoise;
    public AudioSource chugNoise;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void PlayBrakeNoise()
    {
        brakeNoise.time = 0.7f;
        brakeNoise.Play();
    }

    public void PlayChugNoiseQuick()
    {
        StartCoroutine(ChugNoise());
    }

    IEnumerator ChugNoise()
    {
        chugNoise.Play();
        yield return new WaitForSecondsRealtime(8f);
        while (chugNoise.volume > 0)
        {
            chugNoise.volume -= 0.01f;
            yield return new WaitForSeconds(0.1f);
        }
        chugNoise.Stop();
    }
}
