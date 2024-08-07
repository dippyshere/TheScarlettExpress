using UnityEngine;
using UnityEngine.Serialization;

public class Pickup : MonoBehaviour
{
    public GameObject myHands;

    //public AudioSource throwAudio;
    //public AudioSource pickupAudio;

    public bool canPickup;
    [FormerlySerializedAs("ObjectIWantToPickup")] public GameObject objectIWantToPickup;
    public bool hasItem;

    public float throwForce = 600;
    public float dropForce = 0;
    public float throwMulti;

    public GameObject pickupPrompt;

    Vector3 _objectPos;
    public float distance;

    public ParticleSystem chargePS;

    ThrowUI _throwing;
    [SerializeField] GameObject throwUi;

    [Header("feeding temporary awawawawa")]
    public GameObject pendingPassenger;

    // Start is called before the first frame update
    private void Start()
    {
        canPickup = false;
        hasItem = false;
        throwMulti = 1;
        throwForce = 150;

        _throwing = throwUi.GetComponent<ThrowUI>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown("e") && hasItem)
        {
            objectIWantToPickup.transform.parent = null;
            hasItem = false;
            _throwing.throwUI = false;
            if (objectIWantToPickup.GetComponent<FoodManager>())
            {
                objectIWantToPickup.GetComponent<FoodManager>().stoveController.PlacedFood();
            }
            if (pendingPassenger != null)
            {
                pendingPassenger.GetComponent<PassengerController>().FeedPassenger(objectIWantToPickup.GetComponent<FoodManager>().foodType);
                objectIWantToPickup.transform.SetPositionAndRotation(pendingPassenger.GetComponent<PassengerController>().plateTransform.position, pendingPassenger.GetComponent<PassengerController>().plateTransform.rotation);
                objectIWantToPickup.GetComponent<Rigidbody>().isKinematic = true;
                objectIWantToPickup.tag = "Untagged";
                objectIWantToPickup.transform.parent = pendingPassenger.GetComponent<PassengerController>().plateTransform;
                pendingPassenger = null;
                canPickup = false;
                pickupPrompt.SetActive(false);
            }
            else
            {
                objectIWantToPickup.GetComponent<Rigidbody>().isKinematic = false;
            }
        }

        if (canPickup)
        {          
            if (Input.GetKeyDown("e"))
            {
                objectIWantToPickup.GetComponent<Rigidbody>().isKinematic = true;
                objectIWantToPickup.transform.position = myHands.transform.position;
                objectIWantToPickup.transform.parent = myHands.transform;
                hasItem = true;
                //pickupAudio.Play();
                _throwing.throwUI = true;
                pickupPrompt.SetActive(false);
            }
        }

        if (Input.GetKeyDown("f") && hasItem)
        {
            //StartCoroutine(ThrowMulti());
            chargePS.Play();
        }

        if (Input.GetKeyUp("f") && hasItem)
        {
            //StopCoroutine(ThrowMulti());
            throwForce = throwForce * throwMulti;
            objectIWantToPickup.GetComponent<Rigidbody>().isKinematic = false;
            objectIWantToPickup.transform.parent = null;
            objectIWantToPickup.GetComponent<Rigidbody>().AddForce(myHands.transform.forward * throwForce);
            Debug.Log(throwForce);
            //throwAudio.Play();
            hasItem = false;
          
            chargePS.Stop();
            throwForce = 150;
            throwMulti = 1;
            _throwing.throwUI = false;
        }
    }
    private void OnTriggerEnter(Collider other)
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

        //if (other.gameObject.CompareTag("Chihuahua"))
        //{
        //    pickupPrompt.SetActive(true);
        //}

        //if (other.gameObject.CompareTag("Cash"))
        //{
        //    pickupPrompt.SetActive(true);
        //}
    }
    private void OnTriggerExit(Collider other)
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

        //if (other.gameObject.CompareTag("Chihuahua"))
        //{
        //    pickupPrompt.SetActive(false);
        //}

        //if (other.gameObject.CompareTag("Cash"))
        //{
        //    pickupPrompt.SetActive(false);
        //}
    }
}
