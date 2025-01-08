using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Taxi: CarController
{
    private Rigidbody taxiRB;

    public bool isCarryingClient;
    public event Action<Client> droppedClientAtDestination; // ISA: TODO completar donde se lanza este evento

    // Start is called before the first frame update
    void Start()
    {
        taxiRB = gameObject.GetComponent<Rigidbody>();
        inputHandler = gameObject.AddComponent<InputHandler>();
    }
    // Update is called once per frame
    void Update()
    {
        carSpeed = this.taxiRB.velocity.magnitude;
        moveInput = this.inputHandler.GetMoveInput();
        turnInput = this.inputHandler.GetTurnInput();
        this.CheckSpeed(moveInput);
        this.CheckDirection(turnInput);  // To see if we are turning right or left
        this.UpdateMovement();
    }

}
