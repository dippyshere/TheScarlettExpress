using Dypsloom.DypThePenguin.Scripts.Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Cinemachine;

public class SideviewManager : MonoBehaviour
{
    public GameObject sideviewCamera;
    public GameObject carriageSelectionUI;
    public GameObject[] sideviewWalls;

    public GameObject kitchenCarriageUI;
    public GameObject carriage1UI;
    public GameObject carriage2UI;
    public GameObject carriage3UI;

    public GameObject kitchenCarriageCamera;
    public GameObject carriage1Camera;
    public GameObject carriage2Camera;
    public GameObject carriage3Camera;

    public GameObject decorationUpgradeCanvas;
    public GameObject decorateCamera;

    [SerializeField, Tooltip("Reference to the player script.")]
    private Character m_Player;

    [SerializeField, Tooltip("Reference to the cinemachine input manager.")]
    private CinemachineInputAxisController m_CinemachineInputAxisController;

    public GameObject player;
    public CharacterController characterController;
    public Transform kitchenCarriageGo;
    public Transform carriage1Go;
    public Transform carriage2Go;
    public Transform carriage3Go;

    public GameObject sideviewButton;
    public GameObject sterlingButton;

    [SerializeField] private GameObject clipboardUI;

    [SerializeField] private GameObject clipboard;

    public GameObject decrepitObjects;
    public GameObject renovationParticles;
    public float money;

    public AudioSource musicR;

    public ClipboardManager clipboardManager;

    void Start()
    {
        money = ProfileSystem.Get<float>(ProfileSystem.Variable.PlayerMoney);
        
    }

    private void Update()
    {
        money = ProfileSystem.Get<float>(ProfileSystem.Variable.PlayerMoney);

        //if (!sideviewCamera.activeSelf && Input.GetKeyDown(KeyCode.Tab))
        //{
        //    Debug.Log("heyy???");
        //    //Cursor.lockState = CursorLockMode.None;
        //    //sideviewCamera.SetActive(true);
        //    //carriageSelectionUI.SetActive(true);
        //    Invoke(nameof(ActivateCarriageSelection), 2f);
        //    //sideviewWall.SetActive(false);

        //    Invoke(nameof(OpenSideviewMenu), 0.01f);
        //    m_Player.m_MovementMode = MovementMode.Decorating;
        //    m_CinemachineInputAxisController.enabled = false;
        //    Cursor.lockState = CursorLockMode.None;
        //    Cursor.visible = true;
        //}

        //if (sideviewCamera.activeSelf && Input.GetKeyDown(KeyCode.Tab))
        //{
        //    Cursor.lockState = CursorLockMode.Locked;
        //    Cursor.visible = false;
        //    m_CinemachineInputAxisController.enabled = true;
        //    sideviewCamera.SetActive(false);
        //    carriageSelectionUI.SetActive(false);
        //    sideviewWall.SetActive(true);

        //    carriage1Camera.SetActive(false);
        //    carriage2Camera.SetActive(false);
        //    carriage3Camera.SetActive(false);

        //    carriage1UI.SetActive(false);
        //    carriage2UI.SetActive(false);
        //    carriage3UI.SetActive(false);
        //    m_Player.m_MovementMode = MovementMode.Free;

        //}

        //if (decorationUpgradeCanvas.activeSelf && Input.GetKeyDown(KeyCode.Tab))
        //{
        //    decorationUpgradeCanvas.SetActive(false);
        //}
    }

    public void SideViewButton()
    {
        if (!sideviewCamera.activeSelf)
        {
            //Debug.Log("heyy???");
            //Cursor.lockState = CursorLockMode.None;
            //sideviewCamera.SetActive(true);
            //carriageSelectionUI.SetActive(true);
            Invoke(nameof(ActivateCarriageSelection), 2f);
            //sideviewWall.SetActive(false);

            Invoke(nameof(OpenSideviewMenu), 0.01f);
            m_Player.m_MovementMode = MovementMode.Decorating;
            m_CinemachineInputAxisController.enabled = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            clipboard.GetComponent<ClipboardManager>().canClipboard = false;
        }

        if (sideviewCamera.activeSelf)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            m_CinemachineInputAxisController.enabled = true;
            sideviewCamera.SetActive(false);
            carriageSelectionUI.SetActive(false);
            foreach (GameObject wall in sideviewWalls)
            {
                wall.SetActive(true);
            }

            kitchenCarriageCamera.SetActive(false);
            carriage1Camera.SetActive(false);
            carriage2Camera.SetActive(false);
            carriage3Camera.SetActive(false);

            kitchenCarriageUI.SetActive(false);
            carriage1UI.SetActive(false);
            carriage2UI.SetActive(false);
            carriage3UI.SetActive(false);
            m_Player.m_MovementMode = MovementMode.Free;
            clipboard.GetComponent<ClipboardManager>().canClipboard = true;

        }

