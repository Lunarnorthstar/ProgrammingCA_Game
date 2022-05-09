using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SlingshotController : MonoBehaviour
{

    public GameStatus_SO SOmanager;
    public GameManager gm;

    PlayerController playercontrols;

     Vector2 movementInput;
    float verticleInput;
    float horizontalInput;
    float rotationSpeed = 1;

    float maxBlast = 100;
    public float BlastPower = 5;

    public GameObject Projectile;
    public Transform ShotPoint;

    private bool shoot;
    private bool aiming;

    Transform cameraObject;
    Rigidbody playerRigidbody;
    public InputAction aim;

    

    private void OnEnable ()
    {
        if (playercontrols == null) //if the controls are null
        {
            //set up the controls (two seperate input sections for movement and actions (jumping/shooting)
            playercontrols = new PlayerController();
            playercontrols.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();

            playercontrols.PlayerActions.Shoot.performed += i => shoot = true;
            playercontrols.PlayerActions.Aim.performed += i => aiming = true;
        }

        playercontrols.Enable(); //then enable the controls
    }

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>(); //get the rigidbody
        cameraObject = Camera.main.transform; //get the camera
    }
    private void Update()
    {
        verticleInput = movementInput.y;
        horizontalInput = movementInput.x;

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


        if (shoot && SOmanager.ammo > 0 && gm.paused == false)
        {
            shoot = false;
            GameObject CreatedProjectile = Instantiate(Projectile, ShotPoint.position, ShotPoint.rotation);
            Rigidbody gameObjectsRigidBody = CreatedProjectile.AddComponent<Rigidbody>(); 
            gameObjectsRigidBody.mass = 10; 
            CreatedProjectile.GetComponent<Rigidbody>().velocity = ShotPoint.transform.up * BlastPower;
            BlastPower = 5;
            SOmanager.ammo--;
            GetComponentInParent<Animator>().SetLayerWeight(1, 0);
            GetComponentInParent<Animator>().SetBool("Shooting", false);

        }

        if(Mouse.current.rightButton.isPressed)
        {
                Aim();
        };

    }

    void Aim()
    {
        if(BlastPower < maxBlast && gm.paused == false)
        {
            BlastPower += 0.01f;
        }

        aiming = false;
        
        GetComponentInParent<Animator>().SetLayerWeight(1, 1);
        GetComponentInParent<Animator>().SetBool("Shooting", true);
    }

}
