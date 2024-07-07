using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StationSettings : MonoBehaviour
{
    [SerializeField] GameObject mapCanvas;

    public int StationDistance;

    public int stationDestination;


    // Start is called before the first frame update
    void Start()
    {
        StationDistance = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if (StationDistance <= 1 && Input.GetKeyDown(KeyCode.M))
        {
            Debug.Log("Go To Station!");
            TravelToStation();
            StationDistance = 3;
        }

    }

    public void back()
    {
        mapCanvas.SetActive(false);
        Cursor.visible = false;
    }

    public void SetStation1()
    {
        StationDistance = 3;
        stationDestination = 1;
        SceneManager.LoadScene("PlayerTesting");
    }

    public void SetStation2()
    {
        StationDistance = 3;
        stationDestination = 2;
        SceneManager.LoadScene("PlayerTesting");
    }

    public void SetStation3()
    {
        StationDistance = 3;
        stationDestination = 3;
        SceneManager.LoadScene("PlayerTesting");
    }

    public void TravelToStation()
    {   
        if (StationDistance == 1)
        {
            Debug.Log("Load Station1");
            SceneManager.LoadScene("Station1");
        }
        if (StationDistance == 2)
        {
            SceneManager.LoadScene("Station2");
        }
        if (StationDistance == 3)
        {
            SceneManager.LoadScene("Station3");
        }


    }

}
