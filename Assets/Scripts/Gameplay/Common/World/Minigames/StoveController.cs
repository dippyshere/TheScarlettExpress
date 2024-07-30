using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveController : MonoBehaviour
{
    [SerializeField] private GameObject UIPrompt;
    [SerializeField] private Transform foodSpawnPoint;
    [SerializeField] private GameObject[] foodPrefabs;
    private bool isPlayerNear;
    private bool pendingPickup;

    void Update()
    {
        if (UIPrompt.activeSelf && Input.GetKeyDown(KeyCode.E) && isPlayerNear)
        {
            UIPrompt.SetActive(true);
            isPlayerNear = false;
            GameObject food = Instantiate(foodPrefabs[Random.Range(0, foodPrefabs.Length)], foodSpawnPoint.position, foodSpawnPoint.rotation);
            food.GetComponent<FoodManager>().stoveController = this;
            pendingPickup = true;
        }
    }

    public void PlacedFood()
    {
        pendingPickup = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !pendingPickup)
        {
            UIPrompt.SetActive(true);
            isPlayerNear = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UIPrompt.SetActive(false);
            isPlayerNear = false;
        }
    }
}
