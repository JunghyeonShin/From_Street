using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public bool MoveForward { get; private set; }
    public bool MoveBack {get; private set;}
    public bool MoveLeft {get; private set;}
    public bool MoveRight {get; private set;}

    private void Update()
    {
        MoveForward = Input.GetKeyDown(KeyCode.W);
        MoveBack = Input.GetKeyDown(KeyCode.S);
        MoveLeft = Input.GetKeyDown(KeyCode.A);
        MoveRight = Input.GetKeyDown(KeyCode.D);
    }
}
