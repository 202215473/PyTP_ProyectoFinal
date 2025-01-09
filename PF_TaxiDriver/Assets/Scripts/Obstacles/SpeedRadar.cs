using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedRadar : MonoBehaviour
{
    private float speed;
    public float speedLimit = 100f;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            Rigidbody taxiRB = collider.GetComponent<Rigidbody>();
            if (taxiRB != null)
            {
                float taxiSpeed = taxiRB.velocity.magnitude * 3.6f; // Transform from m/s to km/h
                Debug.Log("Taxi detected! Speed: " + speed + " km/h");

                if (speed > speedLimit)
                {
                    Debug.Log("Speeding! Please slow down.");
                }
            }
        }
    }
}
