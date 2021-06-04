﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portal2_handler : MonoBehaviour
{
    Animator animator;
    bool trigger = false;
    public bool Anim2 = false;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (GameGlobalController.gameState)
        {
            case GameGlobalController.GameState.Darking:
                Destroy(gameObject);
                break;
            case GameGlobalController.GameState.Lobby:
                if (Input.GetKey(KeyCode.G) && trigger)
                {
                    if(!Instruction.isNews)
                    {
                        MainCameraHandler.allSound = 7;
                        animator.Play("gotoportal");
                        Slime.disappear();
                    }
                    else
                    {
                        
                    }
                }
                if (Anim2 == true)
                {
                    Anim2 = false;
                    trigger = false;
                    GameGlobalController.StartNewGame();
                }
                break;


        }

    }
    void OnTriggerEnter2D(Collider2D col)
    {
        switch (col.tag)
        {
            case "Slime":
                trigger = true;
                break;
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        switch (col.tag)
        {
            case "Slime":
                trigger = false;
                break;
        }
    }
}
