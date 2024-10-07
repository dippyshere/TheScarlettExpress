#region

using System.Collections;
using System.Collections.Generic;
using Dypsloom.DypThePenguin.Scripts.Character;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

#endregion

public class StoveController : MonoBehaviour
{
    [FormerlySerializedAs("UIFoodPicker"), SerializeField] 
    GameObject uiFoodPicker;

    [SerializeField] GameObject uiFoodTimer;
    [SerializeField] Image foodTimerFill;
    [SerializeField] Transform foodSpawnPoint;
    [SerializeField] GameObject[] foodPrefabs;
    [SerializeField] string stoveFoodType;
    public float foodCookTime = 5f;

    [SerializeField] AudioSource musicC;
    [SerializeField] AudioSource musicD;

    [SerializeField] GameObject clipboard;
    bool _isPlayerNear;
    int _pendingFoodType;
    bool _pendingPickup;

    void Start()
    {
        uiFoodTimer.SetActive(false);
        uiFoodPicker.SetActive(false);
    }

    void Update()
    {
        if (Character.Instance.promptUI.activeSelf && Input.GetKeyDown(KeyCode.E) && _isPlayerNear && uiFoodPicker.activeSelf == false)
        {
            uiFoodPicker.SetActive(true);
            CameraManager.Instance.SetInputModeUI();
        }

        if (uiFoodPicker.activeSelf)
        {
            CameraManager.Instance.SetInputModeUI();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !_pendingPickup)
        {
            if (Pickup.Instance.hasItem)
            {
                return;
            }

            Character.Instance.promptUI.SetActive(true);
            _isPlayerNear = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Character.Instance.promptUI.SetActive(false);
            _isPlayerNear = false;
        }
    }

    public void SelectFood(int selectedFoodType)
    {
        _pendingFoodType = selectedFoodType;
        DismissFoodPicker();
        StartCoroutine(FoodTimer());
        _pendingPickup = true;
        Character.Instance.promptUI.SetActive(false);
        if (TrainGameAnalytics.instance != null)
        {
            TrainGameAnalytics.instance.RecordGameEvent("food_cooked",
                new Dictionary<string, object> { { "foodType", stoveFoodType + selectedFoodType } });
        }

        clipboard.GetComponent<ClipboardManager>().canClipboard = true;
    }

    void SpawnFood()
    {
        _isPlayerNear = false;
        GameObject food = Instantiate(foodPrefabs[_pendingFoodType], foodSpawnPoint.position, foodSpawnPoint.rotation);
        _pendingFoodType = -1;
        food.GetComponent<FoodManager>().stoveController = this;
        food.GetComponent<Rigidbody>().isKinematic = true;
    }

    IEnumerator FoodTimer()
    {
        uiFoodTimer.SetActive(true);
        float timer = 0;
        musicC.Play();
        
        while (timer < foodCookTime)
        {
            timer += Time.smoothDeltaTime;
            foodTimerFill.fillAmount = timer / foodCookTime;
            yield return null;
        }

        musicC.Stop();
        musicD.Play();
        SpawnFood();
        uiFoodTimer.SetActive(false);
    }

    public void DismissFoodPicker()
    {
        uiFoodPicker.SetActive(false);
        CameraManager.Instance.SetInputModeGameplay();
    }

    public void PlacedFood()
    {
        _pendingPickup = false;
    }
}