using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deliver : MonoBehaviour
{
    public bool canPickup;
    public bool hasItem;
 //   float spacingZ = 2.0f;

    public GameObject Food;
    public GameObject Plate;
    public GameObject Passenger;

    void Start()
    {
        canPickup = false;
        hasItem = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Passenger"))
        {
            //if (hasItem == true)
            Debug.Log("Collide");
            canPickup = false;
            Food.transform.position = Plate.transform.position + Plate.transform.forward;
            hasItem = false;
            

            //if (hasItem == false)
            //{
            //    canPickup = false;
            //    transform.position = Vector3.zero;

            //}
        }
    }
}
