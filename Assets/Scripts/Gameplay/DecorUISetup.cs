using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecorUISetup : MonoBehaviour
{

    public GameObject deco1;
    public GameObject deco2;
    public GameObject deco3;
    public GameObject deco4;
    public GameObject deco5;
    public GameObject deco6;
    public GameObject deco7;
    public GameObject deco8;
    public GameObject deco9;
    public GameObject deco10;


    private void Start()
    {
        deco1.SetActive(false); 
        deco2.SetActive(false);
        deco3.SetActive(false);
        deco4.SetActive(false);
        deco5.SetActive(false);
        deco6.SetActive(false);
        deco7.SetActive(false);
        deco8.SetActive(false);
        deco9.SetActive(false);
        deco10.SetActive(false);    
    }

    public void Deco1()
    {
        deco1.SetActive(true);
        deco2.SetActive(false);
        deco3.SetActive(false);
        deco4.SetActive(false);
        deco5.SetActive(false);
        deco6.SetActive(false);
        deco7.SetActive(false);
        deco8.SetActive(false);
        deco9.SetActive(false);
        deco10.SetActive(false);       
    }
    public void Deco2()
    {
        deco1.SetActive(false);
        deco2.SetActive(true);
        deco3.SetActive(false);
        deco4.SetActive(false);
        deco5.SetActive(false);
        deco6.SetActive(false);
        deco7.SetActive(false);
        deco8.SetActive(false);
        deco9.SetActive(false);
        deco10.SetActive(false);
    }
    public void Deco3()
    {
        deco1.SetActive(false);
        deco2.SetActive(false);
        deco3.SetActive(true);
        deco4.SetActive(false);
        deco5.SetActive(false);
        deco6.SetActive(false);
        deco7.SetActive(false);
        deco8.SetActive(false);
        deco9.SetActive(false);
        deco10.SetActive(false);
    }
    public void Deco4()
    {
        deco1.SetActive(false);
        deco2.SetActive(false);
        deco3.SetActive(false);
        deco4.SetActive(true);
        deco5.SetActive(false);
        deco6.SetActive(false);
        deco7.SetActive(false);
        deco8.SetActive(false);
        deco9.SetActive(false);
        deco10.SetActive(false);
    }
    public void Deco5()
    {
        deco1.SetActive(false);
        deco2.SetActive(false);
        deco3.SetActive(false);
        deco4.SetActive(false);
        deco5.SetActive(true);
        deco6.SetActive(false);
        deco7.SetActive(false);
        deco8.SetActive(false);
        deco9.SetActive(false);
        deco10.SetActive(false);
    }
    public void Deco6()
    {
        deco1.SetActive(false);
        deco2.SetActive(false);
        deco3.SetActive(false);
        deco4.SetActive(false);
        deco5.SetActive(false);
        deco6.SetActive(true);
        deco7.SetActive(false);
        deco8.SetActive(false);
        deco9.SetActive(false);
        deco10.SetActive(false);
    }
    public void Deco7()
    {
        deco1.SetActive(false);
        deco2.SetActive(false);
        deco3.SetActive(false);
        deco4.SetActive(false);
        deco5.SetActive(false);
        deco6.SetActive(false);
        deco7.SetActive(true);
        deco8.SetActive(false);
        deco9.SetActive(false);
        deco10.SetActive(false);
    }
    public void Deco8()
    {
        deco1.SetActive(false);
        deco2.SetActive(false);
        deco3.SetActive(false);
        deco4.SetActive(false);
        deco5.SetActive(false);
        deco6.SetActive(false);
        deco7.SetActive(false);
        deco8.SetActive(true);
        deco9.SetActive(false);
        deco10.SetActive(false);
    }
    public void Deco9()
    {
        deco1.SetActive(false);
        deco2.SetActive(false);
        deco3.SetActive(false);
        deco4.SetActive(false);
        deco5.SetActive(false);
        deco6.SetActive(false);
        deco7.SetActive(false);
        deco8.SetActive(false);
        deco9.SetActive(true);
        deco10.SetActive(false);
    }
    public void Deco10()
    {
        deco1.SetActive(false);
        deco2.SetActive(false);
        deco3.SetActive(false);
        deco4.SetActive(false);
        deco5.SetActive(false);
        deco6.SetActive(false);
        deco7.SetActive(false);
        deco8.SetActive(false);
        deco9.SetActive(false);
        deco10.SetActive(true);
    }
}
