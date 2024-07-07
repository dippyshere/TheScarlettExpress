using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassengerManager : MonoBehaviour
{
    [SerializeField, Tooltip("A list of all currently boarded passengers")] private List<PassengerController> passengers = new List<PassengerController>();
    [SerializeField] private GameObject[] passengerPrefabs;
    [SerializeField, Tooltip("A list of passener spawn points")] private List<Transform> spawnPoints = new List<Transform>();

    [SerializeField] GameObject player;
    StationSettings sSettings;


    public void Start()
    {
        sSettings =player.GetComponent<StationSettings>();
        for (int i = 0; i < spawnPoints.Count; i++)
        {
            SpawnPassenger();
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            sSettings.StationDistance--;
            AdvanceDay();
            
        }
    }

    public void SpawnPassenger()
    {
        foreach (Transform spawnPoint in spawnPoints)
        {
            if (spawnPoint.gameObject.activeSelf)
            {
                GameObject passengerPrefab = passengerPrefabs[Random.Range(0, passengerPrefabs.Length)];
                GameObject newPassenger = Instantiate(passengerPrefab, spawnPoint.position, spawnPoint.rotation);
                passengers.Add(newPassenger.GetComponent<PassengerController>());
                spawnPoint.gameObject.SetActive(false);
                break;
            }
        }
    }

    public void RemovePassenger(PassengerController passenger)
    {
        int owed = passenger.CalculateTripValue();
        GameObject.FindGameObjectWithTag("Player").GetComponent<Economy>().AddMoney(owed);
        Transform closestSeat = spawnPoints[0];
        float closestDistance = Vector3.Distance(passenger.transform.position, spawnPoints[0].position);
        foreach (Transform spawnPoint in spawnPoints)
        {
            float distance = Vector3.Distance(passenger.transform.position, spawnPoint.position);
            if (distance < closestDistance)
            {
                closestSeat = spawnPoint;
                closestDistance = distance;
            }
        }
        closestSeat.gameObject.SetActive(true);
        Destroy(passenger.gameObject);
    }

    public void AdvanceDay()
    {
        

        List<PassengerController> passengersToRemove = new List<PassengerController>();
        foreach (PassengerController passenger in passengers)
        {
            passenger.daysLeft--;
            if (passenger.daysLeft <= 0)
            {
                RemovePassenger(passenger);
                passengersToRemove.Add(passenger);
            }
        }
        foreach (PassengerController passenger in passengersToRemove)
        {
            passengers.Remove(passenger);
        }
        StartCoroutine(DelayedSpawnPassengers());
    }

    public IEnumerator DelayedSpawnPassengers()
    {
        yield return new WaitForSeconds(2);
        for (int i = 0; i < spawnPoints.Count; i++)
        {
            SpawnPassenger();
        }
    }
}
