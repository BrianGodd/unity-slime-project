﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerHandler : MonoBehaviour
{
    public GameObject enemyPrefab;

    float timeFromLastSpawn = 0;
    readonly float timeBetweenSpawnMin = 2f;
    readonly float timeBetweenSpawnMax = 5f;
    public int defDirection = 1;
    public bool isActive = true;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        switch (Game.gameState)
        {
            case Game.GameState.Playing:
                if (isActive)
                {
                    timeFromLastSpawn += Time.deltaTime;
                    if (timeFromLastSpawn >= Random.Range(timeBetweenSpawnMin, timeBetweenSpawnMax))
                    {
                        GameObject enemy = Instantiate(enemyPrefab);
                        enemy.transform.SetParent(transform);
                        enemy.transform.position = transform.position;
                        enemy.GetComponent<Entity>().entityDirection = defDirection;
                        timeFromLastSpawn = 0;
                        isActive = false;
                    }
                }
                break;
            case Game.GameState.End:
                Destroy(gameObject);
                break;
        }
    }
}
