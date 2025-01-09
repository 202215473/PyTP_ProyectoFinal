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

// EJEMPLO EVENTO

//public class Flamingo : MonoBehaviour
//{
//    [SerializeField] private CubeSpawner cubeSpawner;
//    [SerializeField] private Transform flamingoHead;

//    private void OnEnable()
//    {
//        cubeSpawner.onCubeSpawned += OnCubeSpawned;
//    }

//    private void OnDisable()
//    {
//        cubeSpawner.onCubeSpawned -= OnCubeSpawned;
//    }
//    private void OnCubeSpawned(Cube cube)
//    {
//        Debug.Log("I have received a cube");
//        cube.cubeCollidedWithFloor += OnCubeCollidedWithFloor;
//    }

//    private void OnCubeCollidedWithFloor(Cube cube)
//    {
//        flamingoHead.transform.LookAt(cube.transform);

//        flamingoHead.GetComponent<Renderer>().material.color =
//            cube.GetComponent<Renderer>().material.color;
//        cube.cubeCollidedWithFloor -= OnCubeCollidedWithFloor;
//    }
//}
