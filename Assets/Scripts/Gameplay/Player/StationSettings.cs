#region

using Dypsloom.DypThePenguin.Scripts.Character;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

#endregion

public class StationSettings : MonoBehaviour
{
    [FormerlySerializedAs("BranchConfirm"),SerializeField] GameObject branchConfirm;
    [FormerlySerializedAs("FernConfirm"),SerializeField] GameObject fernConfirm;
    [FormerlySerializedAs("FurroConfirm"),SerializeField] GameObject furroConfirm;
    [FormerlySerializedAs("RiverConfirm"),SerializeField] GameObject riverConfirm;
    [FormerlySerializedAs("ThampConfirm"),SerializeField] GameObject thampConfirm;
    [SerializeField] GameObject mapCanvas;

    public AudioSource music;


    // Start is called before the first frame update
    void Start()
    {
        if (thampConfirm != null)
        {
            thampConfirm.SetActive(false);
        }

        if (riverConfirm != null)
        {
            riverConfirm.SetActive(false);
        }

        if (furroConfirm != null)
        {
            furroConfirm.SetActive(false);
        }

        if (branchConfirm != null)
        {
            branchConfirm.SetActive(false);
        }

        if (fernConfirm != null)
        {
            fernConfirm.SetActive(false);
        }
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
        }
    }

    public void Back()
    {
        music.Play();
        mapCanvas.SetActive(false);
        CameraManager.Instance.SetInputModeGameplay();
    }

    public void SetStation1()
    {
        ProfileSystem.Set(ProfileSystem.Variable.StationDistance, 3);
        ProfileSystem.Set(ProfileSystem.Variable.StationDestination, 1);
        riverConfirm.SetActive(true);
    }

    public void SetStation2()
    {
        ProfileSystem.Set(ProfileSystem.Variable.StationDistance, 3);
        ProfileSystem.Set(ProfileSystem.Variable.StationDestination, 2);
        furroConfirm.SetActive(true);
    }

    public void SetStation3()
    {
        ProfileSystem.Set(ProfileSystem.Variable.StationDistance, 3);
        ProfileSystem.Set(ProfileSystem.Variable.StationDestination, 3);
        thampConfirm.SetActive(true);
    }

    public void SetStation4()
    {
        ProfileSystem.Set(ProfileSystem.Variable.StationDistance, 3);
        ProfileSystem.Set(ProfileSystem.Variable.StationDestination, 4);
        branchConfirm.SetActive(true);
    }

    public void SetStation5()
    {
        ProfileSystem.Set(ProfileSystem.Variable.StationDistance, 3);
        ProfileSystem.Set(ProfileSystem.Variable.StationDestination, 5);
        fernConfirm.SetActive(true);
    }

    public void TravelToStation()
    {
        int destin = ProfileSystem.Get<int>(ProfileSystem.Variable.StationDestination);
        PassengerManager.Instance.ArriveAtStation(destin);
        // check if the StationDestination is 1,2 or 3 to go to the station

        switch (destin)
        {
            case 0:
                SceneManager.LoadScene("_StationTutorial");
                break;
            case 1:
                SceneManager.LoadScene("_RiversideStation");
                break;
            case 2:
                SceneManager.LoadScene("_FurrowoodStation");
                break;
            case 3:
                SceneManager.LoadScene("_ThampStation");
                break;
            case 4:
                SceneManager.LoadScene("_BranchviewStation");
                break;
            case 5:
                SceneManager.LoadScene("_FernValleyStation");
                break;
        }

        ProfileSystem.Set(ProfileSystem.Variable.StationDistance, 1);
    }

    public void NotLoadTrain()
    {
        thampConfirm.SetActive(false);
        riverConfirm.SetActive(false);
        furroConfirm.SetActive(false);
        branchConfirm.SetActive(false);
        fernConfirm.SetActive(false);
    }

    public void LoadTarin()
    {
        SceneManager.LoadScene("_TrainInterior");
    }
}