using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health = 3;
    public GameManager gm;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            EnemyHurt();
        }
    }
    
    public void EnemyHurt()
    {
        if(health > 1)
        {
            health--;
        }
        else if(health <= 1)
        {
            gm.currentPoints += 50;
            Destroy(this.gameObject);
        }
    }
}
