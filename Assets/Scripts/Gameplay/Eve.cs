using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eve : MonoBehaviour
{
    public GameObject eveDrawing;
    public GameObject shopEve;
    public GameObject panelDialogue;
    public GameObject panelOptions;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowPainting()
    {
        eveDrawing.SetActive(true);
    }

    public void HidePainting()
    {
        eveDrawing.SetActive(false);
    }

    public void ActivateShopEve()
    {
        shopEve.SetActive(true);
        panelDialogue.SetActive(false);
        panelOptions.SetActive(false);
    }
}
