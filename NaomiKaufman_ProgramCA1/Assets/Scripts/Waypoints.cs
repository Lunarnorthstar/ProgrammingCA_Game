using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Waypoints : MonoBehaviour
{

    public int zOffset = 215;

    public GameObject waypointPrefab;
    Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    
    // Update is called once per frame
    void Update()
    {
        WaypointSpawn();
    }
    

    void WaypointSpawn()
    {
        if(Mouse.current.leftButton.wasPressedThisFrame)
        {
            Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit))
            {
                waypointPrefab.transform.position = hit.point;

                //Instantiate(waypointPrefab, hit.point, Quaternion.identity);
            }
        }
    }

}
