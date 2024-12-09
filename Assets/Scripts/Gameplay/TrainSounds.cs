using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainSounds : MonoBehaviour
{
    public static TrainSounds Instance;

    public AudioSource brakeNoise;

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
}
