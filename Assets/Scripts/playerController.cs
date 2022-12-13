using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class playerController : NetworkBehaviour
{
    // Variables
    [SerializeField] private float moveSpeed;
    [SerializeField] public float walkSpeed;
    [SerializeField] public float runSpeed;

    [SerializeField] private float gravity;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float jumpHeight;

    private Vector3 moveDirection;
    private Vector3 velocity;

    // References
    private CharacterController controller;
    bool isGrounded;
    private Animator anim;
    private AudioHandler Audio;

    private void Start()
    {
        // Define all references
        controller = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
        Audio = GetComponent<AudioHandler>();
    }

    private void Update()
    {
        // If Client is owner of this player, move
        if (IsLocalPlayer) Move();
    }

    private void Move()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float moveZ = Input.GetAxis("Vertical");
        float moveX = Input.GetAxis("Horizontal");

        moveDirection = new Vector3(moveX, 0, moveZ);
        moveDirection = transform.TransformDirection(moveDirection);

        if(moveDirection != Vector3.zero && !Input.GetKey(KeyCode.LeftShift))
        {
            Walk();
        }
        else if(moveDirection != Vector3.zero && Input.GetKey(KeyCode.LeftShift))
        {
            Run();
        }
        else
        {
            Idle();
        }

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        
        moveDirection *= moveSpeed;
        controller.Move(moveDirection * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
    
    private void Idle()
    {
        anim.SetFloat("Speed", 0, 0.1f, Time.deltaTime);
    }

    private void Walk()
    {
        moveSpeed = walkSpeed;
        anim.SetFloat("Speed", 2, 0.1f, Time.deltaTime);
        // Play walking sound to all near players
        Audio.PlayerSound("walk");
    }

    private void Run()
    { 
        moveSpeed = runSpeed;
        anim.SetFloat("Speed", 3, 0.1f, Time.deltaTime);
        // Play running sound to all near players
        Audio.PlayerSound("run");
    }

    private void Jump()
    {
        anim.SetFloat("Speed", 4, 0.1f, Time.deltaTime);
    }
}