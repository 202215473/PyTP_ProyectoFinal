using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private new BoxCollider collider;

    protected bool canChaseTaxi;
    protected bool isSolidObject;
    protected int pointsToSubstract;
    protected float moneyToSubstract;

    //public Obstacle()
    //{ }
    void Start()
    {
        this.collider = gameObject.GetComponent<BoxCollider>();
    }
    //void Update()
    //{
    //}
    public int GetPointsToSubstract()
    { return pointsToSubstract; }
    public float GetMoneyToSubstract()
    { return moneyToSubstract; }

}
