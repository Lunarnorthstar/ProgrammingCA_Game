using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System.IO;

//STRUCTS
[Serializable]
public struct GameData
{
    public Vector3 playerPos;
    public List<Vector3> enemyPos;
    
}
[Serializable]
public struct StatisticData
{
    public int lastScore;
    public int highScore;
    public int points;
}
[Serializable]
public struct EnvironmentData
{
    public List<Vector3> pushBlocks;
    public Vector3 currentWaypointPos;
}

public class GameManager : MonoBehaviour
{
    public GameStatus_SO SOmanager;

    public GameObject player;
    public GameObject playerWaypointPos;
    public UnityEvent<string> addAmmo;
    public UnityEvent<string> addCoins;
    public UnityEvent<string> addDistance;
    public UnityEvent<string> addPoints;
    public UnityEvent<string> addLastScore;
    public UnityEvent<string> addHighScore;

    private Vector3 startPos;
    
    private float distance;

    public int currentScore;
    public int bestScore;
    public int currentPoints;

    string filePath;
    const string FILE_NAME_GD = "GameDataJSONFORMAT.json";
    const string FILE_NAME_SD = "StatDataJSONFORMAT.json";
    const string FILE_NAME_ED = "EnvirDataJSONFORMAT.json";

    GameData dataStatus;
    StatisticData dataStatistic;
    EnvironmentData dataEnvironment;

    //public static readonly GameManager instance = new GameManager();


   /* private GameManager()
    { }

    public static GameManager Instance
    {
        get
        {
            return instance;
        }
    } */

