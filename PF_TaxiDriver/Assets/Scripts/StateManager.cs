using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class StateManager : MonoBehaviour
{
    public event Action<GameObject> CollisionWithObstacle = delegate { };
    public event Action CollisionWithWorldLimits;
    
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
        {
            Debug.Log("We have collided with an obstacle"); 
            CollisionWithObstacle.Invoke(collision.gameObject); 
        }
        else if (collision.gameObject.CompareTag("EndMap"))
        { CollisionWithWorldLimits.Invoke(); }
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("We have triggered an obstacle"); 
            CollisionWithObstacle.Invoke(collision.gameObject); }
    }
}
