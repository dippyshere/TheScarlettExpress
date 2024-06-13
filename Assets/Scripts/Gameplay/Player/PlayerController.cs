using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField, Tooltip("The speed at which the player moves forward.")]
    private float moveSpeed = 5.0f;
    [SerializeField, Tooltip("The speed at which the player turns.")]
    private float turnSpeed = 720.0f;

    [Header("Jump Settings")]
    [SerializeField, Tooltip("The height the player can jump.")]
    private float jumpHeight = 2.0f;

    [Header("Ground Detection")]
    [SerializeField, Tooltip("The radius of the sphere used to check if the player is grounded.")]
    private float groundCheckRadius = 0.3f;
    [SerializeField, Tooltip("The distance from the player's center to the ground check sphere.")]
    private Transform groundCheck;
    [SerializeField, Tooltip("Layers considered as ground.")]
    private LayerMask groundMask;

    private Rigidbody rb;
    private Vector2 inputVector;
    private Vector3 moveDirection;
    private bool isGrounded;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        HandleMovement();
        HandleRotation();
        CheckGroundStatus();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        inputVector = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && isGrounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(Vector3.up * Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y), ForceMode.VelocityChange);
        }
    }

    public void Interact(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Pick up food for da passengers");
        }
    }

    private void HandleMovement()
    {
        moveDirection = Camera.main.transform.forward * inputVector.y + Camera.main.transform.right * inputVector.x;
        moveDirection.y = 0;
        moveDirection.Normalize();

        Vector3 movement = Vector3.MoveTowards(rb.velocity, moveDirection * moveSpeed, Time.smoothDeltaTime * moveSpeed);

        rb.velocity = new Vector3(movement.x, rb.velocity.y, movement.z);
    }

    private void HandleRotation()
    {
        if (moveDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, turnSpeed * Time.deltaTime);
        }
    }

    private void CheckGroundStatus()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundMask);
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
