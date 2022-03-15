using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health = 3;

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
            Destroy(this.gameObject);
        }
    }
}
