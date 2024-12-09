#region

using System.Collections;
using Dypsloom.DypThePenguin.Scripts.Character;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

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

    public GameObject kitchenCarriageCamera;
    public Transform kitchenCarriageGo;

    public GameObject kitchenCarriageUI;

    public float money;

    public AudioSource musicR;

    public GameObject decrepitObjects;
    public GameObject renovationParticles;
    public GameObject decrepitBedroomObjects;
    public GameObject renovationBedroomParticles;
    public GameObject decrepitEntertainmentObjects;
    public GameObject renovationEntertainmentParticles;
    public Button renovateButton;
    public Button renovateBedroomButton;
    public Button renovateEntertainmentButton;

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
    public GameObject bedroom1Camera;
    public GameObject bedroom1UpgradeUI;
    public GameObject clipboardPassengerRosterUI;
    public bool isSideviewUpgrade;
    bool sideviewEnabled = false;

    public MoneyUI moneyUI;

    public GameObject goToEveRoomButton;
    public GameObject goToBanksRoomButton;
    public GameObject banksRoomCamera;
    public GameObject banksRoomUI;
    public GameObject banksRoomSterlingButton;
    public GameObject banksRoomSideviewButton;

    void Start()
    {
        money = ProfileSystem.Get<float>(ProfileSystem.Variable.PlayerMoney);
    }

    void Update()
    {
        money = ProfileSystem.Get<float>(ProfileSystem.Variable.PlayerMoney);

        if (ProfileSystem.Get<bool>(ProfileSystem.Variable.HasRenovatedBedroom1))
        {
            renovateBedroomButton.interactable = false;
            decrepitBedroomObjects.SetActive(false);
}
        if (ProfileSystem.Get<bool>(ProfileSystem.Variable.HasRenovatedEntertainment))
        {
            renovateEntertainmentButton.interactable = false;
            decrepitEntertainmentObjects.SetActive(false);
        }
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

            if (bedroom1Camera != null)
                bedroom1Camera.SetActive(false);
            if (restaurant1Camera != null)
                restaurant1Camera.SetActive(false);
            if (restaurant2Camera != null)
                restaurant2Camera.SetActive(false);
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

        if (goToBanksRoomButton != null)
            goToBanksRoomButton.SetActive(false);
        if (goToEveRoomButton != null)
            goToEveRoomButton.SetActive(false);

        if (banksRoomCamera != null)
        {
            banksRoomCamera.SetActive(false);
            banksRoomUI.SetActive(false);
            banksRoomSterlingButton.SetActive(false);
            banksRoomSideviewButton.SetActive(false);
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

        goToBanksRoomButton.SetActive(false);
        goToEveRoomButton.SetActive(false);

        banksRoomCamera.SetActive(false);
        banksRoomUI.SetActive(false);
        banksRoomSterlingButton.SetActive(false);
        banksRoomSideviewButton.SetActive(false);
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

        if (goToBanksRoomButton != null)
            goToBanksRoomButton.SetActive(false);
        if (goToEveRoomButton != null)
            goToEveRoomButton.SetActive(false);

        if (banksRoomCamera != null)
        {
            banksRoomCamera.SetActive(false);
            banksRoomUI.SetActive(false);
            banksRoomSterlingButton.SetActive(false);
            banksRoomSideviewButton.SetActive(false);
        }
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

        goToBanksRoomButton.SetActive(true);
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

            renovateButton.interactable = false;
        }

        if (money <= 99 && decrepitObjects.activeSelf)
        {
            moneyUI.moneyText.color = Color.red;
            Invoke(nameof(RevertMoneyColour), 1f);
        }
    }
    public void RenovateBedroomCarriage()
    {
        if (money >= 100)
        {
            musicR.Play();
            money -= 100;
            ProfileSystem.Set(ProfileSystem.Variable.PlayerMoney, money);

            decrepitBedroomObjects.SetActive(false);
            renovationBedroomParticles.SetActive(true);

            //Invoke(nameof(BackToSterling), 1f);

            renovateBedroomButton.interactable = false;
            ProfileSystem.Set<bool>(ProfileSystem.Variable.HasRenovatedBedroom1, true);
        }

        if (money <= 99 && decrepitBedroomObjects.activeSelf)
        {
            moneyUI.moneyText.color = Color.red;
            Invoke(nameof(RevertMoneyColour), 1f);
        }
    }

    public void RenovateEntertainmentCarriage()
    {
        if (money >= 100)
        {
            musicR.Play();
            money -= 100;
            ProfileSystem.Set(ProfileSystem.Variable.PlayerMoney, money);

            decrepitEntertainmentObjects.SetActive(false);
            renovationEntertainmentParticles.SetActive(true);

            //Invoke(nameof(BackToSterling), 1f);

            renovateEntertainmentButton.interactable = false;
            ProfileSystem.Set<bool>(ProfileSystem.Variable.HasRenovatedEntertainment, true);
        }

        if (money <= 99 && decrepitEntertainmentObjects.activeSelf)
        {
            moneyUI.moneyText.color = Color.red;
            Invoke(nameof(RevertMoneyColour), 1f);
        }
    }

    public void RevertMoneyColour()
    {
        moneyUI.moneyText.color = Color.white;
    }

    public void OpenCarriage1Upgrades()
    {
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

        bedroom1Camera.SetActive(false);
        bedroom1UpgradeUI.SetActive(false);

        isSideviewUpgrade = true;
    }

    public void OpenCarriage2Upgrades()
    {
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

        bedroom1Camera.SetActive(false);
        bedroom1UpgradeUI.SetActive(false);

        isSideviewUpgrade = true;
    }

    public void OpenCarriage3Upgrades()
    {
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

        clipboardUI.SetActive(true);
        clipboardUpgradesUI.SetActive(true);
        clipboardMainMenuUI.SetActive(false);

        carriageSelectionUI1.SetActive(false);
        carriageSelectionUI2.SetActive(false);
        foreach (GameObject wall in sideviewWalls)
        {
            wall.SetActive(false);
        }

        if (bedroom1Camera != null && bedroom1UpgradeUI != null)
        {
            bedroom1Camera.SetActive(true);
            bedroom1UpgradeUI.SetActive(true);
        }

        clipboardManager._isClipboardActive = true;
        clipboardManager.canClipboard = true;

        restaurant1Camera.SetActive(false);
        restaurant1UpgradeUI.SetActive(false);

        restaurant2Camera.SetActive(false);
        restaurant2UpgradeUI.SetActive(false);

        isSideviewUpgrade = true;
    }

    public void CloseClipboard()
    {
        restaurant1Camera.SetActive(false);
        restaurant1UpgradeUI.SetActive(true);
        restaurant2Camera.SetActive(false);
        restaurant2UpgradeUI.SetActive(false);
        bedroom1Camera.SetActive(false);
        bedroom1UpgradeUI.SetActive(false);

        clipboardUpgradesUI.SetActive(false);
        clipboardMainMenuUI.SetActive(true);

        isSideviewUpgrade = false;

        clipboardPassengerRosterUI.SetActive(false);

        foreach (GameObject wall in sideviewWalls)
        {
            wall.SetActive(true);
        }
    }

    public void SwitchFrom1To2()
    {
        if (isSideviewUpgrade)
        {
            restaurant1Camera.SetActive(false);
            restaurant2Camera.SetActive(true);
            bedroom1Camera.SetActive(false);
        }
    }

    public void SwitchFrom1To3()
    {
        if (isSideviewUpgrade)
        {
            restaurant1Camera.SetActive(false);
            restaurant2Camera.SetActive(false);
            bedroom1Camera.SetActive(true);
        }
    }

    public void SwitchFrom2To1()
    {
        if (isSideviewUpgrade)
        {
            restaurant1Camera.SetActive(true);
            restaurant2Camera.SetActive(false);
            bedroom1Camera.SetActive(false);
        }
    }

    public void SwitchFrom2To3()
    {
        if (isSideviewUpgrade)
        {
            restaurant1Camera.SetActive(false);
            restaurant2Camera.SetActive(false);
            bedroom1Camera.SetActive(true);
        }
    }

    public void SwitchFrom3To1()
    {
        if (isSideviewUpgrade)
        {
            restaurant1Camera.SetActive(true);
            restaurant2Camera.SetActive(false);
            bedroom1Camera.SetActive(false);

            foreach (GameObject wall in sideviewWalls)
            {
                wall.SetActive(false);
            }
        }
    }
    public void SwitchFrom3To2()
    {
        if (isSideviewUpgrade)
        {
            restaurant1Camera.SetActive(false);
            restaurant2Camera.SetActive(true);
            bedroom1Camera.SetActive(false);

            foreach (GameObject wall in sideviewWalls)
            {
                wall.SetActive(false);
            }
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