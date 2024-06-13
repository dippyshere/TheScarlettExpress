using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public GameObject myHands;

    //public AudioSource throwAudio;
    //public AudioSource pickupAudio;

    public bool canPickup;
    public GameObject ObjectIWantToPickup;
    public bool hasItem;

    public float throwForce = 600;
    public float dropForce = 0;
    Vector3 objectPos;
    public float distance;

    // Start is called before the first frame update
    void Start()
    {
        canPickup = false;
        hasItem = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (canPickup == true)
        {
            if (Input.GetKeyDown("e"))
            {
                ObjectIWantToPickup.GetComponent<Rigidbody>().isKinematic = true;
                ObjectIWantToPickup.transform.position = myHands.transform.position;
                ObjectIWantToPickup.transform.parent = myHands.transform;
                hasItem = true;
                //pickupAudio.Play();
                //play the animals fly animation
            }

        }
        if (Input.GetKeyDown("f") && hasItem == true)
        {
            ObjectIWantToPickup.GetComponent<Rigidbody>().isKinematic = false;
            ObjectIWantToPickup.transform.parent = null;
            ObjectIWantToPickup.GetComponent<Rigidbody>().AddForce(myHands.transform.forward * throwForce);
            //throwAudio.Play();
            hasItem = false;
        }
        if(Input.GetKeyDown("q") && hasItem == true)
        {
            ObjectIWantToPickup.GetComponent <Rigidbody>().isKinematic = false;
            ObjectIWantToPickup.transform.parent = null;
            hasItem = false;

        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Pickup")
        {
            if (hasItem == false)
            {
                canPickup = true;
                ObjectIWantToPickup = other.gameObject;
            }

        }

    }

    private void OnTriggerExit(Collider other)
    {
        canPickup = false;
    }
}
