using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    //Variables
    [Header("Setup Variables")]
    public Collider playerCheck; //The collider used to check for the player
    public GameManager gameManager; //Gamemanager

    public int ammountToGive = 1; //The ammount of ammo or coins to give (can be changed for each version of the collectable)

    [Header("Type Of Collectable")]
    public bool isAmmo; //if its ammo
    public bool isCoin; //if its coins

    private void OnTriggerEnter(Collider other) //if you enter the trigger
    {
        if (other.gameObject.CompareTag("Player")) //and you are the player
        {
            
            if (isAmmo == true) //if ammo is true
            {
                gameManager.AddAmmo(ammountToGive); //give ammo depending on the ammount variable
                Destroy(gameObject); //destroy the collectable
            }
            if (isCoin == true) //if coin is true
            {
                gameManager.AddCoins(ammountToGive); //give ammo depending on the ammount variable
                Destroy(gameObject); //destroy the collectable
            }
        }
    }

}