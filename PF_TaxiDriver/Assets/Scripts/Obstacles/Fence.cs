using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fence : Obstacle
{
    void Start()
    {
        this.isSolidObject = true;
        this.pointsToSubstract = -15;
        this.moneyToSubstract = -4f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
