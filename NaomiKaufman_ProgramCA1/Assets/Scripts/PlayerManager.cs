using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    //Variable
    PlayerMove PlayerMove; //PlayerMove script

    public void Awake() //When the script instance is loaded
    {
        PlayerMove = GetComponent<PlayerMove>(); //Get playermove
    }

    private void Update() //Every Frame
    {
        PlayerMove.HandleFalling(); //Call the falling classes
    }

    private void FixedUpdate() //Every fixed framerate frame
    {
        //Call the classes that require input (Move and Jump)
        PlayerMove.HandleMovements();
        PlayerMove.HandleJumping();
    }
}
