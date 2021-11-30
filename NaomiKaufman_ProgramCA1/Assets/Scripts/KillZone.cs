using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KillZone : MonoBehaviour 
{
    //Variables
    private bool active = true; //Is this particular killzone active
    public bool permanent = false; //Is it permanent

    private void OnTriggerEnter(Collider other) //When you enter the trigger
    {
        if (active) //If it is on
        {
            if (other.gameObject.CompareTag("Player")) //Check if player
            {
                
                other.SendMessage("gotoCP"); //If it is return him to his last checkpoint
                other.SendMessage("DealDamage"); //If it is deal a damage
            }

            if (other.gameObject.CompareTag("PushObject")) //If it is a pushable object 
            {
                if (!permanent) //if the killzone is NOT permanent and can be destroyed 
                {
                    gameObject.SetActive(false); //Turn off the killzone
                }

                other.SendMessage("gotoPos"); //Return the object to where it started

            }

            
        }
    }
}