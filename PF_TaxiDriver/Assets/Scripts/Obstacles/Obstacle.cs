using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private new BoxCollider collider;

    protected bool isSolidObject;
    protected int pointsToSubstract;
    protected float moneyToSubstract;

    void Start()
    {
        this.collider = gameObject.GetComponent<BoxCollider>();
    }

    public int GetPointsToSubstract()
    { return pointsToSubstract; }
    public float GetMoneyToSubstract()
    { return moneyToSubstract; }
