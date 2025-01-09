using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debuf : Obstacle
{
    // Start is called before the first frame update
    void Start()
    {
        this.canChaseTaxi = false;
        this.isSolidObject = false;
        this.pointsToSubstract = 0;
        //this.speedMultiplier = 0.5f;
        this.duration = 30;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
