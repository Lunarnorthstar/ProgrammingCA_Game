using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Data", order = 1)]

public class GameStatus_SO : ScriptableObject
{
    public int coins;
    public int ammo;
    public int health;

    public Vector3 currentCheckpointPos;

}
