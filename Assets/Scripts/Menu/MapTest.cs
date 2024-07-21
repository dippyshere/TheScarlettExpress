//using Dypsloom.DypThePenguin.Scripts.Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTest : MonoBehaviour
{

    [SerializeField] GameObject canvas;
    [SerializeField] GameObject shopUI;

    //[SerializeField, Tooltip("Reference to the player script.")]
    //private Character m_Player; 

    public bool isEve;

    private void Start()
    {
        canvas.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
       if (isEve && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("EVE! ACCTIVATE!");
            shopUI.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            //m_Player.m_MovementMode = MovementMode.Decorating;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Map"))
        {
            Debug.Log("MAP! ACTIVATE!");
            canvas.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            //m_Player.m_MovementMode = MovementMode.Decorating;
        }

        if (other.CompareTag("Eve"))
        {
            isEve = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Eve"))
        {
            isEve = false;
        }
    }
}
