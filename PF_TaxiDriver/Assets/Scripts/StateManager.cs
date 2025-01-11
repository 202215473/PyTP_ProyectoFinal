using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class StateManager : Singleton<StateManager>
{
    public event Action<GameObject> CollisionWithObstacle = delegate { };
    public event Action CollisionWithWorldLimits;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle") || collision.gameObject.CompareTag("PoliceCar"))
        {
            CollisionWithObstacle.Invoke(collision.gameObject); 
        }
        else if (collision.gameObject.CompareTag("EndMap"))
        { CollisionWithWorldLimits.Invoke(); }
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Radar"))
        { 
            float taxiSpeed = GetComponent<Rigidbody>().velocity.magnitude;
            Radar radar = collision.gameObject.GetComponent<Radar>();
            float speedLimit = radar.GetSpeedLimit();
            if (taxiSpeed > speedLimit)
            { CollisionWithObstacle.Invoke(collision.gameObject); }
        }
        else if (collision.gameObject.CompareTag("Obstacle") || collision.gameObject.CompareTag("PoliceCar"))
        { CollisionWithObstacle.Invoke(collision.gameObject); }
    }
}