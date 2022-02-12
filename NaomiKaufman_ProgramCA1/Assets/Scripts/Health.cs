using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    //Variables
    [Header("Health Values")]
    public int health; //Current health
    public int maxHealth; //Max health

    [Header("Image Variables")]
    public Image[] hearts; //Array of heart images
    public Sprite fullHeart; //image when full
    public Sprite emptyHeart; //Image when empty

    public float hurtTimer = 1;
    float hurtCooldown;

    private void Start()
    {
        hurtCooldown = 0;
    }

    void Update() //EVery frame
    {
        if(health <= 0) //If health is less than or equal to 0 (none)
        {
            Respawn(); //Respawen the player
        }
        if(health > maxHealth) //If health somehow is pushed over the max
        {
            health = maxHealth; //Set it to the max
        }

        for(int i = 0; i < hearts.Length; i++) //For the length of the array
        {
            if(i < health) //if array slot less than current health
            {
                hearts[i].sprite = fullHeart; //make a fullheart sprite in the array
            }
            else //if it isnt
            {
                hearts[i].sprite = emptyHeart; //Make an empty heart in the array
            }

            if(i < maxHealth) //If the array slot is less than max
            {
                hearts[i].enabled = true; //keep enabling hearts
            }
            else //if it isnt
            {
                hearts[i].enabled = false; //dont enable any more hearts
            }

        }

        
        hurtCooldown -= Time.deltaTime;
    }

    public void DealDamage() //Deals damage
    {
        if (hurtCooldown <= 0)
        {
            health--; //Lower health by 1

            GetComponentInChildren<Animator>().SetTrigger("Hurt");
            hurtCooldown = hurtTimer;
        }

    }

    public void Respawn() //Respawns player
    {
        SendMessage("gotoCP"); //return to Checkpoint
        health = maxHealth; //make the health max again

    }
}
