using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public float GetMoveInput()
        { return Input.GetAxis("Vertical"); }
    public float GetTurnInput() 
        { return Input.GetAxis("Horizontal"); }
    


}