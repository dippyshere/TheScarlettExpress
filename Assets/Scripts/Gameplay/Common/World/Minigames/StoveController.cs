using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveController : MonoBehaviour
{
    [SerializeField] private GameObject UIPrompt;
    [SerializeField] private Transform foodSpawnPoint;
    [SerializeField] private GameObject[] foodPrefabs;
    private bool isPlayerNear;

    void Update()
    {
        if (UIPrompt.activeSelf && Input.GetKeyDown(KeyCode.E) && isPlayerNear)
        {
            UIPrompt.SetActive(true);
            isPlayerNear = false;
            GameObject food = Instantiate(foodPrefabs[Random.Range(0, foodPrefabs.Length)], foodSpawnPoint.position, foodSpawnPoint.rotation);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
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
