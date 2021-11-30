using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //Variables (There are a lot so i'll abbreviate)

    //the objects needed such as controller and rigidbody
    PlayerController playercontrols;
    Vector3 moveDirection;
    Transform cameraObject;
    Rigidbody playerRigidbody;

    //Variables that concern movement
    [Header("Movement Variables")]
    public Vector2 movementInput;
    public float verticleInput;
    public float horizontalInput;

    //Variables that concern move speed
    [Header("Movement Speeds")]
    public float movementSpeed = 7;
    public float rotationSpeed = 15;

    //Variables that concern bools like is jumping or is grounded
    [Header("Movement Bools")]
    public bool jumpInput;
    public bool isJumping;
    public bool isGrounded;

    //Variables that concern jumping
    [Header("Jumping Variables")]
    public float jumpHeight = 3;
    public float gravity = -15;
    public float inAir;
    public float fallingSpeed;
    public float leapingSpeed;
    public float rayCastHeightOffset = 0;
    public LayerMask groundLayer;

    private void OnEnable() //when the object is active
    {
        if (playercontrols == null) //if the controls are null
        {
            //set up the controls (two seperate input sections for movement and actions (jumping/shooting)
            playercontrols = new PlayerController();
            playercontrols.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();

            playercontrols.PlayerActions.Jump.performed += i => jumpInput = true;
        }

        playercontrols.Enable(); //then enable the controls
    }

    private void OnDisable() //if the object is disabled
    {
        playercontrols.Disable(); //also disable the controls
    }

    public void Awake () //when loaded
    {
        playerRigidbody = GetComponent<Rigidbody>(); //get the rigidbody
        cameraObject = Camera.main.transform; //get the camera
    }
    

    public void Movement() //this concerns just the movement
    {
         if (isJumping) //if the player is currently jumping
        {
            return; //don't run the rest of the code 
        } 

         //using the forward direction of the camera and the input, get the move direction, normalize it, then add it to the velocity to move 
            moveDirection = cameraObject.forward * verticleInput;
        moveDirection = moveDirection + cameraObject.right * horizontalInput;
        moveDirection.Normalize();
        moveDirection.y = 0;
        moveDirection = moveDirection * movementSpeed;

        Vector3 movementVelocity = moveDirection;
        playerRigidbody.velocity = movementVelocity;
    }

    private void MovementInput () //this is JUST the inputs, not the actual movement 
    {
        verticleInput = movementInput.y;
        horizontalInput = movementInput.x;
    }

    private void Rotation() //this concerns just the rotation of the character 
    {

         if (isJumping) //if the player is in mid jump
        { 
            return; //dont do the rest of the code here
        } 
            
         //using the camera forward and input, normalize it, then Slerp between that and the current rotation (via rotationspeed/deltatime) to turn the character  
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

    public void JumpingInput() //this is just the jumping INPUTS 
    {
        if(jumpInput) //if the player presses the jump button
        {
            jumpInput = false; //reset the input to false
            Jumping(); //and call the action 

        }
        
    }

    public void Jumping() //this is the actual jumping code
    {
        if (isGrounded) //if you are on the ground
        {
            isJumping = true; //and you re jumping

            //use gravity and jump height then normilize it and update the velocity!
            float jumpingVelocity = Mathf.Sqrt(-2 * gravity * jumpHeight);
            Vector3 playerVelocity = moveDirection;
            moveDirection.Normalize();
            playerVelocity.y = jumpingVelocity;
            playerRigidbody.velocity = playerVelocity;

        }
        if (!isGrounded) //if you ARENT on the ground
        {
            //return movement control to the player in case they want to control their jump
            moveDirection = cameraObject.forward * verticleInput;
            moveDirection = moveDirection + cameraObject.right * horizontalInput;
            moveDirection.Normalize();
            moveDirection.y = 0;
            moveDirection = moveDirection * movementSpeed;

            Vector3 movementVelocity = moveDirection;
            playerRigidbody.velocity = movementVelocity;


        }

    }

    public void Falling() //falling code
    {

        //use a raycast to check (just instead of a seperate collider)
        RaycastHit hit;
        Vector3 rayCastOrigin = transform.position;
        rayCastOrigin.y = rayCastOrigin.y + rayCastHeightOffset;

        if(!isGrounded && !isJumping) //if you are NOT grounded OR jumping 
        {
            //then fall
            inAir = inAir + Time.deltaTime;
            playerRigidbody.AddForce(transform.forward * leapingSpeed);
            playerRigidbody.AddForce(Vector3.down * fallingSpeed * inAir);
        }
        if (Physics.SphereCast(rayCastOrigin, 0.2f, Vector3.down, out hit, 0.2f, groundLayer)) //if the raycast sees the groundlayer
        {
            //become grounded and not jumping
            inAir = 0;
            isGrounded = true;
            isJumping = false;
        }
        else //if it doesnt 
        {
            //then you arent grounded
            isGrounded = false;
        }
    }
       

    public void HandleMovements() //just a group that lets you call all the move stuff at once
    {  
        Movement();
        MovementInput();
        Rotation();
    }
    public void HandleFalling() //falling has its own thing because it needs update not fixedupdate
    {
        Falling();
    }
    public void HandleJumping() //jumping has its own thing in case i add other actions later!
    {
        JumpingInput();
    }


}
