using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Train : MonoBehaviour, IMovableObstacleMessage
{
    private Transform _transform = null;

    private Vector3 _spawnPosition = Vector3.zero;

    private float _moveSpeed = 0f;

    private void Update()
    {
        Vector3 moveVec = _moveSpeed * Time.deltaTime * _transform.forward;

        _transform.position += moveVec;

        if (_spawnPosition.x > 0)
        {
            if (_transform.position.x < -_spawnPosition.x)
            {
                _transform.position += _spawnPosition * 2;
            }
        }
        else if (_spawnPosition.x < 0)
        {
            if (_transform.position.x > -_spawnPosition.x)
            {
                _transform.position += _spawnPosition * 2;
            }
        }
    }

    public void SetMovableObstacleInfomations(float moveSpeed, Vector3 spawnPosition, Transform transform)
    {
        _moveSpeed = moveSpeed;

        _spawnPosition = spawnPosition;

        Vector3 adjustVec = new Vector3(0f, _spawnPosition.y, 0f);

        _spawnPosition -= adjustVec;

        _transform = transform;
    }
}
