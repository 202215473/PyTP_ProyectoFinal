using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceCar : Obstacle
{
    [SerializeField] Radar speedRadar;    // Police cars usually have a speed radar
    void Start()
    {
        this.isSolidObject = true;
        this.pointsToSubstract = -30;
        this.moneyToSubstract = -5f;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
