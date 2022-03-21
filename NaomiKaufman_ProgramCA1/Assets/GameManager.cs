using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public GameObject playerWaypointPos;
    public UnityEvent<string> addAmmo;
    public UnityEvent<string> addCoins;
    public UnityEvent<string> addDistance;

    private Vector3 startPos;
    private int ammo;
    private int coins;
    private float distance;

    private void Start()
    {
        ammo = 10;
        coins = 0;
        UpdateUI();

    }

    private void Update()
    {
        distance = Mathf.RoundToInt(Vector3.Distance(playerWaypointPos.transform.position, player.transform.position));
        UpdateUI();
    }

    public void Restart()
    {
        Scene scene = SceneManager.GetActiveScene(); 
        SceneManager.LoadScene(scene.name);
    }

    public void AddAmmo(int ammoAmt)
    {
        ammo += ammoAmt;
        UpdateUI();
    }
    public void AddCoins(int coinAmt)
    {
        coins += coinAmt;
        UpdateUI();
    }

    public void AddDistance()
    {
        distance = Mathf.RoundToInt(Vector3.Distance(playerWaypointPos.transform.position, player.transform.position));
    }

    private void UpdateUI()
    {
        addAmmo.Invoke(ammo.ToString());
        addCoins.Invoke(coins.ToString());
        addDistance.Invoke(distance.ToString());
    }

    
    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void UnpauseGame()
    {
        Time.timeScale = 1;
    }
    
}