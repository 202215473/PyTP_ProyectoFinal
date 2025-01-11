using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class GameState : MonoBehaviour
{
    private static GameState _instance;
    private GameState() { }
    
    private bool playerVsAI;

    public static GameState GetInstance()
    {
        if (_instance == null)
        {
            GameObject singletonObject = new GameObject("GameState");
            _instance = singletonObject.AddComponent<GameState>();
            DontDestroyOnLoad(singletonObject);
        }
        return _instance;
    }

    public bool GetPlayerVsAI()
    {
        return playerVsAI;
    }

    public void SetPlayerVsAI(bool gameState)
    {
        playerVsAI = gameState;
    }
}
