﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class InitialSceneManager : MySceneManager
{
    public void StartGame()
    {
        string newScene = "MainScene";
        ChangeScenes(newScene);
    }
}