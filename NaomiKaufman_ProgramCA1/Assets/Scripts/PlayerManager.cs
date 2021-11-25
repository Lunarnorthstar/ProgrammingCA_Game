using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    PlayerMove newPlayerMove;

    public void Awake()
    {
        newPlayerMove = GetComponent<PlayerMove>();
    }

    private void Update()
    {
        newPlayerMove.HandleInputs();
    }
    private void FixedUpdate()
    {
        newPlayerMove.HandleMovements();
    }
}
