using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    


    
    private void OnTriggerEnter(Collider other) //if you enter the trigger
    {

        if (other.gameObject.CompareTag("Player")) //and are the player
        {
           
                other.SendMessage("DealDamage"); //hurt them
                
            
        }
    }


}
