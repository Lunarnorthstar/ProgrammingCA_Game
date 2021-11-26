using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointCollider : MonoBehaviour 
{
    static public Vector3 newPos;

    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag("Player")) 
        {
           newPos = GameObject.Find("Player").transform.position;

            other.SendMessage("CPSet");
        }
    }
}
