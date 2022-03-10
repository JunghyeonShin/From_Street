using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerInput _playerInput = null;
    private Rigidbody _playerRigidBody = null;

    private const float MOVE_SPEED = 2f;

    private void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
        _playerRigidBody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Move();   
    }

    private void Move()
    {
        Vector3 moveVec = Vector3.zero;

        if (_playerInput.MoveForward)
        {
            moveVec = new Vector3(0f, 0f, MOVE_SPEED);
        }
        else if (_playerInput.MoveBack)
        {
            moveVec = new Vector3(0f, 0f, -MOVE_SPEED);
        }
        else if (_playerInput.MoveLeft)
        {
            moveVec = new Vector3(-MOVE_SPEED, 0f, 0f);
        }
        else if (_playerInput.MoveRight)
        {
            moveVec = new Vector3(MOVE_SPEED, 0f, 0f);
        }

        _playerRigidBody.position += moveVec;
    }
}
