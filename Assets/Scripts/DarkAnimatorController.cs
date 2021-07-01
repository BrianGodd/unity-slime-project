﻿using UnityEngine;

public class DarkAnimatorController : MonoBehaviour
{
	public GameObject slimePrefab, dragonPrefab;
	public static Animator animator;
	SpriteRenderer spriteRenderer;
	public static bool start = true;
	GameObject loading, background;
	public Sprite[] Image;
	public Behaviour flareLayer;

	void Start()
	{
		loading = GameObject.Find("Loading...");
		background = GameObject.Find("Background2");
		animator = GetComponent<Animator>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		flareLayer = (Behaviour)Camera.main.GetComponent ("FlareLayer");
	}

	void Update()
	{
		switch (GameGlobalController.gameState)
		{
			case GameGlobalController.GameState.StartGame:
				if(start)
				{
					background.SetActive(false);
					loading.SetActive(false);
					animator.Play("startgame");
					start = false;
				}
				break;
			case GameGlobalController.GameState.Loading:
				switch(GameGlobalController.storystate)
				{
					case 1:
						Slime.normal();
						loading.SetActive(true);
						Slime.animator.Play("load1");
						Slime.transform.position = new Vector3(43, 14, 0);
						GameGlobalController.storystate = 2;
						break;
					case 3:
						loading.SetActive(false);
						GameGlobalController.gameState = GameGlobalController.GameState.StartStory;
						animator.speed = 1;
						break;
				}
				if(GameGlobalController.storystate == 0)
				{
					if(GameGlobalController.currentLevel == 0)  loadIn();
					else if(start)
					{
						Slime.normal();
						loading.SetActive(true);
						if(GameGlobalController.battle)
						{
							Slime.animator.Play("load1");
							animator.Play("loadgame");
						}
						else  
						{
							Slime.animator.Play("load2");
							animator.Play("loadlobby");
						}	
						Slime.transform.position = new Vector3(46, 14, 0);
						start = false;
					}
				}
				break;
			case GameGlobalController.GameState.StartStory:
				switch(GameGlobalController.storystate)
				{
					case 5:
					case 7:
						animator.speed = 1;
						break;
				}
				break;
			case GameGlobalController.GameState.Darking:
				spriteRenderer.color = Color.HSVToRGB(0, 0, 0);
				animator.Play("black");
				break;
			case GameGlobalController.GameState.Brightening:
				spriteRenderer.color = Color.HSVToRGB(0, 0, 0);
				animator.Play("light");
				break;
			case GameGlobalController.GameState.Lighting:
				spriteRenderer.color = Color.HSVToRGB(0, 0, 100);
				animator.Play("black");
				break;
			case GameGlobalController.GameState.Unlighting:
				spriteRenderer.color = Color.HSVToRGB(0, 0, 100);
				animator.Play("light");
				break;
		}
	}

	void start1()
	{
		animator.speed = 0;
		Slime.transform.position = new Vector3(-3, 11, 0);
		Slime.animator.Play("startjump");
	}

	void start3()
	{
		GameGlobalController.gameState = GameGlobalController.GameState.MenuPrepare;
		GameObject.Find("StartScene").SetActive(false);
	}

	void loadIn()
	{
		loading.SetActive(false);
		GameGlobalController.gameState = GameGlobalController.GameState.Brightening;
	}

	void story1()
	{
		animator.speed = 0;
		GameGlobalController.storyeffect = 1;
		background.SetActive(true);
		background.GetComponent<SpriteRenderer>().sprite = Image[0];
	}

	void story2()
	{
		animator.speed = 0;
		Instantiate(dragonPrefab).GetComponent<Transform>().position = new Vector3(60, 50 ,0);
	}

	void clear()
	{
		flareLayer.enabled = false;
		GameGlobalController.cleareffect = true;
		background.GetComponent<SpriteRenderer>().sprite = Image[1];
	}

	void story3()
	{
		GameGlobalController.storystate = 6;
		animator.speed = 0;
		flareLayer.enabled = true;
		GameGlobalController.storyeffect = 2;
	}

	void story4()
	{
		GameGlobalController.storyeffect = 3;
	}

	void story5()
	{
		animator.speed = 0;
	}
}
