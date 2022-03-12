using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    [SerializeField] private float _rotateSpeed = 0f;

    private PlayerInput _playerInput = null;

    private Transform _playerTransform = null;

    private EPlayerMoveDirections _playerDir = EPlayerMoveDirections.Forward;

    private bool _isRotation = false;

    private bool _sendCoroutineStartMessage = false;

    private float _angle = 0f;

    private void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
        _playerTransform = GetComponent<Transform>();
    }

    private void Update()
    {
        if (false == _isRotation)
        {
            CheckInputMessage();
        }

        if (_sendCoroutineStartMessage)
        {
            StartCoroutine(Rotation(_angle));
        }
    }

    private void CheckInputMessage()
    {
        if (_playerInput.MoveForward)
        {
            _playerDir = EPlayerMoveDirections.Forward;
            _angle = 0f;
            _isRotation = true;
            _sendCoroutineStartMessage = true;
        }
        else if (_playerInput.MoveBack)
        {
            _playerDir = EPlayerMoveDirections.Back;
            _angle = 180f;
            _isRotation = true;
            _sendCoroutineStartMessage = true;
        }
        else if (_playerInput.MoveLeft)
        {
            _playerDir = EPlayerMoveDirections.Left;
            _angle = -90f;
            _isRotation = true;
            _sendCoroutineStartMessage = true;
        }
        else if (_playerInput.MoveRight)
        {
            _playerDir = EPlayerMoveDirections.Right;
            _angle = 90f;
            _isRotation = true;
            _sendCoroutineStartMessage = true;
        }
    }

    private IEnumerator Rotation(float angle)
    {
        _sendCoroutineStartMessage = false;

        while (_isRotation)
        {
            _playerTransform.Rotate(new Vector3(0f, _rotateSpeed * Time.deltaTime, 0f));

            if (_playerTransform.localEulerAngles.y > angle)
            {
                _isRotation = false;
            }

            yield return null;
        }

        Debug.Log($"{_playerTransform.localEulerAngles.y}");
    }
}
