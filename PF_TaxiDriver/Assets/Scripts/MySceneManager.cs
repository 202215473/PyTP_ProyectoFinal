using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : Singleton<MySceneManager>
{
    public void ChangeScenes(string newScene)
    {
        SceneManager.LoadScene(newScene);
    }
}
