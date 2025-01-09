using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class StateManager : MonoBehaviour
{
    public event Action<GameObject> CollisionWithObstacle;
    public event Action CollisionWithWorldLimits;

    //private Taxi player;
    //private Fence fence;
    //private Radar speedRadar;
    //private Debuf debuf;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        { CollisionWithObstacle.Invoke(collision.gameObject); }
        else if (collision.gameObject.CompareTag("EndMap"))
        { CollisionWithWorldLimits.Invoke(); }
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        { CollisionWithObstacle.Invoke(collision.gameObject); }
    }
}
