using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceCar : Obstacle
{
    public Radar speedRadar;    // Police cars usually have a speed radar
    void Start()
    {
        this.canChaseTaxi = true;
        this.isSolidObject = true;
        this.pointsToSubstract = -30;
        //this.speedMultiplier = 0.8f;
        this.duration = 1;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
