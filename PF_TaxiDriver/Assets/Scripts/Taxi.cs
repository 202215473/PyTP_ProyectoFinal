using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Taxi: CarController
{
    public InputHandler inputHandler; 
    private Rigidbody taxiRB;


    private bool isCarryingClient;
    private bool isBlocked = false;

    void Start()
    {
        taxiRB = gameObject.GetComponent<Rigidbody>();
    }
    
    void Update()
    {
        if (!isBlocked)
        {
            carSpeed = taxiRB.velocity.magnitude;
            moveInput = inputHandler.GetMoveInput();
            turnInput = inputHandler.GetTurnInput();
            float movingDirection = Vector3.Dot(transform.forward, taxiRB.velocity);
            CheckSpeed(moveInput, movingDirection);
            CheckDirection(turnInput);  // To see if we are turning right or left
            UpdateMovement();
        }
    }

    public float GetSpeedValue()
    { return carSpeed; }

    public void SetIsBlocked(bool isBlocked)
    { this.isBlocked = isBlocked; }

    public bool GetIsCarryingClient()
    { return isCarryingClient; }

    public void SetIsCarryingClient(bool isCarryingClient)
    { this.isCarryingClient = isCarryingClient; }
}
