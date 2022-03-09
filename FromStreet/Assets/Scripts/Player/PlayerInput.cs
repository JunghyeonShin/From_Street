using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private const string VERTICAL = "Vertical";
    private const string HORIZONTAL = "Horizontal";

    public float MoveVertical { get; private set; }

    public float MoveHorizontal { get; private set; }

    private void Update()
    {
        MoveVertical = Input.GetAxisRaw(VERTICAL);
        MoveHorizontal = Input.GetAxisRaw(HORIZONTAL);
    }
}
