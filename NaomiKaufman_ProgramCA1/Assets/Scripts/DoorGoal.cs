using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorGoal : MonoBehaviour
{

    public GameManager gm;

    [SerializeField] public GameObject winScreen;
    private void OnTriggerEnter(Collider other)
    {
        
            if (other.gameObject.CompareTag("Player"))
            {
            gm.currentScore = gm.currentPoints;
            
            if(gm.currentScore > gm.bestScore)
            {
                gm.bestScore = gm.currentScore;
            }

            winScreen.SetActive(true);

            gm.SaveFromSceneToManager();
            gm.ResetEnvironmentStatus();
            gm.ResetGameStatus();
            gm.ResetStatisticStatus();
            

            }

    }
}
