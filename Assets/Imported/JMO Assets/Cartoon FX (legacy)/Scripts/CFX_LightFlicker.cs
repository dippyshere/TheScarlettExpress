#region

using UnityEngine;

#endregion

// Cartoon FX  - (c) 2015 Jean Moreno

// Randomly changes a light's intensity over time.

[RequireComponent(typeof(Light))]
public class CFX_LightFlicker : MonoBehaviour
{
    // Loop flicker effect
    public bool loop;

    // Perlin scale: makes the flicker more or less smooth
    public float smoothFactor = 1f;

    /// Max intensity will be: baseIntensity + addIntensity
    public float addIntensity = 1.0f;

    float baseIntensity;
    float maxIntensity;

    float minIntensity;

    void Awake()
    {
        baseIntensity = GetComponent<Light>().intensity;
    }

    void Update()
    {
        GetComponent<Light>().intensity =
            Mathf.Lerp(minIntensity, maxIntensity, Mathf.PerlinNoise(Time.time * smoothFactor, 0f));
    }

    void OnEnable()
    {
        minIntensity = baseIntensity;
        maxIntensity = minIntensity + addIntensity;
    }
}