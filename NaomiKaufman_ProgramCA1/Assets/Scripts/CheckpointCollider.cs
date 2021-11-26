using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointCollider : MonoBehaviour 
{

    PlayerCheckpointSet PlayerCheck;
    public float X, Y, Z;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag("Player")) 
        {
            X = GameObject.Find("Player").transform.position.x; 
            Y = GameObject.Find("Player").transform.position.y; 
            Y = GameObject.Find("Player").transform.position.z;

            PlayerCheck.CPSet(X, Y, Z);
            
        }
    }
}
