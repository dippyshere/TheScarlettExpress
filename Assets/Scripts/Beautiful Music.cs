using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeautifulMusic : MonoBehaviour

{
    private int randoNumb;

    public AudioSource audio1;
    public AudioSource audio2;
    public AudioSource audio3;


    void Start()
    {

        randoNumb = Random.Range(1, 4);
    
if (randoNumb == 1)
        {
            audio1.Play();
            Debug.Log("Forest");
        }
        if (randoNumb == 2)
        {
            audio2.Play();
            Debug.Log("Desert");
        }
        if (randoNumb >= 3)
        {
            audio3.Play();
            Debug.Log("Snow");
        }


    }
}
