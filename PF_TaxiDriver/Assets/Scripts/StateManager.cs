using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class StateManager : MonoBehaviour
{
    public event Action NewCollision;  // Esto es del MainSceneManager, habrá que modificarlo

    private Taxi player;
    private Fence fence;
    private SpeedRadar speedRadar;
    private Debuf debuf;
    
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
        Debug.Log("Collided with: " + collision.gameObject.name);

        if (collision.gameObject.CompareTag("Fence"))
        {
            Debug.Log("Player hit a fence");
        }
        else if (collision.gameObject.CompareTag("Debuf"))
        {
            Debug.Log("Player hit a debuf");
        }
        else if (collision.gameObject.CompareTag("Radar"))
        {
            Debug.Log("Player hit a radar");
        }
    }
}
