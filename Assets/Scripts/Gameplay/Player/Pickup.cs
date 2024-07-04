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
    public float throwMulti;


    public GameObject pickupPrompt;

    Vector3 objectPos;
    public float distance;

    public ParticleSystem chargePS;

    ThrowUI throwing;
    [SerializeField] GameObject throwUi;

    [Header("feeding temporary awawawawa")]
    public GameObject pendingPassenger;

    // Start is called before the first frame update
    void Start()
    {
        canPickup = false;
        hasItem = false;
        throwMulti = 1;
        throwForce = 150;

        throwing = throwUi.GetComponent<ThrowUI>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("e") && hasItem == true)
        {
            ObjectIWantToPickup.transform.parent = null;
            hasItem = false;
            throwing.throwUI = false;
            if (pendingPassenger != null)
            {
                pendingPassenger.GetComponent<PassengerController>().FeedPassenger(ObjectIWantToPickup.GetComponent<FoodManager>().foodType);
                ObjectIWantToPickup.transform.SetPositionAndRotation(pendingPassenger.GetComponent<PassengerController>().plateTransform.position, pendingPassenger.GetComponent<PassengerController>().plateTransform.rotation);
                ObjectIWantToPickup.GetComponent<Rigidbody>().isKinematic = true;
                ObjectIWantToPickup.tag = "Untagged";
                ObjectIWantToPickup.transform.parent = pendingPassenger.GetComponent<PassengerController>().plateTransform;
                pendingPassenger = null;
                canPickup = false;
                pickupPrompt.SetActive(false);
            }
            else
            {
                ObjectIWantToPickup.GetComponent<Rigidbody>().isKinematic = false;
            }
        }

        if (canPickup == true)
        {          
            if (Input.GetKeyDown("e"))
            {
                ObjectIWantToPickup.GetComponent<Rigidbody>().isKinematic = true;
                ObjectIWantToPickup.transform.position = myHands.transform.position;
                ObjectIWantToPickup.transform.parent = myHands.transform;
                hasItem = true;
                //pickupAudio.Play();
                throwing.throwUI = true;
            }

        }

        if (Input.GetKeyDown("f") && hasItem == true)
        {
            StartCoroutine(ThrowMulti());
            chargePS.Play();
        }

        if (Input.GetKeyUp("f") && hasItem == true)
        {
            StopCoroutine(ThrowMulti());
            throwForce = throwForce * throwMulti;
            ObjectIWantToPickup.GetComponent<Rigidbody>().isKinematic = false;
            ObjectIWantToPickup.transform.parent = null;
            ObjectIWantToPickup.GetComponent<Rigidbody>().AddForce(myHands.transform.forward * throwForce);
            Debug.Log(throwForce);
            //throwAudio.Play();
            hasItem = false;
          
            chargePS.Stop();
            throwForce = 150;
            throwMulti = 1;
            throwing.throwUI = false;
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
                pickupPrompt.SetActive(true);
            }
        }
        if (other.gameObject.tag == "Passenger")
        {
            if (hasItem && !other.gameObject.GetComponent<PassengerController>().hasBeenFed)
            {
                pendingPassenger = other.gameObject;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Pickup")
        {
            canPickup = false;
            pickupPrompt.SetActive(false);
        }
        if (other.gameObject.tag == "Passenger" && pendingPassenger == other.gameObject)
        {
            pendingPassenger = null;
        }
    }
    IEnumerator ThrowMulti()
    {
        // if throwMulti < (whatever max)
        yield return new WaitForSeconds(1);
        throwMulti++;
        StartCoroutine(ThrowMulti());
    }

}
