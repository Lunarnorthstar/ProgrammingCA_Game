using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointCollider : MonoBehaviour 
{
    static public Vector3 newPos;
    public GameObject onParticles;
    bool isActivated;


    private void OnTriggerEnter(Collider other) 
    {
        
        
        if (other.gameObject.CompareTag("Player")) 
        {
           newPos = GameObject.Find("Player").transform.position;

            other.SendMessage("CPSet");
            isActivated = true;
        }

        if (isActivated)
        {
            onParticles.SetActive(true);
        }
    }

}
