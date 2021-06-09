﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, Attackable
{
    public float moveSpeed = 0.1f; //bullet movement speed

    public int AttackDamage => 50;

    void Start()
    {
        if (moveSpeed < 0)
            transform.localScale = new Vector3(-1, 1, 1);
        if (moveSpeed > 0)
            transform.localScale = new Vector3(1, 1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        switch (GameGlobalController.gameState)
        {
            case GameGlobalController.GameState.Lobby:
                Destroy(gameObject);
                break;
            case GameGlobalController.GameState.Playing:
                transform.Translate(moveSpeed, 0, 0);
                break;
        }
    }

    /*void OnTriggerEnter2D(Collider2D col)
    {
        switch (col.tag)
        {
            case "Walls":
                Destroy(gameObject);
                break;
            case "Grass":
                Destroy(col.gameObject);
                Destroy(gameObject);
                break;
        }
    }*/
}