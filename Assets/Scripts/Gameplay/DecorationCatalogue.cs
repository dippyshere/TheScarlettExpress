#region

using UnityEngine;

#endregion

public class DecorationCatalogue : MonoBehaviour
{
    public GameObject deco10Lock;
    public GameObject deco1Lock;
    public GameObject deco2Lock;
    public GameObject deco3Lock;
    public GameObject deco4Lock;
    public GameObject deco5Lock;

    public GameObject deco6Lock;
    public GameObject deco7Lock;
    public GameObject deco8Lock;
    public GameObject deco9Lock;


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

        int deco6 = ProfileSystem.Get<int>(ProfileSystem.Variable.Deco6);
        if (deco6 == 1)
        {
            deco6Lock.SetActive(false);
        }

        int deco7 = ProfileSystem.Get<int>(ProfileSystem.Variable.Deco7);
        if (deco7 == 1)
        {
            deco7Lock.SetActive(false);
        }

        int deco8 = ProfileSystem.Get<int>(ProfileSystem.Variable.Deco8);
        if (deco8 == 1)
        {
            deco8Lock.SetActive(false);
        }

        int deco9 = ProfileSystem.Get<int>(ProfileSystem.Variable.Deco9);
        if (deco9 == 1)
        {
            deco9Lock.SetActive(false);
        }

        int deco10 = ProfileSystem.Get<int>(ProfileSystem.Variable.Deco10);
        if (deco10 == 1)
        {
            deco10Lock.SetActive(false);
        }
    }
}