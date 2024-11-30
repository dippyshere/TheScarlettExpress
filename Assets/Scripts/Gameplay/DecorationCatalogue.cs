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
    public GameObject deco11Lock;
    public GameObject deco12Lock;
    public GameObject deco13Lock;
    public GameObject deco14Lock;
    public GameObject deco15Lock;

    public GameObject deco10ALock;
    public GameObject deco1ALock;
    public GameObject deco2ALock;
    public GameObject deco3ALock;
    public GameObject deco4ALock;
    public GameObject deco5ALock;
    public GameObject deco6ALock;
    public GameObject deco7ALock;
    public GameObject deco8ALock;
    public GameObject deco9ALock;
    public GameObject deco11ALock;
    public GameObject deco12ALock;
    public GameObject deco13ALock;
    public GameObject deco14ALock;
    public GameObject deco15ALock;

    public GameObject deco16Lock;
    public GameObject deco17Lock;
    public GameObject deco18Lock;
    public GameObject deco19Lock;
    public GameObject deco20Lock;
    public GameObject deco21Lock;
    public GameObject deco22Lock;
    public GameObject deco23Lock;
    public GameObject deco24Lock;       
    public GameObject deco25Lock;
    public GameObject deco26Lock;
    public GameObject deco27Lock;
    public GameObject deco28Lock;
    public GameObject deco29Lock;
    public GameObject deco30Lock;


    // Start is called before the first frame update
    void Start()
    {
        int deco1 = ProfileSystem.Get<int>(ProfileSystem.Variable.Deco1);
        if (deco1 == 1)
        {
            deco1Lock.SetActive(false);
            deco1ALock.SetActive(false);
        }

        int deco2 = ProfileSystem.Get<int>(ProfileSystem.Variable.Deco2);
        if (deco2 == 1)
        {
            deco2Lock.SetActive(false);
            deco2ALock.SetActive(false);
        }

        int deco3 = ProfileSystem.Get<int>(ProfileSystem.Variable.Deco3);
        if (deco3 == 1)
        {
            deco3Lock.SetActive(false);
            deco3ALock.SetActive(false);
        }

        int deco4 = ProfileSystem.Get<int>(ProfileSystem.Variable.Deco4);
        if (deco4 == 1)
        {
            deco4Lock.SetActive(false);
            deco4ALock.SetActive(false);
        }

        int deco5 = ProfileSystem.Get<int>(ProfileSystem.Variable.Deco5);
        if (deco5 == 1)
        {
            deco5Lock.SetActive(false);
            deco5ALock.SetActive(false);
        }

        int deco6 = ProfileSystem.Get<int>(ProfileSystem.Variable.Deco6);
        if (deco6 == 1)
        {
            deco6Lock.SetActive(false);
            deco6ALock.SetActive(false);
        }

        int deco7 = ProfileSystem.Get<int>(ProfileSystem.Variable.Deco7);
        if (deco7 == 1)
        {
            deco7Lock.SetActive(false);
            deco7ALock.SetActive(false);
        }

        int deco8 = ProfileSystem.Get<int>(ProfileSystem.Variable.Deco8);
        if (deco8 == 1)
        {
            deco8Lock.SetActive(false);
            deco8ALock.SetActive(false);
        }

        int deco9 = ProfileSystem.Get<int>(ProfileSystem.Variable.Deco9);
        if (deco9 == 1)
        {
            deco9Lock.SetActive(false);
            deco9ALock.SetActive(false);
        }

        int deco10 = ProfileSystem.Get<int>(ProfileSystem.Variable.Deco10);
        if (deco10 == 1)
        {
            deco10Lock.SetActive(false);
            deco10ALock.SetActive(false);
        }

        int deco11 = ProfileSystem.Get<int>(ProfileSystem.Variable.Deco11);
        if (deco11 == 1)
        {
            deco11Lock.SetActive(false);
            deco11ALock.SetActive(false);
        }

        int deco12 = ProfileSystem.Get<int>(ProfileSystem.Variable.Deco12);
        if (deco12 == 1)
        {
            deco12Lock.SetActive(false);
            deco12ALock.SetActive(false);
        }

        int deco13 = ProfileSystem.Get<int>(ProfileSystem.Variable.Deco13);
        if (deco13 == 1)
        {
            deco13Lock.SetActive(false);
            deco13ALock.SetActive(false);
        }

        int deco14 = ProfileSystem.Get<int>(ProfileSystem.Variable.Deco14);
        if (deco14 == 1)
        {
            deco14Lock.SetActive(false);
            deco14ALock.SetActive(false);
        }

        int deco15 = ProfileSystem.Get<int>(ProfileSystem.Variable.Deco15);
        if (deco15 == 1)
        {
            deco15Lock.SetActive(false);
            deco15ALock.SetActive(false);
        }

        int deco16 = ProfileSystem.Get<int>(ProfileSystem.Variable.Deco16);
        if (deco16 == 1)
        {
            deco16Lock.SetActive(false);
        }

        int deco17 = ProfileSystem.Get<int>(ProfileSystem.Variable.Deco17);
        if (deco17 == 1)
        {
            deco17Lock.SetActive(false);
        }

        int deco18 = ProfileSystem.Get<int>(ProfileSystem.Variable.Deco18);
        if (deco18 == 1)
        {
            deco18Lock.SetActive(false);
        }

        int deco19 = ProfileSystem.Get<int>(ProfileSystem.Variable.Deco19);
        if (deco19 == 1)
        {
            deco19Lock.SetActive(false);
        }

        int deco20 = ProfileSystem.Get<int>(ProfileSystem.Variable.Deco20);
        if (deco20 == 1)
        {
            deco20Lock.SetActive(false);
        }

        int deco21 = ProfileSystem.Get<int>(ProfileSystem.Variable.Deco21);
        if (deco21 == 1)
        {
            deco21Lock.SetActive(false);
        }

        int deco22 = ProfileSystem.Get<int>(ProfileSystem.Variable.Deco22);
        if (deco22 == 1)
        { 
            deco22Lock.SetActive(false);
        }

        int deco23 = ProfileSystem.Get<int>(ProfileSystem.Variable.Deco23);
        if (deco23 == 1)
        {
            deco23Lock.SetActive(false);
        }

        int deco24 = ProfileSystem.Get<int>(ProfileSystem.Variable.Deco24);
        if (deco24 == 1)
        {
            deco24Lock.SetActive(false);
        }

        int deco25 = ProfileSystem.Get<int>(ProfileSystem.Variable.Deco25);
        if (deco25 == 1)
        {
            deco25Lock.SetActive(false);
        }

        int deco26 = ProfileSystem.Get<int>(ProfileSystem.Variable.Deco26);
        if (deco26 == 1)
        {
            deco26Lock.SetActive(false);
        }

        int deco27 = ProfileSystem.Get<int>(ProfileSystem.Variable.Deco27);
        if (deco27 == 1)
        {
            deco27Lock.SetActive(false);
        }

        int deco28 = ProfileSystem.Get<int>(ProfileSystem.Variable.Deco28);
        if (deco28 == 1)
        {
            deco28Lock.SetActive(false);
        }

        int deco29 = ProfileSystem.Get<int>(ProfileSystem.Variable.Deco29);
        if (deco29 == 1)
        {
            deco29Lock.SetActive(false);
        }

        int deco30 = ProfileSystem.Get<int>(ProfileSystem.Variable.Deco30);
        if (deco30 == 1)
        {
            deco30Lock.SetActive(false);
        }

       
    }
}