using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private MainSceneManager mainSceneManager;
    [SerializeField] private ClientSpawner clientSpawner;
    [SerializeField] private GameObject worldLimits;
    [SerializeField] private GameObject player;

    private Taxi taxi;
    private InputHandler inputHandler;

    public event Action<Client> droppedClientAtDestination;
    private Vector3 playersPreviousVelocity = Vector3.zero;
    private Client currentClient;
    private bool gamePaused = false;

    private int numberClientsDroppedOff = 0;
    private int perClientsDroppedOff = 4;
    private float diagonalOfWorld;
    private float expectedVelocity = 40f;
    private float factor = 2.5f;
    private double totalPauseTime = 0;
    private double startTime;
    private double pauseTime;
    private double timeMargin;

    public float minimumDistance = 10f;
    public float minimumVelocity = 5f;

    private void Awake()
    {
        inputHandler = player.GetComponent<InputHandler>();
        taxi = player.GetComponent<Taxi>();
    }

    private void Start()
    {
        Transform edge1 = worldLimits.transform.GetChild(0);
        Transform edge2 = worldLimits.transform.GetChild(1);
        Transform edge3 = worldLimits.transform.GetChild(2);
        Transform edge4 = worldLimits.transform.GetChild(3);

        Vector3 upLeftCorner = new Vector3(edge1.position.x, 0, edge3.position.z);
        Vector3 downRightCorner = new Vector3(edge4.position.x, 0, edge2.position.z);
        diagonalOfWorld = Vector3.Distance(upLeftCorner, downRightCorner);

        Vector3 positionNewClient = GenerateRandomPosition();
        Vector3 clientsDestination = GenerateRandomPosition();
        clientSpawner.Spawn(positionNewClient, clientsDestination);
        startTime = DateTimeOffset.Now.ToUnixTimeMilliseconds() / 1000.0;

        timeMargin = CalculateTimeMargin();
    }

    void Update()
    {
        if (!gamePaused)
        {
            double currentTime = DateTimeOffset.Now.ToUnixTimeMilliseconds() / 1000.0;
            if (currentTime - startTime - totalPauseTime >= timeMargin)
            {
                GenerateNewClient();
            }
            bool playerIsCarryingClient = taxi.GetIsCarryingClient();
            if (!playerIsCarryingClient)
            {
                foreach (Client client in clientSpawner.GetClients())
                {
                    CheckIfClientPickedUp(client);
                    if (!playerIsCarryingClient)
                    {
                        currentClient = client;
                        break;
                    }
                }
            }
            else
            {
                CheckIfClientDroppedOff(currentClient);
            }
        }
    }

    public Vector3 GenerateRandomPosition()
    {
        Transform edge1 = worldLimits.transform.GetChild(0);
        Transform edge2 = worldLimits.transform.GetChild(1);
        Transform edge3 = worldLimits.transform.GetChild(2);
        Transform edge4 = worldLimits.transform.GetChild(3);

        float xMax = edge1.position.x;
        float xMin = edge4.position.x;

        float zMax = edge3.position.z;
        float zMin = edge2.position.z;

        float x = UnityEngine.Random.Range(xMin, xMax);
        while (x < xMin || x > xMax)
        {
            x = UnityEngine.Random.Range(xMin, xMax);
        }
        float y = -30.68f;
        float z = UnityEngine.Random.Range(zMin, zMax);
        while (z < zMin || z > zMax)
        {
            z = UnityEngine.Random.Range(zMin, zMax);
        }
        Vector3 randomPosition = new Vector3(x, y, x);
        return randomPosition;
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
        if (!gamePaused)
        {
            pauseTime = DateTimeOffset.Now.ToUnixTimeMilliseconds() / 1000.0;
            Rigidbody taxiRB = player.GetComponent<Rigidbody>();
            this.playersPreviousVelocity = taxiRB.velocity;
            taxiRB.velocity = Vector3.zero;

            taxi.SetIsBlocked(true);
            gamePaused = true;
        }
    }

    private void ResumeGame()
    {
        Rigidbody taxiRB = player.GetComponent<Rigidbody>();
        taxiRB.velocity = this.playersPreviousVelocity;

        taxi.SetIsBlocked(false);

        double resumeTime = DateTimeOffset.Now.ToUnixTimeMilliseconds() / 1000.0;
        totalPauseTime += resumeTime - pauseTime;
        gamePaused = false;
    }

    private void DeleteClient(Client client)
    {
        numberClientsDroppedOff++;
        clientSpawner.DestroyClient(client);
    }

    private void CheckIfClientPickedUp(Client client)
    {
        Vector3 clientsPosition = client.gameObject.transform.position;
        Vector3 playersPosition = player.gameObject.transform.position;
        float taxisVelocity = Mathf.Abs(player.GetComponent<Rigidbody>().velocity.magnitude);

        if (Vector3.Distance(clientsPosition, playersPosition) <= minimumDistance && taxisVelocity <= minimumVelocity)
        {
            taxi.SetIsCarryingClient(true);
            client.SetIsPickedUp(true);
            client.gameObject.SetActive(false);
        }
    }

    private void CheckIfClientDroppedOff(Client client)
    {
        Vector3 destination = client.GetDestination();
        Vector3 playersPosition = player.gameObject.transform.position;
        float taxisVelocity = Mathf.Abs(player.GetComponent<Rigidbody>().velocity.magnitude);

        if (Vector3.Distance(destination, playersPosition) <= minimumDistance && taxisVelocity <= minimumVelocity)
        {
            taxi.SetIsCarryingClient(false);
            droppedClientAtDestination.Invoke(client);
            DeleteClient(client);
            timeMargin = CalculateTimeMargin();
        }
    }

    public void GenerateNewClient()
    {
        Vector3 positionNewClient = GenerateRandomPosition();
        Vector3 clientsDestination = GenerateRandomPosition();

        clientSpawner.Spawn(positionNewClient, clientsDestination);
        startTime = DateTimeOffset.Now.ToUnixTimeMilliseconds() / 1000.0;
        totalPauseTime = 0;
    }

    public double CalculateTimeMargin()
    {
        if (numberClientsDroppedOff % perClientsDroppedOff == 0 && numberClientsDroppedOff <= 5 * perClientsDroppedOff)
        {
            expectedVelocity += 5;
        }
        else if (numberClientsDroppedOff % perClientsDroppedOff == 0 && numberClientsDroppedOff <= 8 * perClientsDroppedOff)
        {
            factor -= 0.05f;
        }
        return (diagonalOfWorld * factor) / expectedVelocity ;
    }
}
