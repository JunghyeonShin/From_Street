using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 1f;

    private PlayerInput _playerInput = null;
    private Rigidbody _playerRigidBody = null;

    private void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
        _playerRigidBody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Move();   
    }

    private void Move()
    {
        Vector3 _moveVec = new Vector3(_playerInput.MoveHorizontal, 0f, _playerInput.MoveVertical).normalized;

        _playerRigidBody.position += _moveSpeed * Time.deltaTime * _moveVec;
    }
}
