using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private InputHandler inputHandler;
    [SerializeField] private MainSceneManager mainSceneManager;
    public ClientSpawner clientSpawner;
    public BoxCollider worldLimits;
    public Taxi player;

    private bool gamePaused = false;
    private int numberClientsDroppedOff = 0;
    int contador = 0;

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
        Bounds worldBounds = worldLimits.bounds;
        float x = UnityEngine.Random.Range(worldBounds.min.x, worldBounds.max.x);
        while (x < worldBounds.min.x || x > worldBounds.max.x)
        {
            x = UnityEngine.Random.Range(worldBounds.min.x, worldBounds.max.x);
        }
        float y = -30.68f;
        float z = UnityEngine.Random.Range(worldBounds.min.z, worldBounds.max.z);
        while (z < worldBounds.min.z || z > worldBounds.max.z)
        {
            z = UnityEngine.Random.Range(worldBounds.min.z, worldBounds.max.z);
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
