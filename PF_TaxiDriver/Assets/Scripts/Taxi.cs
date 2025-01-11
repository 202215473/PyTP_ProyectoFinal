using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Taxi: CarController
{
    [SerializeField] private InputHandler inputHandler; 
    private Rigidbody taxiRB;

    public bool isCarryingClient;
    public event Action<Client> droppedClientAtDestination; // ISA: TODO completar donde se lanza este evento


    // Start is called before the first frame update
    void Start()
    {
        taxiRB = gameObject.GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {
        carSpeed = taxiRB.velocity.magnitude;
        moveInput = inputHandler.GetMoveInput();
        turnInput = inputHandler.GetTurnInput();
        turnInput = inputHandler.GetTurnInput();
        float movingDirection = Vector3.Dot(transform.forward, taxiRB.velocity);
        CheckSpeed(moveInput, movingDirection);
        CheckDirection(turnInput);  // To see if we are turning right or left
        UpdateMovement();
    }
    public float GetSpeedValue()
    { return carSpeed; }
}
