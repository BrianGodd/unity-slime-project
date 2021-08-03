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
        switch (Game.gameState)
        {
            case Game.GameState.DarkFadeOut:
                Destroy(gameObject);
                break;
            case Game.GameState.Lobby:
                if (Input.GetKey(KeyCode.G) && trigger)
                {
                    if(!Instruction.isNews)
                    {
                        animator.Play("gotoportal");
                        MainCameraHandler.allSound = 8;
                        Slime.instance.disappear();
                    }
                    else
                    {
                        MainCameraHandler.allSound = 12;
                        DialogBoxHandler.advice(0,1);
                    }
                }
                if (Anim2 == true)
                {
                    Anim2 = false;
                    trigger = false;
                    Game.StartNewLevel();
                }
                break;


        }

    }
    void OnTriggerStay2D(Collider2D col)
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
