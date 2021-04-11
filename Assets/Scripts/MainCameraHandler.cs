﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraHandler : MonoBehaviour
{
    public AudioClip[] entityclip= new AudioClip[5];
    public AudioClip[] backgroundclip= new AudioClip[5];
    public AudioClip[] lobbyclip= new AudioClip[2];
    public AudioSource audiosource;
    public static int allSound = 0;
    public static Vector3 targetPosition;
    public float cameraSpeedFactor = 10;
    bool music=false;
    public GameObject clickPrefab;

    Vector2 scrCtrPos;
    SpriteRenderer backgroundSpriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        audiosource = GetComponent<AudioSource>();
        scrCtrPos = new Vector2(Screen.width / 2, Screen.height / 2);
        backgroundSpriteRenderer = GameObject.Find("Background").GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        //this.delta+=Time.deltaTime;
        switch(GameGlobalController.gameState){
            case GameGlobalController.GameState.Start:
                targetPosition = new Vector3(48.0f, 32.0f, -10f);
                this.music=false;
                audiosource.Stop();
                break;
            case GameGlobalController.GameState.MenuPrepare:
                targetPosition = new Vector3(48.0f, 32.0f, -10f);
                this.music=false;
                audiosource.Stop();
                break;
            case GameGlobalController.GameState.Playing:
                if(!this.music){
                    audiosource.clip = backgroundclip[GameGlobalController.currentLevel];
                    audiosource.loop = true;
                    audiosource.Play();
                    this.music=true;
                }
                break;
            case GameGlobalController.GameState.Lobby:
                if(!this.music){
                    audiosource.clip = lobbyclip[GameGlobalController.currentLevel];
                    audiosource.loop = true;
                    audiosource.Play();
                    this.music=true;
                }
                break;
        }
        if(allSound>0){
            audiosource.PlayOneShot(entityclip[allSound]);
            allSound=0;
        }
        Vector3 mPos = Input.mousePosition;
        float camera_x = targetPosition.x + (mPos.x - scrCtrPos.x) / Screen.width;
        float camera_y = targetPosition.y + (mPos.y - scrCtrPos.y) / Screen.height;
        if(Input.GetMouseButtonDown(0)){
            Instantiate(clickPrefab).GetComponent<Transform>().position = new Vector2(mPos.x/scrCtrPos.x*Camera.main.orthographicSize * Camera.main.aspect,mPos.y/scrCtrPos.y*Camera.main.orthographicSize);
        }
        if (camera_x < Camera.main.orthographicSize * Camera.main.aspect)
            camera_x = Camera.main.orthographicSize * Camera.main.aspect;
        if (camera_x > backgroundSpriteRenderer.bounds.size.x - Camera.main.orthographicSize * Camera.main.aspect)
            camera_x = backgroundSpriteRenderer.bounds.size.x - Camera.main.orthographicSize * Camera.main.aspect;
        if (camera_y < Camera.main.orthographicSize)
            camera_y = Camera.main.orthographicSize;
        if (camera_y > backgroundSpriteRenderer.bounds.size.y - Camera.main.orthographicSize)
            camera_y = backgroundSpriteRenderer.bounds.size.y - Camera.main.orthographicSize;
        Vector3 newTarget = new Vector3(camera_x, camera_y, targetPosition.z);
        transform.position = Vector3.MoveTowards(transform.position, newTarget, Vector3.Distance(transform.position, newTarget) * cameraSpeedFactor / 100);
    }
}
