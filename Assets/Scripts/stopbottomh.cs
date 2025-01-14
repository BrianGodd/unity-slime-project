﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class stopbottomh : MonoBehaviour
{
    bool showHelp = true;
    public Sprite[] stop = new Sprite[2];
    Game.GameState nowState,prenowState;
    // Start is called before the first frame update
    public void GameStop()
    {
        MainCameraHandler.PlayEntityClip(3);
        if(Game.gameState == Game.GameState.Instruction)
        {
            HelpController.nowpage = 0;
            if(Game.currentLevel == 0 && DialogBoxHandler.dialogID == 5 && showHelp)
            {
                showHelp = false;
                Game.gameState = Game.GameState.Playing;
            }
            else    Game.gameState = Game.GameState.Pause;
        }
        else
        {
            nowState = Game.gameState;
            Game.gameState = Game.GameState.Pause;
        }
    }
    public void GameStart()
    {
        if(nowState == Game.GameState.Dialog)   Game.dstopanim = true;
        MainCameraHandler.PlayEntityClip(3);
        Game.gameState = nowState;
    }
    void Start()
    {
    }
    void Update(){
        if(Game.gameState == Game.GameState.Instruction){
            if(Game.currentLevel == 0 && DialogBoxHandler.dialogID == 5)    nowState = Game.GameState.Playing;
            gameObject.GetComponent<Image>().sprite = stop[1];
        }
        else gameObject.GetComponent<Image>().sprite = stop[0];
    }
}
