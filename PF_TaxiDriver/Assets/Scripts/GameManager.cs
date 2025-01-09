using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    //[SerializeField]
    private InputHandler inputHandler;
    [SerializeField] private MainSceneManager mainSceneManager;
    public ClientSpawner clientSpawner;
    public GameObject worldLimits;
    public Taxi player;

    private bool gamePaused = false;
    private int numberClientsDroppedOff = 0;
    int contador = 0;

    private void Awake()
    {
        inputHandler = player.GetComponent<InputHandler>();
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
        player.droppedClientAtDestination += DeleteClient;
    }

    private void OnDisable()
    {
        //inputHandler.userPressedSpace -= HandleSpacePress;
        mainSceneManager.resumeGame -= ResumeGame;
        // player.droppedClientAtDestination -= DeleteClient;
    }

    private void HandleSpacePress()
    {
        // TODO: parar el juego
        gamePaused = true;
    }

    private void ResumeGame()
    {
        // TODO: permitir al juego continuar
        gamePaused = false;
    }

    private void DeleteClient(Client client)
    {
        numberClientsDroppedOff++;
        clientSpawner.DestroyClient(client);
    }
}
