using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KillZone : MonoBehaviour 
{

    PlayerCheckpointSet PlayerCheck;

    private bool active = true; 
    public bool permanent = false;

    private void OnTriggerEnter(Collider other) 
    {
        if (active || permanent) 
        {
            if (other.gameObject.CompareTag("Player"))
            {
                
                other.SendMessage("gotoCP");
                other.SendMessage("DealDamage");
            }

            if (other.gameObject.CompareTag("PushObject")) 
            {
                if (!permanent) 
                {
                    gameObject.SetActive(false); 
                }

                other.SendMessage("gotoPos");

            }

            
        }
    }
}