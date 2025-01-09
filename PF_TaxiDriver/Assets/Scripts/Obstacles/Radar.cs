using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radar : Obstacle
{
    private float speed;
    public float speedLimit = 100f;

    void Start()
    {
        this.canChaseTaxi = false;
        this.isSolidObject = false;
        this.pointsToSubstract = -3;
        this.moneyToSubstract = 0f;
    }
    private void Update()
    {
        
    }
}
