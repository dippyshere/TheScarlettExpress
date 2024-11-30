#region

using System.Collections;
using Dypsloom.DypThePenguin.Scripts.Character;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;

#endregion

public class Elevator : MonoBehaviour
{
    [SerializeField, Tooltip("The temp transition to play when going through the elevator")]
    Animator animator;
    [SerializeField, Tooltip("The transition animation to use")]
    string transitionAnimation;
    [SerializeField, Tooltip("The location to teleport to")]
    Transform destination;

    bool _mIsPlayerNear;
    bool _mIsTransitioning;

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
        if (Character.Instance.promptUI.activeSelf && Input.GetKeyDown(KeyCode.E) && _mIsPlayerNear && !_mIsTransitioning)
        {
            Character.Instance.promptUI.SetActive(false);
            _mIsPlayerNear = false;
            StartCoroutine(TeleportPlayer());
        }
    }
    
    IEnumerator TeleportPlayer()
    {
        _mIsTransitioning = true;
        animator.SetTrigger(transitionAnimation);
        CameraManager.Instance.SetInputModeUI();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(Character.Instance.LateTeleport(destination));
        CameraManager.Instance.ResetCameraPosition();
        CameraManager.Instance.SetInputModeGameplay();
        animator.ResetTrigger(transitionAnimation);
        _mIsTransitioning = false;
    }
}