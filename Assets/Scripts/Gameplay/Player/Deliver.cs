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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Passenger")
        {
            if (hasItem == true)
            {
                canPickup = false;
                Food.transform.position = Plate.transform.position + Plate.transform.forward;
                hasItem = false;
            }

            //if (hasItem == false)
            //{
            //    canPickup = false;
            //    transform.position = Vector3.zero;

            //}
        }
    }
}
