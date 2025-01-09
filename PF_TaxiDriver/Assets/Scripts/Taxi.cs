using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Taxi: CarController
{
    private Rigidbody taxiRB;

    public int lifeValue;
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
}
