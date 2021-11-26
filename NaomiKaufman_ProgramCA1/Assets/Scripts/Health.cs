using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [Header("Health Values")]
    public int health;
    public int maxHealth;

    [Header("Image Variables")]
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    void Update()
    {
        if(health <= 0)
        {
            Respawn();
        }
        if(health > maxHealth)
        {
            health = maxHealth;
        }

        for(int i = 0; i < hearts.Length; i++)
        {
            if(i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }

            if(i < maxHealth)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }

        }
    }

    public void DealDamage()
    {
        health--;
    }

    public void Respawn()
    {
        SendMessage("gotoCP");
        health = maxHealth;

    }
}
