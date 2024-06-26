using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowUI : MonoBehaviour
{
    [SerializeField] Transform WPoff;
    [SerializeField] Transform WPon;

    public Transform WP;

    public float moveSpeed = 2f;

    public bool throwUI;

    private void Start()
    {
        WP = WPoff;
        throwUI = false;
    }

    private void Update()
    {
        if(throwUI == false)
        {
            WP = WPoff;
        }
        if (throwUI == true)
        {
            WP = WPon;
        }
        transform.position = Vector3.MoveTowards(transform.position, WP.transform.position, moveSpeed * Time.deltaTime);
    }
}
