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

    public bool pendingEve;

    public GameObject pickupPrompt;

    bool eveQuestStarted;
    bool hasRetrievedSoup;

    void Awake()
    {
        Instance = this;
    }
    
    void Start()
    {
        canPickup = false;
        hasItem = false;

        hasRetrievedSoup = ProfileSystem.Get<bool>(ProfileSystem.Variable.RetrievedBroccoliSoup);
        eveQuestStarted = ProfileSystem.Get<bool>(ProfileSystem.Variable.EveQuestStarted);
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

            if (pendingEve)
            {
                Destroy(objectIWantToPickup);
            }

            if (TrainGameAnalytics.instance != null)
            {
                TrainGameAnalytics.instance.RecordGameEvent("drop",
                    new Dictionary<string, object> { { "location", gameObject.transform.position } });
            }
        }

        hasRetrievedSoup = ProfileSystem.Get<bool>(ProfileSystem.Variable.RetrievedBroccoliSoup);
        eveQuestStarted = ProfileSystem.Get<bool>(ProfileSystem.Variable.EveQuestStarted);

        // pickup
        if (canPickup)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (objectIWantToPickup.GetComponent<FoodManager>() && SpecialPassengerQuests.Instance != null)
                {
                    SpecialPassengerQuests.Instance.lastFoodPickedUp = objectIWantToPickup.GetComponent<FoodManager>().foodType;
                }
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

                if (eveQuestStarted)
                {
                    hasRetrievedSoup = true;
                    ProfileSystem.Set(ProfileSystem.Variable.RetrievedBroccoliSoup, true);
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
                
        if (other.gameObject.GetComponent<SpecialPassengerQuests>())
        {
            pendingEve = true;
        }

        if (other.gameObject.CompareTag("Eve"))
        {
            pickupPrompt.SetActive(true);
            if (MapTest.Instance != null)
            {
                MapTest.Instance.isEve = true;
            }
        }

        if (other.gameObject.CompareTag("Map"))
        {
            pickupPrompt.SetActive(true);
            if (MapTest.Instance != null)
            {
                MapTest.Instance.isMap = true;
            }
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
        
        if (other.gameObject.GetComponent<SpecialPassengerQuests>())
        {
            pendingEve = false;
        }

        if (other.gameObject.CompareTag("Eve"))
        {
            pickupPrompt.SetActive(false);
            if (MapTest.Instance != null)
            {
                MapTest.Instance.isEve = false;
            }
        }

        if (other.gameObject.CompareTag("Map"))
        {
            pickupPrompt.SetActive(false);
            if (MapTest.Instance != null)
            {
                MapTest.Instance.isMap = false;
            }
        }
    }
}