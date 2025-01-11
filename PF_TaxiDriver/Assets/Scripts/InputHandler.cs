using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public event Action userPressedSpace;

    void Update()
    {
        CheckSpacePressed();
    }
    public float GetMoveInput()
        { return Input.GetAxis("Vertical"); }
    public float GetTurnInput() 
        { return Input.GetAxis("Horizontal"); }
    
    public void CheckSpacePressed()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            userPressedSpace.Invoke();
        }
    }

}
