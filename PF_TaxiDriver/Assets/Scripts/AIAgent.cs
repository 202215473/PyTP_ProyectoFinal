using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAgent : MonoBehaviour
{
    [SerializeField] private StateManager stateManager;
    [SerializeField] private GameObject obstacles;
    [SerializeField] private GameObject player;

    private List<GameObject> policeCars = new List<GameObject>();
    private GameObject[] arePersecuting = {null, null, null, null, null, null, null, null, null};
    private double[] startTimeOfPersecution = {0, 0, 0, 0, 0, 0, 0, 0, 0};
    private double durationOfPersecution = 10;
    private bool playerVsAI;

    public event Action gameOver;

    private void Start()
    {
        playerVsAI = GameState.GetInstance().GetPlayerVsAI();
        if (playerVsAI)
        {
            GetPoliceCars();
        }
    }

    private void Update()
    {
        if (playerVsAI)
        {
            for (int i = 0; i < 9; i++)
            {
                if (arePersecuting[i] != null)
                {
                    double currentTime = DateTimeOffset.Now.ToUnixTimeMilliseconds() / 1000.0;
                    if (currentTime - startTimeOfPersecution[i] >= durationOfPersecution)
                    {
                        StopPersecution(policeCars[i]);
                    }
                    else
                    {
                        GameObject policeCarGO = policeCars[i];
                        PoliceCar policeCar = policeCarGO.GetComponent<PoliceCar>();

                        float moveInput = CalculateMoveInput(policeCarGO);
                        float turnInput = CalculateTurnInput(policeCarGO);

                        policeCar.SetMoveInput(moveInput);
                        policeCar.SetTurnInput(turnInput);
                    }
                }
            }
        }
    }

    private void GetPoliceCars()
    {
        foreach (Transform child in obstacles.transform)
        {
            if (child.CompareTag("PoliceCar"))
            {
                policeCars.Add(child.gameObject);
            }
        }
    }

    private void OnEnable()
    {
        stateManager.CollisionWithObstacle += StartPersecution;
    }

    private void OnDisable()
    {
        stateManager.CollisionWithObstacle -= StartPersecution;
    }

    public void StartPersecution(GameObject collision)
    {
        if (playerVsAI)
        {
            if (collision.gameObject.CompareTag("PoliceCar"))
            {
                GameObject policeCar = collision.gameObject;
                policeCar.GetComponent<PoliceCar>().SetIsPersecuting(true);
                int index = policeCars.IndexOf(policeCar);
                if (arePersecuting[index] == null)
                {
                    arePersecuting[index] = policeCar;
                    startTimeOfPersecution[index] = DateTimeOffset.Now.ToUnixTimeMilliseconds() / 1000.0;
                }
                else
                {
                    gameOver.Invoke();
                }
            }
        }
    }

    public void StopPersecution(GameObject policeCar)
    {
        if (playerVsAI)
        {
            policeCar.GetComponent<PoliceCar>().SetIsPersecuting(false);
            int index = policeCars.IndexOf(policeCar);
            arePersecuting[index] = null;
        }
    }

    private float CalculateMoveInput(GameObject policeCarGO)
    {
        Transform playerTransform = player.transform;
        float distanceToPlayer = Vector3.Distance(playerTransform.position, policeCarGO.transform.position);

        return Mathf.Clamp(distanceToPlayer / 10.0f, 5.0f, 15.0f);
    }

    private float CalculateTurnInput(GameObject policeCarGO)
    {
        Transform playerTransform = player.transform;
        Vector3 directionToPlayer = (playerTransform.position - policeCarGO.transform.position).normalized;
        Vector3 localDirection = policeCarGO.transform.InverseTransformDirection(directionToPlayer);

        float turnInput = Mathf.Clamp(localDirection.x, -1f, 1f);
        return turnInput;
    }

}
