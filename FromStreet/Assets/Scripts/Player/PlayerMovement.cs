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
        Vector3 _moveVec = Vector3.zero;

        if (_playerInput.MoveForward)
        {
            _moveVec = new Vector3(0f, 0f, MOVE_SPEED);
        }
        else if (_playerInput.MoveBack)
        {
            _moveVec = new Vector3(0f, 0f, -MOVE_SPEED);
        }
        else if (_playerInput.MoveLeft)
        {
            _moveVec = new Vector3(-MOVE_SPEED, 0f, 0f);
        }
        else if (_playerInput.MoveRight)
        {
            _moveVec = new Vector3(MOVE_SPEED, 0f, 0f);
        }

        _playerRigidBody.position += _moveVec;
    }
}
