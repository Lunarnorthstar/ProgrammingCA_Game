using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointCollider : MonoBehaviour 
{
    //Variables
    static public Vector3 newPos; //Position to go to
    public GameObject onParticles; //The particles to turn on when activated


    private void OnTriggerEnter(Collider other) //When you enter the trigger
    {   
        
        if (other.gameObject.CompareTag("Player")) //Check if they are the player (By tag)
        {
           newPos = GameObject.Find("Player").transform.position; //Get the player's Position and make it the new return position

            other.SendMessage("CPSet"); //Set the CheckPoint
            onParticles.SetActive(true); //Turn on Particles to show its on
            
        }

    }

}