    private void Start()
    {
        filePath = Application.persistentDataPath;
        dataStatus = new GameData();
        dataStatistic = new StatisticData();
        dataEnvironment = new EnvironmentData();

        Debug.Log(filePath);

        LoadGameStatus();
        UpdateSceneFromManager();


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

    

    public void AddDistance()
    {
        distance = Mathf.RoundToInt(Vector3.Distance(playerWaypointPos.transform.position, player.transform.position));
    }

    public void AddPoints(int pointAmt)
    {
        currentPoints += pointAmt;
        UpdateUI();
    }

    public void AddLastScore(int lastAmt)
    {
        currentScore += lastAmt;
        UpdateUI();
    }

    public void AddHighScore(int highAmt)
    {
        bestScore += highAmt;
        UpdateUI();
    }

    public void UpdateUI()
    {
        addAmmo.Invoke(SOmanager.ammo.ToString());
        addCoins.Invoke(SOmanager.coins.ToString());
        addDistance.Invoke(distance.ToString());
        addPoints.Invoke(currentPoints.ToString());
        addLastScore.Invoke(currentScore.ToString());
        addHighScore.Invoke(bestScore.ToString());
    }


    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void UnpauseGame()
    {
        Time.timeScale = 1;
    }


    public void LoadGameStatus()
    {
        if (File.Exists(filePath + "/" + FILE_NAME_GD))
        {
            string loadedJson = File.ReadAllText(filePath + "/" + FILE_NAME_GD);
            dataStatus = JsonUtility.FromJson<GameData>(loadedJson);
            Debug.Log("File loaded successfully");
        }
        else
        {
            ResetGameStatus();
        }

        if (File.Exists(filePath + "/" + FILE_NAME_SD))
        {
            string loadedJson = File.ReadAllText(filePath + "/" + FILE_NAME_SD);
            dataStatistic = JsonUtility.FromJson<StatisticData>(loadedJson);
            Debug.Log("File loaded successfully");
        }
        else
        {
            ResetStatisticStatus();
        }

        if (File.Exists(filePath + "/" + FILE_NAME_ED))
        {
            string loadedJson = File.ReadAllText(filePath + "/" + FILE_NAME_ED);
            dataEnvironment = JsonUtility.FromJson<EnvironmentData>(loadedJson);
            Debug.Log("File loaded successfully");
        }
        else
        {
            Debug.Log("BAD STUFF");
            ResetEnvironmentStatus();
        }
    }


    public void SaveGameStatus()
    {
        string gameStatusJson = JsonUtility.ToJson(dataStatus);
        string statisticStatusJson = JsonUtility.ToJson(dataStatistic);
        string enviornmentStatusJson = JsonUtility.ToJson(dataEnvironment);

        File.WriteAllText(filePath + "/" + FILE_NAME_GD, gameStatusJson);
        File.WriteAllText(filePath + "/" + FILE_NAME_SD, statisticStatusJson);
        File.WriteAllText(filePath + "/" + FILE_NAME_ED, enviornmentStatusJson);

        Debug.Log("File created and saved");
    }

    public void ResetGameStatus()
    {
        dataStatus.playerPos = new Vector3(0, 0, 0);
        dataStatus.enemyPos = new List<Vector3>() {
                              new Vector3(8.28f, -4.33f, 104.2f),
                              new Vector3(5.4f, -2.109f, 85.19f),
                              new Vector3(-5.92f, -1.11f, 24.64f),
                              new Vector3(-8.05f, -0.16f, 7.58f)
        };

        SaveGameStatus();
        Debug.Log("File not found...Creating");
    }

    public void ResetStatisticStatus()
    {
        dataStatistic.points = 0;

        SaveGameStatus();
        Debug.Log("File not found...Creating");
    }
    public void ResetEnvironmentStatus()
    {
        dataEnvironment.pushBlocks = new List<Vector3>() {
                                    new Vector3(5.76f, 1.6f, 1.11f),
                                    new Vector3(-5.26f, -6.58f, 61.09f),
                                    new Vector3(6.5f, -7.51f, 65.17f)
        };

        dataEnvironment.currentWaypointPos = new Vector3(0.7f, 41f, 135f);

        SaveGameStatus();
        Debug.Log("File not found...Creating");
    }

    public void ResetAllData()
    {
        ResetEnvironmentStatus();
        ResetGameStatus();
        ResetStatisticStatus();
        dataStatistic.highScore = 0;
        dataStatistic.lastScore = 0;

        SOmanager.coins = 0;
        SOmanager.ammo = 10;
        SOmanager.currentCheckpointPos = Vector3.zero;
    }


    // Application Lifecycle Events //

    private void OnApplicationPause(bool pause)
    {
        if(pause)
        {
            SaveGameStatus();
        }
        else
        {
            LoadGameStatus();
        }
    }

    void OnApplicationQuit()
    {
        SaveFromSceneToManager();

        SaveGameStatus();
    }


    public void SaveFromSceneToManager()
    {
        dataStatus.enemyPos.Clear();

        dataStatus.playerPos = GameObject.Find("Player").transform.position;

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < enemies.Length; i++)
        {
            dataStatus.enemyPos.Add(enemies[i].transform.position);
        }

        dataStatistic.points = currentPoints;
        dataStatistic.highScore = bestScore;
        dataStatistic.lastScore = currentScore;


        dataEnvironment.currentWaypointPos = GameObject.Find("PlayerWaypoint").transform.position;
        GameObject[] pushObject = GameObject.FindGameObjectsWithTag("PushObject");
        for (int i = 0; i < pushObject.Length; i++)
        {
            dataEnvironment.pushBlocks.Add(pushObject[i].transform.position);
        }
    }

    public void UpdateSceneFromManager()
    {
        GameObject.Find("Player").transform.position = dataStatus.playerPos;

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].transform.position = dataStatus.enemyPos[i];
        }

        currentPoints = dataStatistic.points;
        bestScore = dataStatistic.highScore;
        currentScore = dataStatistic.lastScore;


        GameObject.Find("PlayerWaypoint").transform.position = dataEnvironment.currentWaypointPos;
        GameObject[] pushObject = GameObject.FindGameObjectsWithTag("PushObject");
        for (int i = 0; i < pushObject.Length; i++)
        {
            pushObject[i].transform.position = dataEnvironment.pushBlocks[i];
        }
    }

}
