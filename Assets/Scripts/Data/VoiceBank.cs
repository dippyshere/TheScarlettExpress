using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "VoiceBank", menuName = "Dialogue/VoiceBank")]
public class VoiceBank : ScriptableObject
{
    public AudioClip a;
    public AudioClip b;
    public AudioClip c;
    public AudioClip d;
    public AudioClip e;
    public AudioClip f;
    public AudioClip g;
    public AudioClip h;
    public AudioClip i;
    public AudioClip j;
    public AudioClip k;
    public AudioClip l;
    public AudioClip m;
    public AudioClip n;
    public AudioClip o;
    public AudioClip p;
    public AudioClip q;
    public AudioClip r;
    public AudioClip s;
    public AudioClip t;
    public AudioClip u;
    public AudioClip v;
    public AudioClip w;
    public AudioClip x;
    public AudioClip y;
    public AudioClip z;
    public float volume = 1.0f;
    public float pitch = 1.0f;
    
    public AudioClip GetClip(char character)
    {
        return character switch
        {
            'a' => a,
            'b' => b,
            'c' => c,
            'd' => d,
            'e' => e,
            'f' => f,
            'g' => g,
            'h' => h,
            'i' => i,
            'j' => j,
            'k' => k,
            'l' => l,
            'm' => m,
            'n' => n,
            'o' => o,
            'p' => p,
            'q' => q,
            'r' => r,
            's' => s,
            't' => t,
            'u' => u,
            'v' => v,
            'w' => w,
            'x' => x,
            'y' => y,
            'z' => z,
            _ => null
        };
    }
}
