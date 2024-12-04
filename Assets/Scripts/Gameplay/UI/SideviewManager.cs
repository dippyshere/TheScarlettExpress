#region

using System.Collections;
using Dypsloom.DypThePenguin.Scripts.Character;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;

#endregion

public class SideviewManager : MonoBehaviour
{
    public GameObject carriage1Camera;
    public Transform carriage1Go;
    public GameObject carriage1UI;
    public GameObject carriage2Camera;
    public Transform carriage2Go;
    public GameObject carriage2UI;
    public GameObject carriage3Camera;
    public Transform carriage3Go;
    public GameObject carriage3UI;
    public GameObject carriage4Camera;
    public Transform carriage4Go;
    public GameObject carriage4UI;
    public GameObject carriage5Camera;
    public Transform carriage5Go;
    public GameObject carriage5UI;
    [FormerlySerializedAs("carriageSelectionUI")] public GameObject carriageSelectionUI1;
    public GameObject carriageSelectionUI2;

    public GameObject decorateCamera;

    public GameObject decorationUpgradeCanvas;

    public GameObject decrepitObjects;

    public GameObject kitchenCarriageCamera;
    public Transform kitchenCarriageGo;

    public GameObject kitchenCarriageUI;

    public float money;

    public AudioSource musicR;

    public GameObject renovationParticles;

    public GameObject sideviewButton;
    public GameObject sideviewCamera;
    public GameObject sideviewCamera2;
    public GameObject[] sideviewWalls;
    public GameObject sterlingButton;

    public GameObject clipboardUI;
    public GameObject clipboardMainMenuUI;
    public GameObject clipboardUpgradesUI;
    public GameObject restaurant1Camera;
    public GameObject restaurant1UpgradeUI;
    public ClipboardManager clipboardManager;
    public GameObject restaurant2Camera;
    public GameObject restaurant2UpgradeUI;
    public bool isSideviewUpgrade;
    bool sideviewEnabled = false;

    void Start()
    {
        money = ProfileSystem.Get<float>(ProfileSystem.Variable.PlayerMoney);
    }

    void Update()
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
        if (!sideviewEnabled)
        {
            sideviewEnabled = true;
            ClipboardManager.Instance._isClipboardActive = false;
            //Debug.Log("heyy???");
            //Cursor.lockState = CursorLockMode.None;
            //sideviewCamera.SetActive(true);
            //carriageSelectionUI.SetActive(true);
            CancelInvoke(nameof(ActivateCarriageSelection));
            CancelInvoke(nameof(ActivateCarriageSelection2));
            Invoke(nameof(ActivateCarriageSelection), 2f);
            //sideviewWall.SetActive(false);

            Invoke(nameof(OpenSideviewMenu), 0.01f);
            CameraManager.Instance.SetInputModeUI();
        }

        if (sideviewEnabled)
        {
            CameraManager.Instance.SetInputModeGameplay();
            sideviewEnabled = false;
            sideviewCamera.SetActive(false);
            if (sideviewCamera2 != null)
            {
                sideviewCamera2.SetActive(false);
                carriageSelectionUI2.SetActive(false);
                carriage4Camera.SetActive(false);
                carriage5Camera.SetActive(false);
                carriage4UI.SetActive(false);
                carriage5UI.SetActive(false);
            }
            carriageSelectionUI1.SetActive(false);
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
        }

        if (decorationUpgradeCanvas != null && decorationUpgradeCanvas.activeSelf)
        {
            decorationUpgradeCanvas.SetActive(false);
        }

