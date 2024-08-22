using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class PassengerManager : MonoBehaviour
{
    [HideInInspector, Tooltip("Singleton instance of the passenger manager")] public static PassengerManager instance;
    [Tooltip("A list of all currently boarded passengers")] public List<PassengerController> passengers = new List<PassengerController>();
    [SerializeField] private GameObject[] passengerPrefabs;
    [SerializeField, Tooltip("A list of passener spawn points")] private List<Transform> spawnPoints = new List<Transform>();
    [SerializeField] private bool spawnPassengers = true;

    private Dictionary<string, Dictionary<string, object>> passengerData = new Dictionary<string, Dictionary<string, object>>();

    //StationSettings sSettings;

    private void Awake()
    {
        instance = this;
    }

    public void Start()
    {
        LoadPassengerData();
        if (spawnPassengers)
        {
            int random = UnityEngine.Random.Range(4, 7);
            for (int i = 0; i < random; i++)
            {
                SpawnPassenger();
            }
        }
    }

    public void SpawnPassenger()
    {
        foreach (Transform spawnPoint in spawnPoints)
        {
            if (spawnPoint.gameObject.activeSelf)
            {
                int passengerType = UnityEngine.Random.Range(0, passengerPrefabs.Length);
                GameObject passengerPrefab = passengerPrefabs[passengerType];
                GameObject newPassenger = Instantiate(passengerPrefab, spawnPoint.position, spawnPoint.rotation);
                passengers.Add(newPassenger.GetComponent<PassengerController>());
                spawnPoint.gameObject.SetActive(false);
                passengerData[Convert.ToString(spawnPoints.IndexOf(spawnPoint))] = new Dictionary<string, object>();
                passengerData[Convert.ToString(spawnPoints.IndexOf(spawnPoint))]["foodType"] = (int)newPassenger.GetComponent<PassengerController>().foodType;
                passengerData[Convert.ToString(spawnPoints.IndexOf(spawnPoint))]["hungerLevel"] = newPassenger.GetComponent<PassengerController>().hungerLevel;
                passengerData[Convert.ToString(spawnPoints.IndexOf(spawnPoint))]["comfortLevel"] = newPassenger.GetComponent<PassengerController>().comfortLevel;
                passengerData[Convert.ToString(spawnPoints.IndexOf(spawnPoint))]["entertainmentLevel"] = newPassenger.GetComponent<PassengerController>().entertainmentLevel;
                passengerData[Convert.ToString(spawnPoints.IndexOf(spawnPoint))]["destinationId"] = newPassenger.GetComponent<PassengerController>().destinationId;
                passengerData[Convert.ToString(spawnPoints.IndexOf(spawnPoint))]["species"] = newPassenger.GetComponent<PassengerController>().species;
                passengerData[Convert.ToString(spawnPoints.IndexOf(spawnPoint))]["passengerName"] = newPassenger.GetComponent<PassengerController>().passengerName;
                passengerData[Convert.ToString(spawnPoints.IndexOf(spawnPoint))]["passengerType"] = passengerType;
                break;
            }
        }
        SavePassengerData();
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
        passengerData.Remove(Convert.ToString(spawnPoints.IndexOf(closestSeat)));
        Destroy(passenger.gameObject);
    }

    public void DayAdvanceCleanup()
    {
        foreach (var passenger in passengers)
        {
            passenger.CleanPlate();
        }
        SavePassengerData();
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

    public void SavePassengerData()
    {
        string json = JsonConvert.SerializeObject(passengerData);
        Debug.Log(json);
        ProfileSystem.Set(ProfileSystem.Variable.PassengerStorage, json);
    }

    private void LoadPassengerData()
    {
        passengerData = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, object>>>(ProfileSystem.Get<string>(ProfileSystem.Variable.PassengerStorage));
        foreach (string key in passengerData.Keys)
        {
            foreach (Transform spawnPoint in spawnPoints)
            {
                if (spawnPoints.IndexOf(spawnPoint) == Convert.ToInt32(key))
                {
                    GameObject passengerPrefab = passengerPrefabs[Convert.ToInt32(passengerData[key]["passengerType"])];
                    GameObject newPassenger = Instantiate(passengerPrefab, spawnPoint.position, spawnPoint.rotation);
                    passengers.Add(newPassenger.GetComponent<PassengerController>());
                    newPassenger.GetComponent<PassengerController>().foodType = (FoodManager.FoodType)Convert.ToInt32(passengerData[key]["foodType"]);
                    newPassenger.GetComponent<PassengerController>().hungerLevel = Convert.ToSingle(passengerData[key]["hungerLevel"]);
                    newPassenger.GetComponent<PassengerController>().comfortLevel = Convert.ToSingle(passengerData[key]["comfortLevel"]);
                    newPassenger.GetComponent<PassengerController>().entertainmentLevel = Convert.ToSingle(passengerData[key]["entertainmentLevel"]);
                    newPassenger.GetComponent<PassengerController>().destinationId = Convert.ToInt32(passengerData[key]["destinationId"]);
                    newPassenger.GetComponent<PassengerController>().species = (string)passengerData[key]["species"];
                    newPassenger.GetComponent<PassengerController>().passengerName = (string)passengerData[key]["passengerName"];
                    spawnPoint.gameObject.SetActive(false);
                    break;
                }
            }
        }
    }
}
