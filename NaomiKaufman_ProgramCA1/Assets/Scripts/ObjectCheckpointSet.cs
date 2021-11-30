using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCheckpointSet : MonoBehaviour
{
    //Variable
    Vector3 StartPos; //hold objects starting position

    public void Start()
    {
         StartPos = GetComponent<Transform>().position; //Get the objects starting position
    }

    public void gotoPos()
    {
        transform.position = StartPos; //Return the object to where it started
    }
}
