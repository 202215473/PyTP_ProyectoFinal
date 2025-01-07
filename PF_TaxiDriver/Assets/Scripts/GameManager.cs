using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private InputHandler inputHandler;
    [SerializeField] private MainSceneManager mainSceneManager;
    public GameObject player;
    public GameObject cityMap;
    public GameObject playersLocationOnMap;
    public BoxCollider worldLimits;

    void Start()
    {
        cityMap.SetActive(true);
        playersLocationOnMap.SetActive(true);
        UpdatePlayersLocationOnMap();
    }

    void Update()
    {
        UpdatePlayersLocationOnMap();
    }

    private void UpdatePlayersLocationOnMap()
    {
        Vector3 playersPosition = player.transform.position;

        RectTransform cityMapRect = cityMap.GetComponent<RectTransform>();
        Vector2 mapSize = cityMapRect.sizeDelta;

        RectTransform playersLocationRect = playersLocationOnMap.GetComponent<RectTransform>();
        Bounds worldBounds = worldLimits.bounds;

        float x = (playersPosition.x * mapSize.x) / (worldBounds.max.x - worldBounds.min.x);
        float y = (playersPosition.z * mapSize.y) / (worldBounds.max.z - worldBounds.min.z);

        playersLocationRect.anchoredPosition = new Vector2(x, y);
    }

    private void OnEnable()
    {
        inputHandler.userPressedSpace += HandleSpacePress;
        mainSceneManager.resumeGame += ResumeGame;
    }

    private void OnDisable()
    {
        inputHandler.userPressedSpace -= HandleSpacePress;
        mainSceneManager.resumeGame -= ResumeGame;
    }

    private void HandleSpacePress()
    {
        // TODO: parar el juego
        cityMap.SetActive(false);
        playersLocationOnMap.SetActive(false);
    }

    private void ResumeGame()
    {
        // TODO: permitir al juego continuar
        cityMap.SetActive(true);
        playersLocationOnMap.SetActive(true);
    }
}
