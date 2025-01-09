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
    [SerializeField] private InputHandler inputHandler;
    [SerializeField] private LifeManager lifeManager;
    [SerializeField] private GameObject resumeBotton;
    [SerializeField] private GameObject quitBotton;
    [SerializeField] private GameObject gameOverMessage;
    public event Action resumeGame;

    void Start()
    {
        resumeBotton.SetActive(false);
        quitBotton.SetActive(false);
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
        resumeBotton.SetActive(true);
        quitBotton.SetActive(true);
    }

    private void EndGame()
    {
        gameOverMessage.SetActive(true);
        StartCoroutine(HandleGameOverSequence(5f));
    }

    public IEnumerator HandleGameOverSequence(float seconds2Sleep)
    {
        yield return new WaitForSeconds(seconds2Sleep);
        string newScene = "InitialScene";
        ChangeScenes(newScene);
    }

    public void ResumeGame()
    {
        resumeBotton.SetActive(false);
        quitBotton.SetActive(false);
        resumeGame.Invoke();
    }

    public void QuitGame()
    {
        resumeBotton.SetActive(false);
        quitBotton.SetActive(false);
        EndGame();
    }
}
