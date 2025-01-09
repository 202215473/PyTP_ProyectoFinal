using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private BoxCollider collider;

    protected bool canChaseTaxi;
    protected bool isSolidObject;
    protected int pointsToSubstract;
    //protected float speedMultiplier;
    protected int duration;
   
    
    //public Obstacle()
    //{ }
    void Start()
    {
        this.collider = gameObject.GetComponent<BoxCollider>();
    }
    //void Update()
    //{
    //}
    public int GetPointsToSubstract()
    { return pointsToSubstract; }
    //public float GetSpeedMultiplier()
    //{ return speedMultiplier; }
    public int GetDuration()
    { return duration; }
    
}
