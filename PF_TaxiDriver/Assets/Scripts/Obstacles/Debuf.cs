using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debuf : Obstacle
{
    // Start is called before the first frame update
    void Start()
    {
        this.isSolidObject = false;
        this.pointsToSubstract = -2;
        this.moneyToSubstract = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
