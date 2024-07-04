using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideviewManager : MonoBehaviour
{
    public GameObject sideviewCamera;
    public GameObject carriageSelectionUI;

    public GameObject carriage1UI;
    public GameObject carriage2UI;
    public GameObject carriage3UI;

    public GameObject carriage1Camera;
    public GameObject carriage2Camera;
    public GameObject carriage3Camera;

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Tab))
    //    {
    //        Cursor.lockState = CursorLockMode.None;
    //    }
    //}

    public void Carriage1()
    {
        carriageSelectionUI.SetActive(false);
        sideviewCamera.SetActive(false);
        carriage1Camera.SetActive(true);
        carriage1UI.SetActive(true);
    }

    public void Carriage2()
    {
        carriageSelectionUI.SetActive(false);
        sideviewCamera.SetActive(false);
        carriage2Camera.SetActive(true);
        carriage2UI.SetActive(true);
    }

    public void Carriage3()
    {
        carriageSelectionUI.SetActive(false);
        sideviewCamera.SetActive(false);
        carriage3Camera.SetActive(true);
        carriage3UI.SetActive(true);
    }

    public void Back()
    {
        carriageSelectionUI.SetActive(true);
        sideviewCamera.SetActive(true);

        carriage1Camera.SetActive(false);
        carriage2Camera.SetActive(false);
        carriage3Camera.SetActive(false);

        carriage1UI.SetActive(false);
        carriage2UI.SetActive(false);
        carriage3UI.SetActive(false);
    }
}
