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
    public GameObject gameOverMessage;
    public event Action resumeGame;

    void Start()
    {
        pauseMenu.SetActive(false);
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
        pauseMenu.SetActive(true);
    }

    private void EndGame()
    {
        gameOverMessage.SetActive(true);
        StartCoroutine(Sleep(3));
        QuitGame();
    }

    public IEnumerator  Sleep(int seconds2Sleep)
    {
        yield return new WaitForSeconds(seconds2Sleep);
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        resumeGame.Invoke();
    }

    public void QuitGame()
    {
        string newScene = "InitialScene";
        ChangeScenes(newScene);
    }
}
