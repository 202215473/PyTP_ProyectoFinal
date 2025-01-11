using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debuf : Obstacle
{
    void Start()
    {
        this.isSolidObject = false;
        this.pointsToSubstract = -6;
        this.moneyToSubstract = 0f;
    }

    void Update()
    {
        
    }
}
