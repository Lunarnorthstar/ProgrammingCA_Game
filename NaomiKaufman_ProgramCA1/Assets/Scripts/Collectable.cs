using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{

    [Header("Setup Variables")]
    public Collider playerCheck;
    public GameManager gameManager;

    public int ammountToGive = 1;

    [Header("Type Of Collectable")]
    public bool isAmmo;
    public bool isCoin;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            
            if (isAmmo == true)
            {
                gameManager.AddAmmo(ammountToGive);
                Destroy(gameObject);
            }
            if (isCoin == true)
            {
                gameManager.AddCoins(ammountToGive);
                Destroy(gameObject);
            }
        }
    }

}