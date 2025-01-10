using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Taxi: CarController
{
    public InputHandler inputHandler; 
    private Rigidbody taxiRB;

    //public StateManager stateManager;

    public int lifeValue;
    private bool isCarryingClient;
    private bool isBlocked = false;

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
        if (!isBlocked)
        {
            carSpeed = this.taxiRB.velocity.magnitude;
            moveInput = this.inputHandler.GetMoveInput();
            turnInput = this.inputHandler.GetTurnInput();
            this.CheckSpeed(moveInput);
            this.CheckDirection(turnInput);  // To see if we are turning right or left
            this.UpdateMovement();
        }
    }
    public void CrashWithObstacle(Obstacle obstacle)
    {
        int lastLifeValue = this.lifeValue;
        this.lifeValue += obstacle.GetPointsToSubstract();
        //float lastCarSpeed = this.carSpeed;
        //this.carSpeed = lastCarSpeed * obstacle.GetSpeedMultiplier();

        Thread.Sleep(obstacle.GetDuration());

        this.SetLifeValue(lastLifeValue);
        //this.SetSpeedValue(this.lastSpeedValue);
    }

    public int GetLifeValue()
    { return lifeValue; }

    public void SetLifeValue(int value)
    { this.lifeValue = value; }

    public float GetSpeedValue()
    { return this.carSpeed; }

    public void SetIsBlocked(bool isBlocked)
    { this.isBlocked = isBlocked; }

    public bool GetIsCarryingClient()
    { return isCarryingClient; }

    public void SetIsCarryingClient(bool isCarryingClient)
    { this.isCarryingClient = isCarryingClient; }
}
