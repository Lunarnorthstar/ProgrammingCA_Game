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
        moveDirection = cameraObject.forward * verticleInput;
        moveDirection = moveDirection + cameraObject.right * horizontalInput;
        moveDirection.Normalize();
        moveDirection.y = 0;
        moveDirection = moveDirection * movementSpeed;

        Vector3 movementVelocity = moveDirection;
        playerRigidbody.velocity = movementVelocity;
    }

    public void Jumping()
    { 
    }

    private void MovementInput ()
    {
        verticleInput = movementInput.y;
        horizontalInput = movementInput.x;
    }

    private void Rotation()
    {
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

        }
    }

    public void HandleMovements()
    {
        Movement();
        MovementInput();
        Rotation();
    }
    public void HandleInputs()
    {
        MovementInput();
        JumpingInput();
    }

}
