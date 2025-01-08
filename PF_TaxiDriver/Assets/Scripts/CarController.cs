using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public float turnAngle;
    public float acceleration;
    public WheelColliders colliders;
    public WheelMeshes meshes;
    public AnimationCurve curve;

    private Rigidbody carRB;
    private InputHandler inputHandler;
    private float carSpeed;
    private float moveInput;
    private float turnInput;

    public bool isCarryingClient;
    public event Action<Client> droppedClientAtDestination; // ISA: TODO completar donde se lanza este evento


    // Start is called before the first frame update
    void Start()
    {
        carRB = gameObject.GetComponent<Rigidbody>();
        inputHandler = gameObject.AddComponent<InputHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        carSpeed = this.carRB.velocity.magnitude;
        moveInput = this.inputHandler.GetMoveInput();
        turnInput = this.inputHandler.GetTurnInput();
        this.CheckSpeed(moveInput);
        this.CheckDirection(turnInput);  // To see if we are turning right or left
        this.UpdateMovement();
        
        Ray ray = new Ray(transform.position, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit, 10f, ~LayerMask.GetMask("CarBody")))
        {
            Debug.Log("Terreno detectado: " + hit.collider.name);
        }
        else
        {
            Debug.Log("Terreno no detectado. El taxi está flotando.");
        }
    }

    void CheckSpeed(float moveInput)
    {
        if (moveInput > 0)
        { Accelerate(moveInput); }
        else if (moveInput < 0)
        { Brake(moveInput); }
        else
        { Decelerate(); }
        

    }
    void CheckDirection(float turnInput)
    {
        if (turnInput != 0)
        { Turn(turnInput); }
    }
    void UpdateMovement()
    {
        UpdateGraphics(this.colliders.wheelFR, this.meshes.wheelFR);
        UpdateGraphics(this.colliders.wheelFL, this.meshes.wheelFL);
        UpdateGraphics(this.colliders.wheelRR, this.meshes.wheelRR);
        UpdateGraphics(this.colliders.wheelRL, this.meshes.wheelRL);
    }

    private void Brake(float moveInput)
    {
        this.colliders.wheelRR.brakeTorque = Mathf.Abs(this.acceleration * moveInput);
        this.colliders.wheelRL.brakeTorque = Mathf.Abs(this.acceleration * moveInput);
    }
    private void Decelerate()
    {
        float resistance = this.acceleration * 0.1f;
        this.colliders.wheelRR.brakeTorque = resistance;
        this.colliders.wheelRL.brakeTorque = resistance;
    }
    private void Accelerate(float moveInput)
    {
        this.colliders.wheelRR.motorTorque = this.acceleration * moveInput;
        this.colliders.wheelRL.motorTorque = this.acceleration * moveInput;
    }

    private void Turn(float turnInput)
    {
        float angle = turnInput * this.curve.Evaluate(this.carSpeed); ;

        this.colliders.wheelFR.steerAngle = angle;
        this.colliders.wheelFL.steerAngle = angle;
    }
    void UpdateGraphics(WheelCollider wheelCollider, MeshRenderer wheelMesh)
    {
        Vector3 position;
        Quaternion rotation;

        wheelCollider.GetWorldPose(out position, out rotation);
        wheelMesh.transform.SetPositionAndRotation(position, rotation);
    }
    [System.Serializable]
    public class WheelColliders
    {
        public WheelCollider wheelFR;
        public WheelCollider wheelFL;
        public WheelCollider wheelRR;
        public WheelCollider wheelRL;
    }

    [System.Serializable]
    public class WheelMeshes
    {
        public MeshRenderer wheelFR;
        public MeshRenderer wheelFL;
        public MeshRenderer wheelRR;
        public MeshRenderer wheelRL;
    }
}
