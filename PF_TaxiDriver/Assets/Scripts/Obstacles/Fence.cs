using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fence : Obstacle
{
    void Start()
    {
        this.canChaseTaxi = false;
        this.isSolidObject = true;
        this.pointsToSubstract = -4;
        this.moneyToSubstract = -4f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
