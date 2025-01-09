using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fence : Obstacle
{
    void Start()
    {
        this.canChaseTaxi = false;
        this.isSolidObject = true;
        this.pointsToSubstract = -10;
        //this.speedMultiplier = 0.8f;
        this.duration = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
