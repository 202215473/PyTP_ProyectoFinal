using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radar : Obstacle
{
    public float speedLimit = 15f;

    void Start()
    {
        this.isSolidObject = false;
        this.pointsToSubstract = -8;
        this.moneyToSubstract = 5f;
    }
    private void Update()
    {
        
    }
    public float GetSpeedLimit()
    { return speedLimit; }

}
