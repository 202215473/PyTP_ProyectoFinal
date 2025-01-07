using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Client : MonoBehaviour
{
    private Vector3 destination;
    private Vector3 position;
    private bool isPickedUp = false;

    public Vector3 GetDestination()
    {
        return destination;
    }

    public void SetDestination(Vector3 destination)
    {
        this.destination = destination;
    }

    public Vector3 GetPosition()
    {
        return position;
    }

    public void SetPosition(Vector3 position)
    {
        this.position = position;
    }

    public bool IsPickedUp()
    {
        return isPickedUp;
    }

    public void SetIsPickedUp(bool isPickedUp)
    {
        this.isPickedUp = isPickedUp;
    }
}
