#region

using UnityEngine;

#endregion

// Cartoon FX  - (c) 2015 Jean Moreno
//
// Script handling looped effect in the Demo Scene, so that they eventually stop

[RequireComponent(typeof(ParticleSystem))]
public class CFX_AutoStopLoopedEffect : MonoBehaviour
{
    public float effectDuration = 2.5f;
    float d;

    void Update()
    {
        if (d > 0)
        {
            d -= Time.deltaTime;
            if (d <= 0)
            {
                GetComponent<ParticleSystem>().Stop(true);

                CFX_Demo_Translate translation = gameObject.GetComponent<CFX_Demo_Translate>();
                if (translation != null)
                {
                    translation.enabled = false;
                }
            }
        }
    }

    void OnEnable()
    {
        d = effectDuration;
    }
}