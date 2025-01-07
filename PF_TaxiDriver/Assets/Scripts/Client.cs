using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Client : MonoBehaviour
{
    private Vector3 destination;

    public Vector3 GetDestination()
    {
        return destination;
    }

    public void SetDestination(Vector3 destination)
    {
        this.destination = destination;
    }
}
