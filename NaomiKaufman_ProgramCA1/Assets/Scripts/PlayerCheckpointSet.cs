using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCheckpointSet : MonoBehaviour 
{
    //Variable
    Vector3 playerPos;

    public void Start()
    {
        playerPos = GameObject.Find("Player").transform.position; //Get the OG starting position in case he dies before a checkpoint
    }
    public void CPSet() 
    {
        playerPos = CheckpointCollider.newPos; //Update the position 
    }
  
    public void gotoCP() 
    {
        transform.position = playerPos; //Return the player to the checkpoint
    }
}
