using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainSounds : MonoBehaviour
{
    public static TrainSounds Instance;

    public AudioSource brakeNoise;

    void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void PlayBrakeNoise()
    {
        brakeNoise.Play();
    }
}
