using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StationSettings : MonoBehaviour
{
    [SerializeField] GameObject mapCanvas;

    public int StationDistanceA;

    public int stationDestinationA;
    


    // Start is called before the first frame update
    void Start()
    {
        int destin = ProfileSystem.Get<int>(ProfileSystem.Variable.StationDestination);
        
    }

    // Update is called once per frame
    void Update()
    {
        int dist = ProfileSystem.Get<int>(ProfileSystem.Variable.StationDistance);
        if (dist <= 0 && Input.GetKeyDown(KeyCode.M))
        {
            Debug.Log("Go To Station!");
            TravelToStation();
            StationDistanceA = 3;
        }

        
        int destin = ProfileSystem.Get<int>(ProfileSystem.Variable.StationDestination);
    }

    public void back()
    {
        mapCanvas.SetActive(false);
        
    }

    public void SetStation1()
    {
        ProfileSystem.Set(ProfileSystem.Variable.StationDistance, 3);
        ProfileSystem.Set(ProfileSystem.Variable.StationDestination, 1);

        //StationDistanceA = 3;
        //stationDestinationA = 1;
        SceneManager.LoadScene("PlayerTesting");
    }

    public void SetStation2()
    {
        ProfileSystem.Set(ProfileSystem.Variable.StationDistance, 3);
        ProfileSystem.Set(ProfileSystem.Variable.StationDestination, 2);

        //StationDistanceA = 3;
        //stationDestinationA = 2;
        SceneManager.LoadScene("PlayerTesting");
    }

    public void SetStation3()
    {
        ProfileSystem.Set(ProfileSystem.Variable.StationDistance, 3);
        ProfileSystem.Set(ProfileSystem.Variable.StationDestination, 3);

        //StationDistanceA = 3;
        //stationDestinationA = 3;
        SceneManager.LoadScene("PlayerTesting");
    }

    public void TravelToStation()
    {
        int destin = ProfileSystem.Get<int>(ProfileSystem.Variable.StationDestination);
        PassengerManager.instance.ArriveAtStation(destin);
        // check if the StationDestination is 1,2 or 3 to go to the station
        if (destin <= 1)
        {
            Debug.Log("Load Station1");
            SceneManager.LoadScene("Station1");
        }
        if (destin == 2)
        {
            SceneManager.LoadScene("Station2");
        }
        if (destin == 3)
        {
            SceneManager.LoadScene("Station3");
        }

        //if (StationDistanceA == 1)
        //{
        //    Debug.Log("Load Station1");
        //    SceneManager.LoadScene("Station1");
        //}
        //if (StationDistanceA == 2)
        //{
        //    SceneManager.LoadScene("Station2");
        //}
        //if (StationDistanceA == 3)
        //{
        //    SceneManager.LoadScene("Station3");
        //}


    }

}
