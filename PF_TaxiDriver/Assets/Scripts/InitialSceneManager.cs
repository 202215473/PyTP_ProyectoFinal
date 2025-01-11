using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class InitialSceneManager : MySceneManager
{
    private static bool numPlayers1;
    public void StartGame(bool playerVsAI)
    {
        GameState.GetInstance().SetPlayerVsAI(playerVsAI);
        string newScene = "MainScene";
        ChangeScenes(newScene);
    }
}
