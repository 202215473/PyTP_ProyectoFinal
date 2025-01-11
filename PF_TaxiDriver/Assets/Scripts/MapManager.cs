using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class MapManager : Singleton<MapManager>
{
    [SerializeField] private MainSceneManager mainSceneManager;
    [SerializeField] private ClientSpawner clientSpawner;
    [SerializeField] private GameManager gameManager;
    public GameObject worldLimits;
    public Taxi player;
    public GameObject cityMap;
    public GameObject playersLocationOnMap;
    public ImageSpawner pinkImageSpawner;
    public ImageSpawner yellowImageSpawner;
    private InputHandler inputHandler;

    private List<Client> clients = new List<Client>();
    private Dictionary<Client, List<GameObject>> clientMapDictionary;
    private bool gamePaused = false;

    private void Awake()
    {
        inputHandler = player.GetComponent<InputHandler>();
        clientMapDictionary = new Dictionary<Client, List<GameObject>>();
    }

    void Start()
    {
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

        Transform edge1 = worldLimits.transform.GetChild(0).transform;
        Transform edge2 = worldLimits.transform.GetChild(1);
        Transform edge3 = worldLimits.transform.GetChild(2);
        Transform edge4 = worldLimits.transform.GetChild(3);

        float xMax = edge1.position.x;
        float xMin = edge4.position.x;

        float zMax = edge3.position.z;
        float zMin = edge2.position.z;

        float x = (coordinates2Change.x * mapSize.x) / (xMax - xMin);
        float y = (coordinates2Change.z * mapSize.y) / (zMax - zMin);

        return new Vector2(x, y);
    }

    private void OnEnable()
    {
        inputHandler.userPressedSpace += HandleSpacePress;
        mainSceneManager.resumeGame += ResumeGame;
        clientSpawner.newClientSpawned += AddNewClient;
        gameManager.droppedClientAtDestination += DeleteClient;
    }

    private void OnDisable()
    {
        inputHandler.userPressedSpace -= HandleSpacePress;
        mainSceneManager.resumeGame -= ResumeGame;
        clientSpawner.newClientSpawned -= AddNewClient;
        gameManager.droppedClientAtDestination -= DeleteClient;
    }

    private void HandleSpacePress()
    {
        if (!gamePaused)
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
            gamePaused = true;
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
        gamePaused = false;
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
