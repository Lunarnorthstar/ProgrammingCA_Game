using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //VARIABLES
    PlayerController playercontrols;
    Vector3 moveDirection;
    Transform cameraObject;
    Rigidbody playerRigidbody;

    [Header("Movement Variables")]
    public Vector2 movementInput;
    public float verticleInput;
    public float horizontalInput;

    [Header("Movement Speeds")]
    public float movementSpeed = 7;
    public float rotationSpeed = 15;

    [Header("Movement Bools")]
    public bool jumpInput;
    public bool isJumping;
    public bool isGrounded;

    [Header("Jumping Variables")]
    public float jumpHeight = 3;
    public float gravity = -15;
    public float inAir;
    public float fallingSpeed;
    public float leapingSpeed;
    public float rayCastHeightOffset = 0;
    public LayerMask groundLayer;

    private void OnEnable()
    {
        if (playercontrols == null)
        {
            playercontrols = new PlayerController();
            playercontrols.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();

            playercontrols.PlayerActions.Jump.performed += i => jumpInput = true;
        }

        playercontrols.Enable();
    }

    private void OnDisable()
    {
        playercontrols.Disable();
    }

    public void Awake ()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        cameraObject = Camera.main.transform;
    }
    

    public void Movement()
    {
         if (isJumping)
        {
            return;
        } 


            moveDirection = cameraObject.forward * verticleInput;
        moveDirection = moveDirection + cameraObject.right * horizontalInput;
        moveDirection.Normalize();
        moveDirection.y = 0;
        moveDirection = moveDirection * movementSpeed;

        Vector3 movementVelocity = moveDirection;
        playerRigidbody.velocity = movementVelocity;
    }

    private void MovementInput ()
    {
        verticleInput = movementInput.y;
        horizontalInput = movementInput.x;
    }

    private void Rotation()
    {

         if (isJumping)
        { 
            return; 
        } 
            

        Vector3 targetDirection = Vector3.zero;
       
        targetDirection = cameraObject.forward * verticleInput;
        targetDirection = targetDirection + cameraObject.right * horizontalInput;
        targetDirection.Normalize();

        targetDirection.y = 0;

        if (targetDirection == Vector3.zero)
        {
            targetDirection = transform.forward;
        }

            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        transform.rotation = playerRotation;
    }

    public void JumpingInput()
    {
        if(jumpInput)
        {
            jumpInput = false;
            Jumping();

        }
        
    }

    public void Jumping()
    {
        if (isGrounded)
        {
            isJumping = true;
            float jumpingVelocity = Mathf.Sqrt(-2 * gravity * jumpHeight);
            Vector3 playerVelocity = moveDirection;
            moveDirection.Normalize();
            playerVelocity.y = jumpingVelocity;
            playerRigidbody.velocity = playerVelocity;

        }
        if (!isGrounded)
        {
            moveDirection = cameraObject.forward * verticleInput;
            moveDirection = moveDirection + cameraObject.right * horizontalInput;
            moveDirection.Normalize();
            moveDirection.y = 0;
            moveDirection = moveDirection * movementSpeed;

            Vector3 movementVelocity = moveDirection;
            playerRigidbody.velocity = movementVelocity;


        }

    }

    public void Falling()
    {
        RaycastHit hit;
        Vector3 rayCastOrigin = transform.position;
        rayCastOrigin.y = rayCastOrigin.y + rayCastHeightOffset;

        if(!isGrounded && !isJumping)
        {
            inAir = inAir + Time.deltaTime;
            playerRigidbody.AddForce(transform.forward * leapingSpeed);
            playerRigidbody.AddForce(Vector3.down * fallingSpeed * inAir);
        }
        if (Physics.SphereCast(rayCastOrigin, 0.2f, Vector3.down, out hit, 0.2f, groundLayer))
        {
            inAir = 0;
            isGrounded = true;
            isJumping = false;
        }
        else
        {
            isGrounded = false;
        }
    }
       

    public void HandleMovements()
    {  
        Movement();
        MovementInput();
        Rotation();
    }
    public void HandleFalling()
    {
        Falling();
    }
    public void HandleJumping()
    {
        JumpingInput();
    }


}
