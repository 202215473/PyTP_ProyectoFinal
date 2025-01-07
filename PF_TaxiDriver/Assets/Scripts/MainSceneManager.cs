using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

internal class MainSceneManager : MySceneManager
{
    [SerializeField]  private InputHandler inputHandler;
    [SerializeField] private LifeManager lifeManager;
    public GameObject pauseMenu;
    public GameObject cityMap;
    public GameObject gameOverMessage;

    void Start()
    {
        pauseMenu.SetActive(false);
        cityMap.SetActive(true);
        gameOverMessage.SetActive(false);
    }

    private void OnEnable()
    {
        inputHandler.userPressedSpace += HandleSpacePress;
        lifeManager.gameOver += EndGame;
    }

    private void OnDisable()
    {
        inputHandler.userPressedSpace -= HandleSpacePress;
        lifeManager.gameOver -= EndGame;
    }

    private void HandleSpacePress()
    {
        // TODO: parar el juego
        pauseMenu.SetActive(true);
        cityMap.SetActive(false);
    }

    private void EndGame()
    {
        gameOverMessage.SetActive(true);
        Thread.Sleep(3000);
        QuitGame();
    }

    public void ResumeGame()
    {
        // TODO: permitir al juego continuar
        pauseMenu.SetActive(false);
        cityMap.SetActive(true);
    }

    public void QuitGame()
    {
        string newScene = "InitialScene";
        ChangeScenes(newScene);
    }
}
