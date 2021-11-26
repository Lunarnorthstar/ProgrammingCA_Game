using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCheckpointSet : MonoBehaviour 
{
    float CPX = 0; 
    float CPY = 0; 
    float CPZ = 0; 
    

    public void CPSet(float checkX, float checkY, float checkZ) 
    {
        CPX = checkX;
        CPY = checkY;
        CPY = checkZ;
    }
  
    public void gotoCP() 
    {
        transform.position = new Vector3(CPX, CPY, CPZ); 
    }
}
