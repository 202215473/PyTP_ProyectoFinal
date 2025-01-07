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

    bool gamePaused = false;

    void Update()
    {
        if (!gamePaused)
        {
            // pensar cuando deben salir los clientes (en funcion al tiempo jugado o en funcion a los clientes recogidos?)
            if () // condicion para nuevo cliente
            {
                Vector3 positionNewClient = GenerateRandomPosition();
                Vector3 clientsDestination = GenerateRandomPosition();  // el destino se ve cuando el taxi a recogido al cliente

                clientSpawner.Spawn(positionNewClient, clientsDestination);
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
        float y = -16.3f;
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
    }

    private void OnDisable()
    {
        inputHandler.userPressedSpace -= HandleSpacePress;
        mainSceneManager.resumeGame -= ResumeGame;
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
}
