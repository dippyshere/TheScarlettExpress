using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeleport : MonoBehaviour
{
    public Transform carriage1Go;
    public Transform carriage2Go;
    public Transform carriage3Go;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Go1()
    {
        transform.position = carriage1Go.transform.position;
        transform.rotation = carriage1Go.transform.rotation;
    }

    public void Go2()
    {
        transform.position = carriage2Go.transform.position;
        transform.rotation = carriage2Go.transform.rotation;
    }

    public void Go3()
    {
        transform.position = carriage3Go.transform.position;
        transform.rotation = carriage3Go.transform.rotation;
    }
}
