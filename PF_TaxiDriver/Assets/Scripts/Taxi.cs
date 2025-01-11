using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Taxi: CarController
{
    [SerializeField] private InputHandler inputHandler; 
    private Rigidbody taxiRB;

    //public StateManager stateManager;

    public int lifeValue;
    public bool isCarryingClient;
    public event Action<Client> droppedClientAtDestination; // ISA: TODO completar donde se lanza este evento


    // Start is called before the first frame update
    void Start()
    {
        taxiRB = gameObject.GetComponent<Rigidbody>();
        // InputHandler inputHandler = gameObject.AddComponent<InputHandler>(); ISA: he comentado esto y 
        // lo he puesto como atributo para que funcionen bien los managers
    }
    // Update is called once per frame
    void Update()
    {
        carSpeed = this.taxiRB.velocity.magnitude;
        moveInput = this.inputHandler.GetMoveInput();
        turnInput = this.inputHandler.GetTurnInput();
        float movingDirection = Vector3.Dot(transform.forward, taxiRB.velocity);
        this.CheckSpeed(moveInput, movingDirection);
        this.CheckDirection(turnInput);  // To see if we are turning right or left
        this.UpdateMovement();
    }
    public float GetSpeedValue()
    { return this.carSpeed; }
}
