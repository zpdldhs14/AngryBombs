using System;
using UnityEngine;

public class playerMovement : MonoBehaviour
{

    public CharacterController controller;

    public float speed = 5;
    public float gravity = -9.18f;
    public float jumpHeight = 3f;
    public Animator animator;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    private Vector3 velocity;
    private bool isGrounded;
    private bool isMovementEnabled = true;
    private IPlayerState currentState;

    private void Start()
    {
        currentState = new IdleState();
        currentState.EnterState(this);
    }

    public void ChangeState(IPlayerState newState)
    {
        currentState.ExitState(this);
        currentState = newState;
        currentState.EnterState(this);
    }

    public bool IsGrounded()
    {
        bool isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        return isGrounded;
    }

    void Update()
    {
        if(!isMovementEnabled) return;
        
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (Input.GetKey("left shift") && isGrounded)
        {
            speed = 10;
        }
        else
        {
            speed = 5;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);
        animator.SetFloat("Speed", move.magnitude);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
        
        
    }
    public void EnablePlayerContorls()
    {
        isMovementEnabled = true;
    }
    public void DisablePlayerContorls()
    {
        isMovementEnabled = false;
    }
}