#region

using Dypsloom.DypThePenguin.Scripts.Character;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;

#endregion

public class DoorInteraction : MonoBehaviour
{
    public GameObject decorationUpgradeCanvas;

    [FormerlySerializedAs("m_Camera"),SerializeField, Tooltip("The camera to enable/disable when the door is interacted with.")]
    GameObject mCamera;

    [FormerlySerializedAs("m_ExitPrompt"),SerializeField, Tooltip("The Exit UI Prompt to show when inside the room.")]
    GameObject mExitPrompt;

    bool _mIsPlayerNear;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Character.Instance.promptUI.SetActive(true);
            _mIsPlayerNear = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Character.Instance.promptUI.SetActive(false);
            _mIsPlayerNear = false;
        }
    }

    void Update()
    {
        if (Character.Instance.promptUI.activeSelf && Input.GetKeyDown(KeyCode.E) && _mIsPlayerNear && !DialogueCallback.Instance.inDialogue)
        {
            mCamera.SetActive(true);
            Character.Instance.promptUI.SetActive(false);
            mExitPrompt.SetActive(true);
            _mIsPlayerNear = false;
            CameraManager.Instance.SetInputModeUI();
        }
    }

    public void ExitRoom()
    {
        mCamera.SetActive(false);
        mExitPrompt.SetActive(false);
        decorationUpgradeCanvas.SetActive(false);
        CameraManager.Instance.SetInputModeGameplay();
    }
}