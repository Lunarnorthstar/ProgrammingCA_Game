using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCheckpointSet : MonoBehaviour 
{

    public GameStatus_SO SOmanager;

    public void Start()
    {
        //SOmanager.currentCheckpointPos = GameObject.Find("Player").transform.position; //Get the OG starting position in case he dies before a checkpoint
    }
    public void CPSet() 
    {
        SOmanager.currentCheckpointPos = gameObject.transform.position; //Update the position 
    }
  
    public void gotoCP() 
    {
        transform.position = SOmanager.currentCheckpointPos; //Return the player to the checkpoint
    }
}
