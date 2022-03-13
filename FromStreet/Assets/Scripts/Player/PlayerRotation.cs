using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    [SerializeField] private float _rotateTime = 0f;

    private PlayerInput _playerInput = null;

    private bool _isRotation = false;

    private float _angle = 0f;

    private void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        if (false == _isRotation)
        {
            CheckInputMessage();

            StartCoroutine(Rotation(_angle));
        }
    }

    private void CheckInputMessage()
    {
        if (_playerInput.MoveForward)
        {
            _angle = 0f;
            _isRotation = true;
        }
        else if (_playerInput.MoveBack)
        {
            _angle = 180f;
            _isRotation = true;
        }
        else if (_playerInput.MoveLeft)
        {
            _angle = -90f;
            _isRotation = true;
        }
        else if (_playerInput.MoveRight)
        {
            _angle = 90f;
            _isRotation = true;
        }
    }

    private IEnumerator Rotation(float angle)
    {
        float elapsedTime = 0f;

        Quaternion start = transform.rotation;
        Quaternion dest = Quaternion.Euler(0, angle, 0);

        while (_isRotation)
        {
            elapsedTime += Time.deltaTime;

            if(elapsedTime >= _rotateTime)
            {
                elapsedTime = _rotateTime;
                _isRotation = false;
            }

            transform.rotation = Quaternion.Slerp(start, dest, elapsedTime / _rotateTime);

            yield return null;
        }
    }
}
