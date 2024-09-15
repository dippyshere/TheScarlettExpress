#region

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

#endregion

public class Pickup : MonoBehaviour
{
    [HideInInspector, Tooltip("Singleton instance of the Pickup.")]
    public static Pickup Instance;
    
    Vector3 _objectPos;

    public bool canPickup;

    public bool hasItem;
    public GameObject myHands;

    [FormerlySerializedAs("ObjectIWantToPickup")]
    public GameObject objectIWantToPickup;

    [Header("feeding temporary awawawawa")]
    public GameObject pendingPassenger;

    public GameObject pickupPrompt;

    void Awake()
    {
        Instance = this;
    }
    
    void Start()
    {
        canPickup = false;
        hasItem = false;
    }

    void Update()
    {
        // drop
        if (Input.GetKeyDown(KeyCode.E) && hasItem)
        {
            objectIWantToPickup.transform.parent = null;
            hasItem = false;
            if (objectIWantToPickup.GetComponent<FoodManager>())
            {
                objectIWantToPickup.GetComponent<FoodManager>().stoveController.PlacedFood();
            }

            if (pendingPassenger != null)
            {
                pendingPassenger.GetComponent<PassengerController>()
                    .FeedPassenger(objectIWantToPickup.GetComponent<FoodManager>().foodType);
                objectIWantToPickup.transform.SetPositionAndRotation(
                    pendingPassenger.GetComponent<PassengerController>().plateTransform.position,
                    pendingPassenger.GetComponent<PassengerController>().plateTransform.rotation);
                objectIWantToPickup.GetComponent<Rigidbody>().isKinematic = true;
                objectIWantToPickup.tag = "Untagged";
                objectIWantToPickup.transform.parent =
                    pendingPassenger.GetComponent<PassengerController>().plateTransform;
                pendingPassenger = null;
                canPickup = false;
                pickupPrompt.SetActive(false);
            }
            else
            {
                objectIWantToPickup.GetComponent<Rigidbody>().isKinematic = false;
            }

            if (TrainGameAnalytics.instance != null)
            {
                TrainGameAnalytics.instance.RecordGameEvent("drop",
                    new Dictionary<string, object> { { "location", gameObject.transform.position } });
            }
        }

        // pickup
        if (canPickup)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                objectIWantToPickup.GetComponent<Rigidbody>().isKinematic = true;
                objectIWantToPickup.transform.position = myHands.transform.position;
                objectIWantToPickup.transform.parent = myHands.transform;
                hasItem = true;
                //pickupAudio.Play();
                pickupPrompt.SetActive(false);
                if (TrainGameAnalytics.instance != null)
                {
                    TrainGameAnalytics.instance.RecordGameEvent("pickup",
                        new Dictionary<string, object> { { "location", gameObject.transform.position } });
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pickup"))
        {
            if (hasItem == false)
            {
                canPickup = true;
                objectIWantToPickup = other.gameObject;
                pickupPrompt.SetActive(true);
            }
        }

        if (other.gameObject.CompareTag("Passenger"))
        {
            if (hasItem && !other.gameObject.GetComponent<PassengerController>().hasBeenFed)
            {
                pendingPassenger = other.gameObject;
            }
        }

        if (other.gameObject.CompareTag("Eve"))
        {
            pickupPrompt.SetActive(true);
        }

        if (other.gameObject.CompareTag("Map"))
        {
            pickupPrompt.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Pickup"))
        {
            canPickup = false;
            pickupPrompt.SetActive(false);
        }

        if (other.gameObject.CompareTag("Passenger") && pendingPassenger == other.gameObject)
        {
            pendingPassenger = null;
        }

        if (other.gameObject.CompareTag("Eve"))
        {
            pickupPrompt.SetActive(false);
        }

        if (other.gameObject.CompareTag("Map"))
        {
            pickupPrompt.SetActive(false);
        }
    }
}