        ClipboardManager.Instance.clipboardUI.SetActive(false);
    }

    public void KitchenCarriage()
    {
        carriageSelectionUI1.SetActive(false);
        carriageSelectionUI2.SetActive(false);
        sideviewCamera.SetActive(false);
        sideviewCamera2.SetActive(false);
        kitchenCarriageCamera.SetActive(true);
        kitchenCarriageUI.SetActive(true);
    }

    public void Carriage1()
    {
        carriageSelectionUI1.SetActive(false);
        carriageSelectionUI2.SetActive(false);
        sideviewCamera.SetActive(false);
        sideviewCamera2.SetActive(false);
        carriage1Camera.SetActive(true);
        carriage1UI.SetActive(true);
    }

    public void Carriage2()
    {
        carriageSelectionUI1.SetActive(false);
        if (carriageSelectionUI2 != null)
            carriageSelectionUI2.SetActive(false);
        sideviewCamera.SetActive(false);
        if (sideviewCamera2 != null)
            sideviewCamera2.SetActive(false);
        carriage2Camera.SetActive(true);
        carriage2UI.SetActive(true);
    }

    public void Carriage3()
    {
        carriageSelectionUI1.SetActive(false);
        carriageSelectionUI2.SetActive(false);
        sideviewCamera.SetActive(false);
        sideviewCamera2.SetActive(false);
        carriage3Camera.SetActive(true);
        carriage3UI.SetActive(true);
    }
    
    public void Carriage4()
    {
        carriageSelectionUI1.SetActive(false);
        carriageSelectionUI2.SetActive(false);
        sideviewCamera.SetActive(false);
        sideviewCamera2.SetActive(false);
        carriage4Camera.SetActive(true);
        carriage4UI.SetActive(true);
    }
    
    public void Carriage5()
    {
        carriageSelectionUI1.SetActive(false);
        carriageSelectionUI2.SetActive(false);
        sideviewCamera.SetActive(false);
        sideviewCamera2.SetActive(false);
        carriage5Camera.SetActive(true);
        carriage5UI.SetActive(true);
    }

    public void BackToSideview()
    {
        CancelInvoke(nameof(ActivateCarriageSelection));
        CancelInvoke(nameof(ActivateCarriageSelection2));
        Invoke(nameof(ActivateCarriageSelection), 2f);

        //carriageSelectionUI.SetActive(true);
        sideviewEnabled = false;
        sideviewCamera.SetActive(true);
        if (sideviewCamera2 != null)
        {
            sideviewCamera2.SetActive(false);
            carriage4Camera.SetActive(false);
            carriage5Camera.SetActive(false);
            carriage4UI.SetActive(false);
            carriage5UI.SetActive(false);
            carriageSelectionUI2.SetActive(false);
        }
        carriageSelectionUI1.SetActive(true);

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

        if (decorationUpgradeCanvas != null)
        {
            decorationUpgradeCanvas.SetActive(false);
            decorateCamera.SetActive(false);
        }
    }
    
    public void BackToSideview2()
    {
        CancelInvoke(nameof(ActivateCarriageSelection));
        CancelInvoke(nameof(ActivateCarriageSelection2));
        Invoke(nameof(ActivateCarriageSelection2), 2f);

        //carriageSelectionUI.SetActive(true);
        sideviewCamera.SetActive(false);
        sideviewCamera2.SetActive(true);
        carriageSelectionUI1.SetActive(false);
        carriageSelectionUI2.SetActive(true);

        kitchenCarriageCamera.SetActive(false);
        carriage1Camera.SetActive(false);
        carriage2Camera.SetActive(false);
        carriage3Camera.SetActive(false);
        carriage4Camera.SetActive(false);
        carriage5Camera.SetActive(false);

        kitchenCarriageUI.SetActive(false);
        carriage1UI.SetActive(false);
        carriage2UI.SetActive(false);
        carriage3UI.SetActive(false);
        carriage4UI.SetActive(false);
        carriage5UI.SetActive(false);

        if (sideviewButton != null && sterlingButton != null)
        {
            sideviewButton.SetActive(false);
            sterlingButton.SetActive(false);
        }

        foreach (GameObject wall in sideviewWalls)
        {
            wall.SetActive(false);
        }

        if (decorationUpgradeCanvas != null)
        {
            decorationUpgradeCanvas.SetActive(false);
            decorateCamera.SetActive(false);
        }
    }

    public void BackToSterling()
    {
        sideviewCamera.SetActive(false);
        if (sideviewCamera2 != null)
        {
            sideviewCamera2.SetActive(false);
            carriage4Camera.SetActive(false);
            carriage5Camera.SetActive(false);
            carriage4UI.SetActive(false);
            carriage5UI.SetActive(false);
            carriageSelectionUI2.SetActive(false);
        }
        kitchenCarriageCamera.SetActive(false);
        carriage1Camera.SetActive(false);
        carriage2Camera.SetActive(false);
        carriage3Camera.SetActive(false);

        kitchenCarriageUI.SetActive(false);
        carriage1UI.SetActive(false);
        carriage2UI.SetActive(false);
        carriage3UI.SetActive(false);

        carriageSelectionUI1.SetActive(false);
        foreach (GameObject wall in sideviewWalls)
        {
            wall.SetActive(true);
        }
        
        CameraManager.Instance.SetInputModeGameplay();

        if (sideviewButton != null && sterlingButton != null)
        {
            sideviewButton.SetActive(false);
            sterlingButton.SetActive(false);
        }

        if (decorationUpgradeCanvas != null)
        {
            decorationUpgradeCanvas.SetActive(false);
            decorateCamera.SetActive(false);
        }
        
        ClipboardManager.Instance.tabButton.SetActive(true);
    }

    void ActivateCarriageSelection()
    {
        carriageSelectionUI1.SetActive(true);
        if (carriageSelectionUI2 != null)
            carriageSelectionUI2.SetActive(false);
    }
    
    void ActivateCarriageSelection2()
    {
        carriageSelectionUI1.SetActive(false);
        carriageSelectionUI2.SetActive(true);
    }

    void OpenSideviewMenu()
    {
        CameraManager.Instance.SetInputModeUI();
        sideviewCamera.SetActive(true);
        if (sideviewCamera2 != null)
        {
            sideviewCamera2.SetActive(false);
        }
        foreach (GameObject wall in sideviewWalls)
        {
            wall.SetActive(false);
        }
        sideviewEnabled = true;
    }

    public void Decorate()
    {
        if (decorationUpgradeCanvas != null)
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
        carriage4Camera.SetActive(false);
        carriage5Camera.SetActive(false);

        kitchenCarriageUI.SetActive(false);
        carriage1UI.SetActive(false);
        carriage2UI.SetActive(false);
        carriage3UI.SetActive(false);
        carriage4UI.SetActive(false);
        carriage5UI.SetActive(false);

        if (sideviewButton != null && sterlingButton != null)
        {
            sideviewButton.SetActive(true);
            sterlingButton.SetActive(true);
        }
    }

    public void Go0()
    {
        StartCoroutine(Character.Instance.LateTeleport(kitchenCarriageGo));

        sideviewEnabled = false;
        sideviewCamera.SetActive(false);
        sideviewCamera2.SetActive(false);
        kitchenCarriageCamera.SetActive(false);
        carriage1Camera.SetActive(false);
        carriage2Camera.SetActive(false);
        carriage3Camera.SetActive(false);
        carriage4Camera.SetActive(false);
        carriage5Camera.SetActive(false);

        kitchenCarriageUI.SetActive(false);
        carriage1UI.SetActive(false);
        carriage2UI.SetActive(false);
        carriage3UI.SetActive(false);
        carriage4UI.SetActive(false);
        carriage5UI.SetActive(false);

        carriageSelectionUI1.SetActive(false);
        carriageSelectionUI2.SetActive(false);
        foreach (GameObject wall in sideviewWalls)
        {
            wall.SetActive(true);
        }

        if (decorationUpgradeCanvas != null)
        {
            decorationUpgradeCanvas.SetActive(false);
            decorateCamera.SetActive(false);
        }

        CameraManager.Instance.SetInputModeGameplay();
    }

    public void Go1()
    {
        StartCoroutine(Character.Instance.LateTeleport(carriage1Go));

        sideviewEnabled = false;
        sideviewCamera.SetActive(false);
        sideviewCamera2.SetActive(false);
        kitchenCarriageCamera.SetActive(false);
        carriage1Camera.SetActive(false);
        carriage2Camera.SetActive(false);
        carriage3Camera.SetActive(false);
        carriage4Camera.SetActive(false);
        carriage5Camera.SetActive(false);

        kitchenCarriageUI.SetActive(false);
        carriage1UI.SetActive(false);
        carriage2UI.SetActive(false);
        carriage3UI.SetActive(false);
        carriage4UI.SetActive(false);
        carriage5UI.SetActive(false);

        carriageSelectionUI1.SetActive(false);
        carriageSelectionUI2.SetActive(false);
        foreach (GameObject wall in sideviewWalls)
        {
            wall.SetActive(true);
        }

        if (decorationUpgradeCanvas != null)
        {
            decorationUpgradeCanvas.SetActive(false);
            decorateCamera.SetActive(false);
        }

        CameraManager.Instance.SetInputModeGameplay();
    }

    public void Go2()
    {
        StartCoroutine(Character.Instance.LateTeleport(carriage2Go));

        sideviewEnabled = false;
        sideviewCamera.SetActive(false);
        sideviewCamera2.SetActive(false);
        kitchenCarriageCamera.SetActive(false);
        carriage1Camera.SetActive(false);
        carriage2Camera.SetActive(false);
        carriage3Camera.SetActive(false);
        carriage4Camera.SetActive(false);
        carriage5Camera.SetActive(false);

        kitchenCarriageUI.SetActive(false);
        carriage1UI.SetActive(false);
        carriage2UI.SetActive(false);
        carriage3UI.SetActive(false);
        carriage4UI.SetActive(false);
        carriage5UI.SetActive(false);

        carriageSelectionUI1.SetActive(false);
        carriageSelectionUI2.SetActive(false);
        foreach (GameObject wall in sideviewWalls)
        {
            wall.SetActive(true);
        }

        if (decorationUpgradeCanvas != null)
        {
            decorationUpgradeCanvas.SetActive(false);
            decorateCamera.SetActive(false);
        }

        CameraManager.Instance.SetInputModeGameplay();
    }

    public void Go3()
    {
        StartCoroutine(Character.Instance.LateTeleport(carriage3Go));

        sideviewEnabled = false;
        sideviewCamera.SetActive(false);
        sideviewCamera2.SetActive(false);
        kitchenCarriageCamera.SetActive(false);
        carriage1Camera.SetActive(false);
        carriage2Camera.SetActive(false);
        carriage3Camera.SetActive(false);
        carriage4Camera.SetActive(false);
        carriage5Camera.SetActive(false);

        kitchenCarriageUI.SetActive(false);
        carriage1UI.SetActive(false);
        carriage2UI.SetActive(false);
        carriage3UI.SetActive(false);
        carriage4UI.SetActive(false);
        carriage5UI.SetActive(false);

        carriageSelectionUI1.SetActive(false);
        carriageSelectionUI2.SetActive(false);
        foreach (GameObject wall in sideviewWalls)
        {
            wall.SetActive(true);
        }

        if (decorationUpgradeCanvas != null)
        {
            decorationUpgradeCanvas.SetActive(false);
            decorateCamera.SetActive(false);
        }

        CameraManager.Instance.SetInputModeGameplay();
    }
    
    public void Go4()
    {
        StartCoroutine(Character.Instance.LateTeleport(carriage4Go));

        sideviewEnabled = false;
        sideviewCamera.SetActive(false);
        sideviewCamera2.SetActive(false);
        kitchenCarriageCamera.SetActive(false);
        carriage1Camera.SetActive(false);
        carriage2Camera.SetActive(false);
        carriage3Camera.SetActive(false);
        carriage4Camera.SetActive(false);
        carriage5Camera.SetActive(false);

        kitchenCarriageUI.SetActive(false);
        carriage1UI.SetActive(false);
        carriage2UI.SetActive(false);
        carriage3UI.SetActive(false);
        carriage4UI.SetActive(false);
        carriage5UI.SetActive(false);

        carriageSelectionUI1.SetActive(false);
        carriageSelectionUI2.SetActive(false);
        foreach (GameObject wall in sideviewWalls)
        {
            wall.SetActive(true);
        }

        if (decorationUpgradeCanvas != null)
        {
            decorationUpgradeCanvas.SetActive(false);
            decorateCamera.SetActive(false);
        }

        CameraManager.Instance.SetInputModeGameplay();
    }
    
    public void Go5()
    {
        StartCoroutine(Character.Instance.LateTeleport(carriage5Go));

        sideviewEnabled = false;
        sideviewCamera.SetActive(false);
        sideviewCamera2.SetActive(false);
        kitchenCarriageCamera.SetActive(false);
        carriage1Camera.SetActive(false);
        carriage2Camera.SetActive(false);
        carriage3Camera.SetActive(false);
        carriage4Camera.SetActive(false);
        carriage5Camera.SetActive(false);

        kitchenCarriageUI.SetActive(false);
        carriage1UI.SetActive(false);
        carriage2UI.SetActive(false);
        carriage3UI.SetActive(false);
        carriage4UI.SetActive(false);
        carriage5UI.SetActive(false);

        carriageSelectionUI1.SetActive(false);
        carriageSelectionUI2.SetActive(false);
        foreach (GameObject wall in sideviewWalls)
        {
            wall.SetActive(true);
        }

        if (decorationUpgradeCanvas != null)
        {
            decorationUpgradeCanvas.SetActive(false);
            decorateCamera.SetActive(false);
        }

        CameraManager.Instance.SetInputModeGameplay();
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

    public void OpenCarriage1Upgrades()
    {
        sideviewCamera.SetActive(false);
        sideviewCamera2.SetActive(false);
        kitchenCarriageCamera.SetActive(false);
        carriage1Camera.SetActive(false);
        carriage2Camera.SetActive(false);
        carriage3Camera.SetActive(false);
        carriage4Camera.SetActive(false);
        carriage5Camera.SetActive(false);

        kitchenCarriageUI.SetActive(false);
        carriage1UI.SetActive(false);
        carriage2UI.SetActive(false);
        carriage3UI.SetActive(false);
        carriage4UI.SetActive(false);
        carriage5UI.SetActive(false);

        clipboardUI.SetActive(true);
        clipboardUpgradesUI.SetActive(true);
        clipboardMainMenuUI.SetActive(false);

        carriageSelectionUI1.SetActive(false);
        carriageSelectionUI2.SetActive(false);
        foreach (GameObject wall in sideviewWalls)
        {
            wall.SetActive(true);
        }

        if (restaurant1Camera != null && restaurant1UpgradeUI != null)
        {
            restaurant1Camera.SetActive(true);
            restaurant1UpgradeUI.SetActive(true);
        }

        clipboardManager._isClipboardActive = true;
        clipboardManager.canClipboard = true;

        restaurant2Camera.SetActive(false);
        restaurant2UpgradeUI.SetActive(false);

        isSideviewUpgrade = true;
    }

    public void OpenCarriage2Upgrades()
    {
        sideviewCamera.SetActive(false);
        sideviewCamera2.SetActive(false);
        kitchenCarriageCamera.SetActive(false);
        carriage1Camera.SetActive(false);
        carriage2Camera.SetActive(false);
        carriage3Camera.SetActive(false);
        carriage4Camera.SetActive(false);
        carriage5Camera.SetActive(false);

        kitchenCarriageUI.SetActive(false);
        carriage1UI.SetActive(false);
        carriage2UI.SetActive(false);
        carriage3UI.SetActive(false);
        carriage4UI.SetActive(false);
        carriage5UI.SetActive(false);

        clipboardUI.SetActive(true);
        clipboardUpgradesUI.SetActive(true);
        clipboardMainMenuUI.SetActive(false);

        carriageSelectionUI1.SetActive(false);
        carriageSelectionUI2.SetActive(false);
        foreach (GameObject wall in sideviewWalls)
        {
            wall.SetActive(true);
        }

        if (restaurant2Camera != null && restaurant2UpgradeUI != null)
        {
            restaurant2Camera.SetActive(true);
            restaurant2UpgradeUI.SetActive(true);
        }

        clipboardManager._isClipboardActive = true;
        clipboardManager.canClipboard = true;

        restaurant1Camera.SetActive(false);
        restaurant1UpgradeUI.SetActive(false);

        isSideviewUpgrade = true;
    }

    public void CloseClipboard()
    {
        restaurant1Camera.SetActive(false);
        restaurant1UpgradeUI.SetActive(true);
        restaurant2Camera.SetActive(false);
        restaurant2UpgradeUI.SetActive(false);

        clipboardUpgradesUI.SetActive(false);
        clipboardMainMenuUI.SetActive(true);

        isSideviewUpgrade = false;
    }

    public void SwitchFrom1To2()
    {
        if (isSideviewUpgrade)
        {
            restaurant1Camera.SetActive(false);
            restaurant2Camera.SetActive(true);
        }
    }

    public void SwitchFrom2To1()
    {
        if (isSideviewUpgrade)
        {
            restaurant1Camera.SetActive(true);
            restaurant2Camera.SetActive(false);
        }
    }
    
    public void ShowSideview1()
    {
        carriageSelectionUI1.SetActive(true);
        carriageSelectionUI2.SetActive(false);
        sideviewCamera.SetActive(true);
        sideviewCamera2.SetActive(false);
    }
    
    public void ShowSideview2()
    {
        carriageSelectionUI1.SetActive(false);
        carriageSelectionUI2.SetActive(true);
        sideviewCamera.SetActive(false);
        sideviewCamera2.SetActive(true);
    }
}