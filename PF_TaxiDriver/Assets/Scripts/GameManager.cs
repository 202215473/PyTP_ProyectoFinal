using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private MainSceneManager mainSceneManager;
    [SerializeField] private ClientSpawner clientSpawner;
    [SerializeField] private GameObject worldLimits;
    [SerializeField] private GameObject player;

    private Taxi taxi;
    private InputHandler inputHandler;
    private bool gamePaused = false;
    private int numberClientsDroppedOff = 0;
    private Vector3 playersPreviousVelocity = Vector3.zero;

    public float minimumDistance = 10f;
    public float minimumVelocity = 2f;
    int contador = 0;

    private void Awake()
    {
        inputHandler = player.GetComponent<InputHandler>();
        taxi = player.GetComponent<Taxi>();
    }

    void Update()
    {
        if (!gamePaused)
        {
            // pensar cuando deben salir los clientes (en funcion al tiempo jugado o en funcion a los clientes recogidos?)
            if (contador == 0) // condicion para nuevo cliente
            {
                Vector3 positionNewClient = GenerateRandomPosition();
                Vector3 clientsDestination = GenerateRandomPosition();  // el destino se ve cuando el taxi a recogido al cliente

                clientSpawner.Spawn(positionNewClient, clientsDestination);
                contador += 1;
                // se cuenta como q el taxi ha recogido al cliente cuando esta a menos de una distancia de x
            }
            bool playerIsCarryingClient = taxi.GetIsCarryingClient();
            if (!playerIsCarryingClient)
            {
                foreach (Client client in clientSpawner.GetClients())
                {
                    CheckIfClientPickedUp(client);
                    if (!playerIsCarryingClient)
                    {
                        break;
                    }
                }
            }
        }
    }

    public Vector3 GenerateRandomPosition()
    {
        Transform edge1 = worldLimits.transform.GetChild(0).transform;
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
            x = UnityEngine.Random.Range(xMin, xMax);
        }
        Vector3 randomPosition = new Vector3(x, y, x);
        return randomPosition;
    }

    private void OnEnable()
    {
        inputHandler.userPressedSpace += HandleSpacePress;
        mainSceneManager.resumeGame += ResumeGame;
        taxi.droppedClientAtDestination += DeleteClient;
    }

    private void OnDisable()
    {
        inputHandler.userPressedSpace -= HandleSpacePress;
        mainSceneManager.resumeGame -= ResumeGame;
        taxi.droppedClientAtDestination -= DeleteClient;
    }

    private void HandleSpacePress()
    {
        if (!gamePaused)
        {
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
}
