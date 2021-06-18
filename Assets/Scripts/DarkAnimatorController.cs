﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkAnimatorController : MonoBehaviour
{
	Animator animator;
    SpriteRenderer spriteRenderer;

    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {	
        switch(GameGlobalController.gameState)
		{
		    case GameGlobalController.GameState.Darking:
                spriteRenderer.color = Color.HSVToRGB(0,0,0);
		    	animator.Play("black");
        		break;
        	case GameGlobalController.GameState.Brightening:
                spriteRenderer.color = Color.HSVToRGB(0,0,0);
    			animator.Play("light");
        		break;
			case GameGlobalController.GameState.Lighting:
                spriteRenderer.color = Color.HSVToRGB(0,0,100);
		    	animator.Play("black");
        		break;
			case GameGlobalController.GameState.Unlighting:
                spriteRenderer.color = Color.HSVToRGB(0,0,100);
		    	animator.Play("light");
        		break;
        	}
    }
}
