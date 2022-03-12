using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerInput _playerInput = null;

    private Rigidbody _playerRigidBody = null;

    private Transform _playerTransform = null;

    private const float MOVE_SPEED = 2f;
    private const float ROTATION_DIRECTION = 90f;

    private void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
        _playerRigidBody = GetComponent<Rigidbody>();
        _playerTransform = GetComponent<Transform>();
    }

    private void Update()
    {
        Rotation();

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

    private void Rotation()
    {
        Vector3 rotationVec = Vector3.zero;

        if (_playerInput.MoveForward)
        {
            rotationVec = new Vector3(0f, 0f, 0f);
            _playerTransform.rotation = Quaternion.Euler(rotationVec);
        }
        else if (_playerInput.MoveBack)
        {
            rotationVec = new Vector3(0f, ROTATION_DIRECTION * 2, 0f);
            _playerTransform.rotation = Quaternion.Euler(rotationVec);
        }
        else if (_playerInput.MoveLeft)
        {
            rotationVec = new Vector3(0f, -ROTATION_DIRECTION, 0f);
            _playerTransform.rotation = Quaternion.Euler(rotationVec);
        }
        else if (_playerInput.MoveRight)
        {
            rotationVec = new Vector3(0f, ROTATION_DIRECTION, 0f);
            _playerTransform.rotation = Quaternion.Euler(rotationVec);
        }

        //_playerTransform.Rotate(new Vector3(0f, 90f, 0f) * Time.deltaTime);
    }


}
