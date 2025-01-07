using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : Singleton<MapManager>
{
    [SerializeField] private InputHandler inputHandler;
    [SerializeField] private MainSceneManager mainSceneManager;
    [SerializeField] private ClientSpawner clientSpawner;
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

        RectTransform playersLocationRect = playersLocationOnMap.GetComponent<RectTransform>();
        playersLocationRect.anchoredPosition = CoordinatesOfWorld2Map(playersPosition);
    }

    private Vector2 CoordinatesOfWorld2Map(Vector3 coordinates2Change)
    {
        RectTransform cityMapRect = cityMap.GetComponent<RectTransform>();
        Vector2 mapSize = cityMapRect.sizeDelta;


        Bounds worldBounds = worldLimits.bounds;

        float x = (coordinates2Change.x * mapSize.x) / (worldBounds.max.x - worldBounds.min.x);
        float y = (coordinates2Change.z * mapSize.y) / (worldBounds.max.z - worldBounds.min.z);

        return new Vector2(x, y);
    }

    private void OnEnable()
    {
        inputHandler.userPressedSpace += HandleSpacePress;
        mainSceneManager.resumeGame += ResumeGame;
        clientSpawner.newClientSpawned += AddNewClient;
    }

    private void OnDisable()
    {
        inputHandler.userPressedSpace -= HandleSpacePress;
        mainSceneManager.resumeGame -= ResumeGame;
        clientSpawner.newClientSpawned -= AddNewClient;
    }

    private void HandleSpacePress()
    {
        cityMap.SetActive(false);
        playersLocationOnMap.SetActive(false);
    }

    private void ResumeGame()
    {
        cityMap.SetActive(true);
        playersLocationOnMap.SetActive(true);
    }

    private void AddNewClient(Client newClient)
    {
        // Añado el cliente al mapa
    }
}
