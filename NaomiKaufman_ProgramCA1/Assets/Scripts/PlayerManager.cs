using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    PlayerMove PlayerMove;

    public void Awake()
    {
        PlayerMove = GetComponent<PlayerMove>();
    }

    private void Update()
    {
        PlayerMove.HandleFalling();
    }

    private void FixedUpdate()
    {
        PlayerMove.HandleMovements();
        PlayerMove.HandleJumping();
    }
}
