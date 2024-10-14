#region

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using Random = UnityEngine.Random;

#endregion

public class PassengerManager : MonoBehaviour
{
    [HideInInspector, Tooltip("Singleton instance of the passenger manager")]
    public static PassengerManager Instance;

    Dictionary<string, Dictionary<string, object>> _passengerData = new();
    [SerializeField] GameObject[] passengerPrefabs;

    [Tooltip("A list of all currently boarded passengers")]
    public List<PassengerController> passengers = new();

    [SerializeField] bool spawnPassengers = true;

    [SerializeField, Tooltip("A list of passener spawn points")]
    List<Transform> spawnPoints = new();

    void Awake()
    {
        Instance = this;
    }

    public void Start()
    {
        LoadPassengerData();
        if (spawnPassengers)
        {
            int random = Random.Range(4, 7);
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
                int passengerType = Random.Range(0, passengerPrefabs.Length);
                GameObject passengerPrefab = passengerPrefabs[passengerType];
                GameObject newPassenger = Instantiate(passengerPrefab, spawnPoint.position, spawnPoint.rotation);
                passengers.Add(newPassenger.GetComponent<PassengerController>());
                spawnPoint.gameObject.SetActive(false);
                _passengerData[Convert.ToString(spawnPoints.IndexOf(spawnPoint))] = new Dictionary<string, object>();
                _passengerData[Convert.ToString(spawnPoints.IndexOf(spawnPoint))]["foodType"] =
                    (int)newPassenger.GetComponent<PassengerController>().foodType;
                _passengerData[Convert.ToString(spawnPoints.IndexOf(spawnPoint))]["hungerLevel"] =
                    newPassenger.GetComponent<PassengerController>().hungerLevel;
                _passengerData[Convert.ToString(spawnPoints.IndexOf(spawnPoint))]["comfortLevel"] =
                    newPassenger.GetComponent<PassengerController>().comfortLevel;
                _passengerData[Convert.ToString(spawnPoints.IndexOf(spawnPoint))]["entertainmentLevel"] =
                    newPassenger.GetComponent<PassengerController>().entertainmentLevel;
                _passengerData[Convert.ToString(spawnPoints.IndexOf(spawnPoint))]["destinationId"] =
                    newPassenger.GetComponent<PassengerController>().destinationId;
                _passengerData[Convert.ToString(spawnPoints.IndexOf(spawnPoint))]["species"] =
                    newPassenger.GetComponent<PassengerController>().species;
                _passengerData[Convert.ToString(spawnPoints.IndexOf(spawnPoint))]["passengerName"] =
                    newPassenger.GetComponent<PassengerController>().passengerName;
                _passengerData[Convert.ToString(spawnPoints.IndexOf(spawnPoint))]["passengerType"] = passengerType;
                break;
            }
        }

        SavePassengerData();
    }

    public int RemovePassenger(PassengerController passenger)
    {
        int owed = passenger.CalculateTripValue();
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
        _passengerData.Remove(Convert.ToString(spawnPoints.IndexOf(closestSeat)));
        Destroy(passenger.gameObject);
        return owed;
    }

    public void DayAdvanceCleanup()
    {
        foreach (PassengerController passenger in passengers)
        {
            passenger.CleanPlate();
        }

        SavePassengerData();

        SpecialPassengerRent();
    }
    
    void SpecialPassengerRent()
    {
        foreach (PassengerController passenger in passengers)
        {
            if (passenger.isSpecialPassenger)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<Economy>().AddMoney(5);
            }
        }
    }

    public void ArriveAtStation(int stationId)
    {
        int owedMoney = 0;
        List<PassengerController> passengersToRemove = new();
        foreach (PassengerController passenger in passengers)
        {
            if (passenger.destinationId == stationId && !passenger.isSpecialPassenger)
            {
                owedMoney += RemovePassenger(passenger);
                passengersToRemove.Add(passenger);
            }
        }

        foreach (PassengerController passenger in passengersToRemove)
        {
            passengers.Remove(passenger);
        }
        GameObject.FindGameObjectWithTag("Player").GetComponent<Economy>().AddMoney(owedMoney);
    }

    public void SavePassengerData()
    {
        string json = JsonConvert.SerializeObject(_passengerData);
        ProfileSystem.Set(ProfileSystem.Variable.PassengerStorage, json);
    }

    void LoadPassengerData()
    {
        _passengerData =
            JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, object>>>(
                ProfileSystem.Get<string>(ProfileSystem.Variable.PassengerStorage));
        foreach (string key in _passengerData.Keys)
        {
            foreach (Transform spawnPoint in spawnPoints)
            {
                if (spawnPoints.IndexOf(spawnPoint) == Convert.ToInt32(key))
                {
                    GameObject passengerPrefab = passengerPrefabs[Convert.ToInt32(_passengerData[key]["passengerType"])];
                    GameObject newPassenger = Instantiate(passengerPrefab, spawnPoint.position, spawnPoint.rotation);
                    passengers.Add(newPassenger.GetComponent<PassengerController>());
                    newPassenger.GetComponent<PassengerController>().foodType =
                        (FoodManager.FoodType)Convert.ToInt32(_passengerData[key]["foodType"]);
                    newPassenger.GetComponent<PassengerController>().hungerLevel =
                        Convert.ToSingle(_passengerData[key]["hungerLevel"]);
                    newPassenger.GetComponent<PassengerController>().comfortLevel =
                        Convert.ToSingle(_passengerData[key]["comfortLevel"]);
                    newPassenger.GetComponent<PassengerController>().entertainmentLevel =
                        Convert.ToSingle(_passengerData[key]["entertainmentLevel"]);
                    newPassenger.GetComponent<PassengerController>().destinationId =
                        Convert.ToInt32(_passengerData[key]["destinationId"]);
                    newPassenger.GetComponent<PassengerController>().species = (string)_passengerData[key]["species"];
                    newPassenger.GetComponent<PassengerController>().passengerName =
                        (string)_passengerData[key]["passengerName"];
                    spawnPoint.gameObject.SetActive(false);
                    break;
                }
            }
        }
    }
}