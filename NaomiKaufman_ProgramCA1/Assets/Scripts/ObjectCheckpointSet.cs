using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCheckpointSet : MonoBehaviour
{

    Vector3 StartPos;

    public void Start()
    {
         StartPos = GetComponent<Transform>().position;
    }

    public void gotoPos()
    {
        transform.position = StartPos;
    }
}
