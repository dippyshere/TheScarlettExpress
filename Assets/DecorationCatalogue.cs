using Beautify.Demos;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecorationCatalogue : MonoBehaviour
{
    public GameObject deco1Lock;
    public GameObject deco2Lock;
    public GameObject deco3Lock;
    public GameObject deco4Lock;
    public GameObject deco5Lock;

    // Start is called before the first frame update
    void Start()
    {
        int deco1 = ProfileSystem.Get<int>(ProfileSystem.Variable.Deco1);
        if (deco1 == 1)
        {
            deco1Lock.SetActive(false);
        }

        int deco2 = ProfileSystem.Get<int>(ProfileSystem.Variable.Deco2);
        if (deco2 == 1)
        {
            deco2Lock.SetActive(false);
        }

        int deco3 = ProfileSystem.Get<int>(ProfileSystem.Variable.Deco3);
        if (deco3 == 1)
        {
            deco3Lock.SetActive(false);
        }

        int deco4 = ProfileSystem.Get<int>(ProfileSystem.Variable.Deco4);
        if (deco4 == 1)
        {
            deco4Lock.SetActive(false);
        }

        int deco5 = ProfileSystem.Get<int>(ProfileSystem.Variable.Deco5);
        if (deco5 == 1)
        {
            deco5Lock.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
