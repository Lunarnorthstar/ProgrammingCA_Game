using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCheckpointSet : MonoBehaviour 
{
    Vector3 playerPos;

    public void Start()
    {
        playerPos = GameObject.Find("Player").transform.position;
    }
    public void CPSet() 
    {
        playerPos = CheckpointCollider.newPos;
    }
  
    public void gotoCP() 
    {
        transform.position = playerPos; 
    }
}
