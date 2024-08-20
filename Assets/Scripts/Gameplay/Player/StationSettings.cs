using Dypsloom.DypThePenguin.Scripts.Character;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.Cinemachine;

public class StationSettings : MonoBehaviour
{
    [SerializeField] GameObject mapCanvas;

    public int StationDistanceA;

    public int stationDestinationA;

    [SerializeField, Tooltip("Reference to the player script.")]
    private Character m_Player;

    [SerializeField, Tooltip("Reference to the cinemachine input manager.")]
    private CinemachineInputAxisController m_CinemachineInputAxisController;

    [SerializeField] private GameObject ThampConfirm;
    [SerializeField] private GameObject RiverConfirm;
    [SerializeField] private GameObject FurroConfirm;
    [SerializeField] private GameObject BranchConfirm;
    [SerializeField] private GameObject FernConfirm;

    public AudioSource music;

    // Start is called before the first frame update
    void Start()
    {
        if (ThampConfirm != null)
            ThampConfirm.SetActive(false);
        if (RiverConfirm != null)
            RiverConfirm.SetActive(false);
        if (FurroConfirm != null)
            FurroConfirm.SetActive(false);
        if (BranchConfirm != null)
            BranchConfirm.SetActive(false);
        if (FernConfirm != null)
            FernConfirm.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            float moneys = ProfileSystem.Get<float>(ProfileSystem.Variable.PlayerMoney);
            ProfileSystem.Set(ProfileSystem.Variable.PlayerMoney, moneys + 100);
        }

        int dist = ProfileSystem.Get<int>(ProfileSystem.Variable.StationDistance);
        if (dist <= 0)
        {
            Debug.Log("Go To Station!");
            TravelToStation();
            StationDistanceA = 3;
        }
    }

    public void back()
    {
        music.Play();
        mapCanvas.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        m_CinemachineInputAxisController.enabled = true;

        m_Player.m_MovementMode = MovementMode.Free;
    }

    public void SetStation1()
    {
        ProfileSystem.Set(ProfileSystem.Variable.StationDistance, 3);
        ProfileSystem.Set(ProfileSystem.Variable.StationDestination, 1);
        RiverConfirm.SetActive(true);
    }

    public void SetStation2()
    {
        ProfileSystem.Set(ProfileSystem.Variable.StationDistance, 3);
        ProfileSystem.Set(ProfileSystem.Variable.StationDestination, 2);
        FurroConfirm.SetActive(true);
    }

    public void SetStation3()
    {
        ProfileSystem.Set(ProfileSystem.Variable.StationDistance, 3);
        ProfileSystem.Set(ProfileSystem.Variable.StationDestination, 3);
        ThampConfirm.SetActive(true);
    }

    public void SetStation4()
    {
        ProfileSystem.Set(ProfileSystem.Variable.StationDistance, 3);
        ProfileSystem.Set(ProfileSystem.Variable.StationDestination, 4);
        BranchConfirm.SetActive(true);
    }

    public void SetStation5()
    {
        ProfileSystem.Set(ProfileSystem.Variable.StationDistance, 3);
        ProfileSystem.Set(ProfileSystem.Variable.StationDestination, 5);
        FernConfirm.SetActive(true);
    }

    public void TravelToStation()
    {
        int destin = ProfileSystem.Get<int>(ProfileSystem.Variable.StationDestination);
        PassengerManager.instance.ArriveAtStation(destin);
        // check if the StationDestination is 1,2 or 3 to go to the station

        if (destin == 0)
        {
            SceneManager.LoadScene("StationTutorial");
        }
        if (destin == 1)
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
        if (destin == 4)
        {
            SceneManager.LoadScene("Station4");
        }
        if (destin == 5)
        {
            SceneManager.LoadScene("Station5");
        }

        ProfileSystem.Set(ProfileSystem.Variable.StationDistance, 1);
    }

    public void NotLoadTrain()
    {
        ThampConfirm.SetActive(false);
        RiverConfirm.SetActive(false);
        FurroConfirm.SetActive(false);
        BranchConfirm.SetActive(false);
        FernConfirm.SetActive(false);
    }

    public void LoadTarin()
    {
        SceneManager.LoadScene("PlayerTesting");
    }

}
