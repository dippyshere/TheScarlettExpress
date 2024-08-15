using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveSelect : MonoBehaviour
{
    [SerializeField] GameObject Select1;
    [SerializeField] GameObject Select2;
    [SerializeField] GameObject Select3;
    [SerializeField] GameObject Select4;

    [SerializeField] GameObject Select5;
    [SerializeField] GameObject Select6;
    [SerializeField] GameObject Select7;
    [SerializeField] GameObject Select8;

    [SerializeField] GameObject Select9;
    [SerializeField] GameObject Select10;
    [SerializeField] GameObject Select11;
    [SerializeField] GameObject Select12;

    [SerializeField] GameObject Select13;

    public void Select1GO()
    {
        Select1.SetActive(true);
        Select2.SetActive(false);
        Select3.SetActive(false);
        Select4.SetActive(false);
    }
       
    public void Select2GO()
    {
        Select1.SetActive(false); 
        Select2.SetActive(true);
        Select3.SetActive(false);
        Select4.SetActive(false);
    }
    
    public void Select3GO()
    {
        Select1.SetActive(false);
        Select2.SetActive(false);
        Select3.SetActive(true);
        Select4.SetActive(false);
    }

    public void Select4GO()
    {
        Select1.SetActive(false);
        Select2.SetActive(false);
        Select3.SetActive(false);
        Select4.SetActive(true);
    }

    public void Select5GO()
    {
        Select5.SetActive(true);
        Select6.SetActive(false);
        Select7.SetActive(false);
        Select8.SetActive(false);
    }

    public void Select6GO()
    {
        Select5.SetActive(false);
        Select6.SetActive(true);
        Select7.SetActive(false);
        Select8.SetActive(false);
    }

    public void Select7GO()
    {
        Select5.SetActive(false);
        Select6.SetActive(false);
        Select7.SetActive(true);
        Select8.SetActive(false);
    }

    public void Select8GO()
    {
        Select5.SetActive(false);
        Select6.SetActive(false);
        Select7.SetActive(false);
        Select8.SetActive(true);
    }

    public void Select9GO()
    {
        Select9.SetActive(true);
        Select10.SetActive(false);
        Select11.SetActive(false);
        Select12.SetActive(false);
    }

    public void Select10GO()
    {
        Select9.SetActive(false);
        Select10.SetActive(true);
        Select11.SetActive(false);
        Select12.SetActive(false);
    }

    public void Select11GO()
    {
        Select9.SetActive(false);
        Select10.SetActive(false);
        Select11.SetActive(true);
        Select12.SetActive(false);
    }

    public void Select12GO()
    {
        Select9.SetActive(false);
        Select10.SetActive(false);
        Select11.SetActive(false);
        Select12.SetActive(true);
    }

    public void Select13GO()
    {
        Select13.SetActive(true);
    }


    public void Close()
    {
        Select1.SetActive(false);
        Select2.SetActive(false);
        Select3.SetActive(false);
        Select4.SetActive(false);
        Select5.SetActive(false);
        Select6.SetActive(false);
        Select7.SetActive(false);
        Select8.SetActive(false);
        Select9.SetActive(false);
        Select10.SetActive(false);
        Select11.SetActive(false);
        Select12.SetActive(false);
        Select13.SetActive(false);

    }
    

}
