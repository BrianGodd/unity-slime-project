﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class GameGlobalController : MonoBehaviour
{
    public enum GameState { Init, Start, MenuPrepare, Darking, Brightening, Playing, Pause, End, Lobby, Animation, Shaking, Lighting, Unlighting, Interval};
    public static int currentLevel = 0;

    static Text titleText;
    static Text subtitleText;
    static Image brand;
    static Image cb;

    public GameObject lobbyCanvas;
    public GameObject passCanvas;
    public GameObject deadCanvas;
    public GameObject slimeHealthCanvas;

    public GameObject[] levelPrefab;

    public GameObject slimePrefab;
    static GameObject slimeInstance = null;
    public GameObject enemySpawnerPrefab;
    public GameObject brickPrefab;
    public GameObject portalPrefab;
    public GameObject floorPrefab;
    public GameObject instructPrefab;
    public static GameState gameState;

    SpriteRenderer background;
    public Sprite[] gameBackground;
    public Sprite[] menuBackground;
    public static bool battle = false;
    float delta = 0;

    // Start is called before the first frame update
    void Start()
    {
        gameState = GameState.MenuPrepare;
        brand = GameObject.Find("Brand").GetComponent<Image>();
        cb = GameObject.Find("Cb").GetComponent<Image>();
        titleText = GameObject.Find("Title").GetComponent<Text>();
        subtitleText = GameObject.Find("Subtitle").GetComponent<Text>();
        background = GetComponent<SpriteRenderer>();
        slimeInstance = Instantiate(slimePrefab);
    }

    // Update is called once per frame
    void Update()
    {
        brand.gameObject.SetActive(gameState == GameState.Lobby && currentLevel > 0);
        lobbyCanvas.SetActive((/*gameState == GameState.Brightening || */gameState == GameState.Lobby) && currentLevel == 0 && !battle);
        passCanvas.SetActive(gameState == GameState.End && !battle);
        deadCanvas.SetActive(gameState == GameState.End && battle);
        slimeHealthCanvas.SetActive(gameState == GameState.Playing || gameState == GameState.Animation);
        cb.gameObject.SetActive(gameState == GameState.Animation);
        switch (gameState)
        {
            case GameState.Init:
                titleText.text = "";
                subtitleText.text = "";
                battle = true;
                // sprite.sprite = menuBackground[0];
                gameState = GameState.Darking;
                break;
            case GameState.Lighting:
                delta += Time.deltaTime;
                if(delta >= 1)
                {
                    delta = 0;
                    gameState = GameState.Unlighting;
                }
                break;
            case GameState.Unlighting:
                delta += Time.deltaTime;
                if (delta >= 1)
                {
                    delta = 0;
                    gameState = battle ? GameState.Playing : GameState.Lobby;
                }
                break;
            case GameState.Darking:
                delta += Time.deltaTime;
                if (delta >= 1)
                {
                    delta = 0;
                    if (battle)
                        gameState = GameState.Start;
                    else
                        gameState = GameState.MenuPrepare;
                }
                break;
            case GameState.MenuPrepare:
                if(currentLevel != 0){
                    slimeInstance.GetComponent<Transform>().position = new Vector2(1f, 5f);
                    Instantiate(floorPrefab);
                    Instantiate(instructPrefab).GetComponent<Transform>().position = new Vector2(80,8f);
                }
                gameState = GameState.Brightening;
                break;
            case GameState.Start:
                SlimeLifeCanvas.life = 6;
                if (currentLevel < LevelVarity.spawnpoint.Count)
                {
                    slimeInstance.GetComponent<Transform>().position = LevelVarity.spawnpoint[currentLevel];
                    Instantiate(levelPrefab[currentLevel]);
                }
                gameState = GameState.Brightening;
                break;
            case GameState.Brightening:
                background.sprite = battle ? gameBackground[currentLevel] : menuBackground[currentLevel];
                delta += Time.deltaTime;
                if (delta >= 1)
                {
                    delta = 0;
                    gameState = battle ? GameState.Playing : GameState.Lobby;
                }
                break;
            case GameState.Playing:
                if (Input.GetKey(KeyCode.Escape))
                {
                    titleText.text = "Game Paused.";
                    subtitleText.text = "Press Q to resume.";
                    gameState = GameState.Pause;
                }
                if (slimeInstance == null) gameState = GameState.End;
                break;
            case GameState.Pause:
                if (Input.GetKey(KeyCode.Q))
                {
                    titleText.text = "";
                    subtitleText.text = "";
                    gameState = GameState.Playing;
                }
                break;
            case GameState.End:
                break;
            case GameState.Lobby:
                break;
            case GameState.Animation:
                break;
            case GameState.Shaking:
                break;
        }
    }

    public static void GameReset()
    {
        gameState = GameState.Init;
    }

    public static void WinPass()
    {
        battle = false;
        gameState = GameState.End;
    }

    public static bool isPlaying { get => gameState == GameState.Playing; }
}
