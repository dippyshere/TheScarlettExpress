using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassengerManager : MonoBehaviour
{
    [HideInInspector, Tooltip("Singleton instance of the passenger manager")] public static PassengerManager instance;
    [Tooltip("A list of all currently boarded passengers")] public List<PassengerController> passengers = new List<PassengerController>();
    [SerializeField] private GameObject[] passengerPrefabs;
    [SerializeField, Tooltip("A list of passener spawn points")] private List<Transform> spawnPoints = new List<Transform>();

    //StationSettings sSettings;

    private void Awake()
    {
        instance = this;
    }

    public void Start()
    {
        int random = Random.Range(4, 7);
        for (int i = 0; i < random; i++)
        {
            SpawnPassenger();
        }
    }

    public void Update()
    {

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

    public void DayAdvanceCleanup()
    {
        foreach (var passenger in passengers)
        {
            passenger.CleanPlate();
        }
    }

    public void ArriveAtStation(int stationId)
    {
        List<PassengerController> passengersToRemove = new List<PassengerController>();
        foreach (PassengerController passenger in passengers)
        {
            if (passenger.destinationId == stationId)
            {
                RemovePassenger(passenger);
                passengersToRemove.Add(passenger);
            }
        }
        foreach (PassengerController passenger in passengersToRemove)
        {
            passengers.Remove(passenger);
        }
    }

    //public IEnumerator DelayedSpawnPassengers()
    //{
    //    yield return new WaitForSeconds(2);
    //    for (int i = 0; i < spawnPoints.Count; i++)
    //    {
    //        SpawnPassenger();
    //    }
    //}
}
