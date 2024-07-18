using Dypsloom.DypThePenguin.Scripts.Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideviewManager : MonoBehaviour
{
    public GameObject sideviewCamera;
    public GameObject carriageSelectionUI;
    public GameObject sideviewWall;

    public GameObject carriage1UI;
    public GameObject carriage2UI;
    public GameObject carriage3UI;

    public GameObject carriage1Camera;
    public GameObject carriage2Camera;
    public GameObject carriage3Camera;

    public GameObject decorationUpgradeCanvas;
    public GameObject decorateCamera;

    [SerializeField, Tooltip("Reference to the player script.")]
    private Character m_Player;

    private void Update()
    {
        if (!sideviewCamera.activeSelf && Input.GetKeyDown(KeyCode.Tab))
        {
            Debug.Log("heyy???");
            //Cursor.lockState = CursorLockMode.None;
            //sideviewCamera.SetActive(true);
            //carriageSelectionUI.SetActive(true);
            Invoke(nameof(ActivateCarriageSelection), 2f);
            //sideviewWall.SetActive(false);

            Invoke(nameof(OpenSideviewMenu), 0.01f);
            m_Player.m_MovementMode = MovementMode.Decorating;
        }

        if (sideviewCamera.activeSelf && Input.GetKeyDown(KeyCode.Tab))
        {
            Cursor.lockState = CursorLockMode.Locked;
            sideviewCamera.SetActive(false);
            carriageSelectionUI.SetActive(false);
            sideviewWall.SetActive(true);

            carriage1Camera.SetActive(false);
            carriage2Camera.SetActive(false);
            carriage3Camera.SetActive(false);

            carriage1UI.SetActive(false);
            carriage2UI.SetActive(false);
            carriage3UI.SetActive(false);
            m_Player.m_MovementMode = MovementMode.Free;

        }

        if (decorationUpgradeCanvas.activeSelf && Input.GetKeyDown(KeyCode.Tab))
        {
            decorationUpgradeCanvas.SetActive(false);
        }
    }

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
        Invoke(nameof(ActivateCarriageSelection), 2f);

        //carriageSelectionUI.SetActive(true);
        sideviewCamera.SetActive(true);

        carriage1Camera.SetActive(false);
        carriage2Camera.SetActive(false);
        carriage3Camera.SetActive(false);

        carriage1UI.SetActive(false);
        carriage2UI.SetActive(false);
        carriage3UI.SetActive(false);
    }

    private void ActivateCarriageSelection()
    {
        carriageSelectionUI.SetActive(true);

    }

    private void OpenSideviewMenu()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        sideviewCamera.SetActive(true);
        sideviewWall.SetActive(false);
    }

    public void Decorate()
    {
        decorationUpgradeCanvas.SetActive(true);
        decorateCamera.SetActive(true);
        sideviewWall.SetActive(true);

        carriage1Camera.SetActive(false);
        carriage2Camera.SetActive(false);
        carriage3Camera.SetActive(false);

        carriage1UI.SetActive(false);
        carriage2UI.SetActive(false);
        carriage3UI.SetActive(false);
    }

    public void Go1()
    {

    }

    public void HelpPlease()
    {
        decorationUpgradeCanvas.SetActive(false);
    }
}
