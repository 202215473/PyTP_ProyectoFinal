using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public float turnAngle;
    public float acceleration;
    public float brakeForce;
    public AnimationCurve curve;
    public float carSpeed = 0f;
    public WheelColliders colliders;
    public WheelMeshes meshes;

    protected float moveInput;
    protected float turnInput;

    private float threshold = 0.5f;

    protected void CheckSpeed(float moveInput, float movingDirection)
    {
        if ((movingDirection < threshold && moveInput < 0) || (movingDirection > -threshold && moveInput > 0))
        { Accelerate(moveInput); }
        else if ((movingDirection > -threshold && moveInput < 0) || (movingDirection < +threshold && moveInput > 0))
        { Brake(moveInput); }
        else
        { Decelerate(); }
    }
    protected void CheckDirection(float turnInput)
    {
        if (turnInput != 0)
        { Turn(turnInput); }
    }
    protected void UpdateMovement()
    {
        UpdateGraphics(this.colliders.wheelFR, this.meshes.wheelFR);
        UpdateGraphics(this.colliders.wheelFL, this.meshes.wheelFL);
        UpdateGraphics(this.colliders.wheelRR, this.meshes.wheelRR);
        UpdateGraphics(this.colliders.wheelRL, this.meshes.wheelRL);
    }
    private void Accelerate(float moveInput)
    {
        if (this.colliders.wheelRR.brakeTorque != 0f || this.colliders.wheelRL.brakeTorque != 0f)
        {
            this.colliders.wheelRR.brakeTorque = 0f;
            this.colliders.wheelRL.brakeTorque = 0f;
        }
        this.colliders.wheelRR.motorTorque = this.acceleration * moveInput;
        this.colliders.wheelRL.motorTorque = this.acceleration * moveInput;;
    }

    private void Decelerate()
    {
        float resistance = this.acceleration * 0.5f;
        this.colliders.wheelRR.brakeTorque = resistance;
        this.colliders.wheelRL.brakeTorque = resistance;
    }
    private void Brake(float moveInput)
    {
        if (moveInput != 0)
        {
            this.colliders.wheelRR.brakeTorque = Mathf.Abs(this.brakeForce * moveInput);
            this.colliders.wheelRL.brakeTorque = Mathf.Abs(this.brakeForce * moveInput);
        }
    }
    private void Turn(float turnInput)
    {
        float angle = turnInput * this.turnAngle;  //this.curve.Evaluate(this.carSpeed);

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
