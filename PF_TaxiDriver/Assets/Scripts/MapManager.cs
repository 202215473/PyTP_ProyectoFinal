using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class MapManager : Singleton<MapManager>
{
    [SerializeField] private InputHandler inputHandler;
    [SerializeField] private MainSceneManager mainSceneManager;
    [SerializeField] private ClientSpawner clientSpawner;
    public BoxCollider worldLimits;
    public Taxi player;
    public GameObject cityMap;
    public GameObject playersLocationOnMap;
    public ImageSpawner pinkImageSpawner;
    public ImageSpawner yellowImageSpawner;

    private List<Client> clients = new List<Client>();
    private Dictionary<Client, List<GameObject>> clientMapDictionary;


    void Start()
    {
        clientMapDictionary = new Dictionary<Client, List<GameObject>>();
        cityMap.SetActive(true);
        playersLocationOnMap.SetActive(true);
        UpdatePlayersLocationOnMap();
    }

    void Update()
    {
        UpdatePlayersLocationOnMap();
        UpdateClientImages();
    }

    private void UpdatePlayersLocationOnMap()
    {
        Vector3 playersPosition = player.gameObject.transform.position;

        RectTransform playersLocationRect = playersLocationOnMap.GetComponent<RectTransform>();
        playersLocationRect.anchoredPosition = CoordinatesOfWorld2Map(playersPosition);
    }

    private void UpdateClientImages()
    {
        foreach (KeyValuePair<Client, List<GameObject>> entry in clientMapDictionary)
        {
            Client client = entry.Key;
            List<GameObject> clientImages = entry.Value;

            if (client.IsPickedUp())
            {
                clientImages[0].SetActive(false);
                clientImages[1].SetActive(true);
            }
        }
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
        player.droppedClientAtDestination += DeleteClient;
    }

    private void OnDisable()
    {
        inputHandler.userPressedSpace -= HandleSpacePress;
        mainSceneManager.resumeGame -= ResumeGame;
        clientSpawner.newClientSpawned -= AddNewClient;
        player.droppedClientAtDestination -= DeleteClient;
    }

    private void HandleSpacePress()
    {
        cityMap.SetActive(false);
        playersLocationOnMap.SetActive(false);
        foreach (KeyValuePair<Client, List<GameObject>> entry in clientMapDictionary)
        {
            Client client = entry.Key;
            List<GameObject> clientImages = entry.Value;

            clientImages[0].SetActive(false);
            clientImages[1].SetActive(false);
        }
    }

    private void ResumeGame()
    {
        cityMap.SetActive(true);
        playersLocationOnMap.SetActive(true);
        foreach (KeyValuePair<Client, List<GameObject>> entry in clientMapDictionary)
        {
            Client client = entry.Key;
            List<GameObject> clientImages = entry.Value;

            if (client.IsPickedUp())
            {
                clientImages[1].SetActive(true);
            }
            else
            {
                clientImages[0].SetActive(true);
            }
        }
    }

    private void AddNewClient(Client newClient)
    {
        Vector3 clienPosition3D = newClient.GetPosition();
        Vector2 clientPosition = CoordinatesOfWorld2Map(clienPosition3D);
        GameObject positionImage = pinkImageSpawner.Spawn(clientPosition, cityMap);
        positionImage.SetActive(true);

        Vector2 clientDestination = CoordinatesOfWorld2Map(newClient.GetDestination());
        GameObject destinationImage = yellowImageSpawner.Spawn(clientDestination, cityMap);
        destinationImage.SetActive(false);


        List<GameObject> clientsImages = new List<GameObject>();
        clientsImages.Add(positionImage);
        clientsImages.Add(destinationImage);
        clientMapDictionary[newClient] = clientsImages;
    }

    private void DeleteClient(Client client)
    {
        List<GameObject> clientsImages = clientMapDictionary[client];
        clientsImages[0].SetActive(false);
        clientsImages[1].SetActive(false);
        clientMapDictionary.Remove(client);
    }
}
