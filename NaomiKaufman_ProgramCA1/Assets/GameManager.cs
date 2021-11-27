using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public UnityEvent<string> addAmmo;
    public UnityEvent<string> addCoins;

    private Vector3 startPos;
    private int ammo;
    private int coins;

    private void Start()
    {
        ammo = 10;
        coins = 0;
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

    private void UpdateUI()
    {
        addAmmo.Invoke(ammo.ToString());
        addCoins.Invoke(coins.ToString());
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