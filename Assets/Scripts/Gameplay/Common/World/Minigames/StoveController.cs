using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Unity.Cinemachine;
using Dypsloom.DypThePenguin.Scripts.Character;

public class StoveController : MonoBehaviour
{
    [SerializeField, Tooltip("Reference to the player script.")] private Character player;
    [SerializeField, Tooltip("Reference to the cinemachine input manager.")] private CinemachineInputAxisController cinemachineInputAxisController;
    [FormerlySerializedAs("UIFoodPicker")] [SerializeField] private GameObject uiFoodPicker;
    [SerializeField] private GameObject uiFoodTimer;
    [SerializeField] private Image foodTimerFill;
    [SerializeField] private Transform foodSpawnPoint;
    [SerializeField] private GameObject[] foodPrefabs;
    public float foodCookTime = 5f;
    private GameObject uiPrompt;
    private bool _isPlayerNear;
    private bool _pendingPickup;
    private int _pendingFoodType;

    private void Start()
    {
        uiFoodTimer.SetActive(false);
        uiFoodPicker.SetActive(false);
        uiPrompt = player.GetComponent<Pickup>().pickupPrompt;
    }

    private void Update()
    {
        if (uiPrompt.activeSelf && Input.GetKeyDown(KeyCode.E) && _isPlayerNear && uiFoodPicker.activeSelf == false)
        {
            uiFoodPicker.SetActive(true);
            player.m_MovementMode = MovementMode.Decorating;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            cinemachineInputAxisController.enabled = false;
        }
    }

    public void SelectFood(int selectedFoodType)
    {
        _pendingFoodType = selectedFoodType;
        DismissFoodPicker();
        StartCoroutine(FoodTimer());
        _pendingPickup = true;
        uiPrompt.SetActive(false);
    }

    private void SpawnFood()
    {
        uiPrompt.SetActive(true);
        _isPlayerNear = false;
        GameObject food = Instantiate(foodPrefabs[_pendingFoodType], foodSpawnPoint.position, foodSpawnPoint.rotation);
        _pendingFoodType = -1;
        food.GetComponent<FoodManager>().stoveController = this;
        food.GetComponent<Rigidbody>().isKinematic = true;
    }

    private IEnumerator FoodTimer()
    {
        uiFoodTimer.SetActive(true);
        float timer = 0;
        while (timer < foodCookTime)
        {
            timer += Time.smoothDeltaTime;
            foodTimerFill.fillAmount = timer / foodCookTime;
            yield return null;
        }
        SpawnFood();
        uiFoodTimer.SetActive(false);
    }

    public void DismissFoodPicker()
    {
        uiFoodPicker.SetActive(false);
        player.m_MovementMode = MovementMode.Free;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        cinemachineInputAxisController.enabled = true;
    }

    public void PlacedFood()
    {
        _pendingPickup = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !_pendingPickup)
        {
            if (player.GetComponent<Pickup>().hasItem)
            {
                return;
            }
            uiPrompt.SetActive(true);
            _isPlayerNear = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            uiPrompt.SetActive(false);
            _isPlayerNear = false;
        }
    }
}
