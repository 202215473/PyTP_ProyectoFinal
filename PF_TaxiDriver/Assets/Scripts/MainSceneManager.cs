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
    [SerializeField] private ClientSpawner clientSpawner;
    [SerializeField] private GameObject resumeBotton;
    [SerializeField] private GameObject quitBotton;
    [SerializeField] private GameObject gameOverMessage;
    [SerializeField] private GameObject newClientMessage;

    public event Action resumeGame;
    private bool gamePaused = false;

    void Start()
    {
        resumeBotton.SetActive(false);
        quitBotton.SetActive(false);
        gameOverMessage.SetActive(false);
        newClientMessage.SetActive(false);
    }

    private void OnEnable()
    {
        inputHandler.userPressedSpace += HandleSpacePress;
        lifeManager.gameOver += EndGame;
        clientSpawner.newClientSpawned += ShowNewClientMessage;
    }

    private void OnDisable()
    {
        inputHandler.userPressedSpace -= HandleSpacePress;
        lifeManager.gameOver -= EndGame;
        clientSpawner.newClientSpawned -= ShowNewClientMessage;
    }

    private void HandleSpacePress()
    {
        if (!gamePaused)
        {
            resumeBotton.SetActive(true);
            quitBotton.SetActive(true);
            gamePaused = true;
        }
    }

    private void EndGame()
    {
        gameOverMessage.SetActive(true);
        StartCoroutine(HandleGameOverSequence(3f));
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
        gamePaused = false;
        resumeGame.Invoke();
    }

    public void QuitGame()
    {
        resumeBotton.SetActive(false);
        quitBotton.SetActive(false);
        EndGame();
    }

    private void ShowNewClientMessage(Client client)
    {
        newClientMessage.SetActive(true);
        StartCoroutine(HandleNewClientMessage(3f));
    }

    public IEnumerator HandleNewClientMessage(float seconds2Sleep)
    {
        yield return new WaitForSeconds(seconds2Sleep);
        newClientMessage.SetActive(false);
    }
}
