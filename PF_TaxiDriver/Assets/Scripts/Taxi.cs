using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Taxi : MonoBehaviour
{
    public float turnAngle;
    public float acceleration;

    private Rigidbody taxiRB;
    private WheelColliders colliders;
    private WheelMeshes meshes;
    private InputHandler inputHandler;
    private float taxiSpeed = 0f;
    private float moveInput;
    private float turnInput;


    // Start is called before the first frame update
    void Start()
    {
        taxiRB = gameObject.AddComponent<Rigidbody>();
        inputHandler = gameObject.AddComponent<InputHandler>();
}

    // Update is called once per frame
    void Update()
    {
        taxiSpeed = taxiRB.velocity.magnitude;
        moveInput = inputHandler.GetMoveInput();
        turnInput = inputHandler.GetTurnInput();
        CheckSpeed();
        CheckDirection();  // To see if we are turning right or left
        UpdateMovement();
    }

    void CheckSpeed()
    {
        if (moveInput > 0)
            { Accelerate(); }
        else if (moveInput < 0)
            { Brake(); }
        else
            { Decelerate(); }
    }
    void CheckDirection()
    {
        if (turnInput != 0)
            { Turn(); }
    }
    void UpdateMovement()
    {
        UpdateGraphics(colliders.wheelFR, meshes.wheelFR);
        UpdateGraphics(colliders.wheelFL, meshes.wheelFL);
        UpdateGraphics(colliders.wheelRR, meshes.wheelRR);
        UpdateGraphics(colliders.wheelRL, meshes.wheelRL);
    }

    private void Brake()
    {
        this.colliders.wheelRR.brakeTorque = Mathf.Abs(acceleration * moveInput); 
        this.colliders.wheelRR.brakeTorque = Mathf.Abs(acceleration * moveInput); 
    }
    private void Decelerate()
    {
        float resistance = acceleration * 0.1f;
        colliders.wheelRR.brakeTorque = resistance;
        colliders.wheelRL.brakeTorque = resistance;
    }
    private void Accelerate()
    {
        this.colliders.wheelRR.motorTorque = acceleration * moveInput;
        this.colliders.wheelRR.motorTorque = acceleration * moveInput;
    }
    private void Turn()
    {
        // Calcular el ángulo de dirección basado en el input del jugador
        float angle = turnAngle * turnInput;

        // Aplicar el ángulo de giro a las ruedas delanteras
        colliders.wheelFR.steerAngle = angle;
        colliders.wheelFL.steerAngle = angle;
    }
    void UpdateGraphics(WheelCollider wheelCollider, MeshRenderer wheelMesh)
    {
        Vector3 position;
        Quaternion rotation;

        // Obtener la posición y rotación del WheelCollider
        wheelCollider.GetWorldPose(out position, out rotation);

        // Aplicar la posición y rotación al modelo visual
        wheelMesh.transform.position = position;
        wheelMesh.transform.rotation = rotation;
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