        if (decorationUpgradeCanvas != null && decorationUpgradeCanvas.activeSelf)
        {
            decorationUpgradeCanvas.SetActive(false);
        }

        clipboardUI.SetActive(false);
    }

    public void KitchenCarriage()
    {
        carriageSelectionUI.SetActive(false);
        sideviewCamera.SetActive(false);
        kitchenCarriageCamera.SetActive(true);
        kitchenCarriageUI.SetActive(true);
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

    public void BackToSideview()
    {
        Invoke(nameof(ActivateCarriageSelection), 2f);

        //carriageSelectionUI.SetActive(true);
        sideviewCamera.SetActive(true);
        carriageSelectionUI.SetActive(true);

        kitchenCarriageCamera.SetActive(false);
        carriage1Camera.SetActive(false);
        carriage2Camera.SetActive(false);
        carriage3Camera.SetActive(false);

        kitchenCarriageUI.SetActive(false);
        carriage1UI.SetActive(false);
        carriage2UI.SetActive(false);
        carriage3UI.SetActive(false);

        if (sideviewButton != null && sterlingButton != null)
        {
            sideviewButton.SetActive(false);
            sterlingButton.SetActive(false);
        }

        foreach (GameObject wall in sideviewWalls)
        {
            wall.SetActive(false);
        }

        if (decorationUpgradeCanvas != null && decorationUpgradeCanvas.activeSelf)
        {
            decorationUpgradeCanvas.SetActive(false);
            decorateCamera.SetActive(false);
        }
    }

    public void BackToSterling()
    {
        sideviewCamera.SetActive(false);
        kitchenCarriageCamera.SetActive(false);
        carriage1Camera.SetActive(false);
        carriage2Camera.SetActive(false);
        carriage3Camera.SetActive(false);

        kitchenCarriageUI.SetActive(false);
        carriage1UI.SetActive(false);
        carriage2UI.SetActive(false);
        carriage3UI.SetActive(false);

        carriageSelectionUI.SetActive(false);
        foreach (GameObject wall in sideviewWalls)
        {
            wall.SetActive(true);
        }


        m_Player.m_MovementMode = MovementMode.Free;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        m_CinemachineInputAxisController.enabled = true;

        if (sideviewButton != null && sterlingButton != null)
        {
            sideviewButton.SetActive(false);
            sterlingButton.SetActive(false);
        }
        clipboard.GetComponent<ClipboardManager>().canClipboard = true;

        if (decorationUpgradeCanvas != null && decorationUpgradeCanvas.activeSelf)
        {
            decorationUpgradeCanvas.SetActive(false);
            decorateCamera.SetActive(false);
        }
    }

    private void ActivateCarriageSelection()
    {
        carriageSelectionUI.SetActive(true);

    }

    private void OpenSideviewMenu()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        m_CinemachineInputAxisController.enabled = false;
        sideviewCamera.SetActive(true);
        foreach (GameObject wall in sideviewWalls)
        {
            wall.SetActive(false);
        }
    }

    public void Decorate()
    {
        if (decorationUpgradeCanvas != null && decorationUpgradeCanvas.activeSelf)
        {
            decorationUpgradeCanvas.SetActive(true);
            decorateCamera.SetActive(true);
        }
        foreach (GameObject wall in sideviewWalls)
        {
            wall.SetActive(true);
        }

        kitchenCarriageCamera.SetActive(false);
        carriage1Camera.SetActive(false);
        carriage2Camera.SetActive(false);
        carriage3Camera.SetActive(false);

        kitchenCarriageUI.SetActive(false);
        carriage1UI.SetActive(false);
        carriage2UI.SetActive(false);
        carriage3UI.SetActive(false);

        if (sideviewButton != null && sterlingButton != null)
        {
            sideviewButton.SetActive(true);
            sterlingButton.SetActive(true);
        }
    }

    public void Go0()
    {
        m_Player.m_MovementMode = MovementMode.Free;
        StartCoroutine(LateTeleport(kitchenCarriageGo));

        sideviewCamera.SetActive(false);
        kitchenCarriageCamera.SetActive(false);
        carriage1Camera.SetActive(false);
        carriage2Camera.SetActive(false);
        carriage3Camera.SetActive(false);

        kitchenCarriageUI.SetActive(false);
        carriage1UI.SetActive(false);
        carriage2UI.SetActive(false);
        carriage3UI.SetActive(false);

        carriageSelectionUI.SetActive(false);
        foreach (GameObject wall in sideviewWalls)
        {
            wall.SetActive(true);
        }

        if (decorationUpgradeCanvas != null && decorationUpgradeCanvas.activeSelf)
        {
            decorationUpgradeCanvas.SetActive(false);
            decorateCamera.SetActive(false);
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        m_CinemachineInputAxisController.enabled = true;
        clipboardManager.canClipboard = true;
    }

    public void Go1()
    {
        m_Player.m_MovementMode = MovementMode.Free;
        StartCoroutine(LateTeleport(carriage1Go));

        sideviewCamera.SetActive(false);
        kitchenCarriageCamera.SetActive(false);
        carriage1Camera.SetActive(false);
        carriage2Camera.SetActive(false);
        carriage3Camera.SetActive(false);

        kitchenCarriageUI.SetActive(false);
        carriage1UI.SetActive(false);
        carriage2UI.SetActive(false);
        carriage3UI.SetActive(false);

        carriageSelectionUI.SetActive(false);
        foreach (GameObject wall in sideviewWalls)
        {
            wall.SetActive(true);
        }

        if (decorationUpgradeCanvas != null && decorationUpgradeCanvas.activeSelf)
        {
            decorationUpgradeCanvas.SetActive(false);
            decorateCamera.SetActive(false);
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        m_CinemachineInputAxisController.enabled = true;
        clipboardManager.canClipboard = true;
    }

    public void Go2()
    {
        m_Player.m_MovementMode = MovementMode.Free;
        StartCoroutine(LateTeleport(carriage2Go));

        sideviewCamera.SetActive(false);
        kitchenCarriageCamera.SetActive(false);
        carriage1Camera.SetActive(false);
        carriage2Camera.SetActive(false);
        carriage3Camera.SetActive(false);

        kitchenCarriageUI.SetActive(false);
        carriage1UI.SetActive(false);
        carriage2UI.SetActive(false);
        carriage3UI.SetActive(false);

        carriageSelectionUI.SetActive(false);
        foreach (GameObject wall in sideviewWalls)
        {
            wall.SetActive(true);
        }

        if (decorationUpgradeCanvas != null && decorationUpgradeCanvas.activeSelf)
        {
            decorationUpgradeCanvas.SetActive(false);
            decorateCamera.SetActive(false);
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        m_CinemachineInputAxisController.enabled = true;
        clipboardManager.canClipboard = true;
    }

    public void Go3()
    {
        m_Player.m_MovementMode = MovementMode.Free;
        StartCoroutine(LateTeleport(carriage3Go));

        sideviewCamera.SetActive(false);
        kitchenCarriageCamera.SetActive(false);
        carriage1Camera.SetActive(false);
        carriage2Camera.SetActive(false);
        carriage3Camera.SetActive(false);

        kitchenCarriageUI.SetActive(false);
        carriage1UI.SetActive(false);
        carriage2UI.SetActive(false);
        carriage3UI.SetActive(false);

        carriageSelectionUI.SetActive(false);
        foreach (GameObject wall in sideviewWalls)
        {
            wall.SetActive(true);
        }

        if (decorationUpgradeCanvas != null && decorationUpgradeCanvas.activeSelf)
        {
            decorationUpgradeCanvas.SetActive(false);
            decorateCamera.SetActive(false);
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        m_CinemachineInputAxisController.enabled = true;
        clipboardManager.canClipboard = true;
    }

    private IEnumerator LateTeleport(Transform transform)
    {
        characterController.enabled = false;
        characterController.transform.SetPositionAndRotation(transform.position, transform.rotation);
        yield return new WaitForEndOfFrame();
        characterController.enabled = true;
    }

    public void RenovateCarriage()
    {
        if (money >= 100)
        {
            musicR.Play();
            money -= 100;
            ProfileSystem.Set(ProfileSystem.Variable.PlayerMoney, money);

            decrepitObjects.SetActive(false);
            renovationParticles.SetActive(true);

            Invoke(nameof(BackToSterling), 1f);
            
        }
    }
}
