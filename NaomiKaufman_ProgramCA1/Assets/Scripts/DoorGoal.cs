using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorGoal : MonoBehaviour
{
    [SerializeField] public GameObject winScreen;
    private void OnTriggerEnter(Collider other)
    {
        
            if (other.gameObject.CompareTag("Player"))
            {

            winScreen.SetActive(true);

            }

    }
}
