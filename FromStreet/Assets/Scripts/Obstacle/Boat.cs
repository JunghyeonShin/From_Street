using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : MonoBehaviour, IMovableObstacleMessage
{
    private Transform _transform = null;

    private float _moveSpeed = 0f;

    private void Update()
    {
        Vector3 moveVec = _moveSpeed * Time.deltaTime * _transform.forward;

        _transform.position += moveVec;

        Vector3 adjustVec = new Vector3(50f, 0f, 0f);

        if (_transform.position.x > 25f)
        {
            _transform.position -= adjustVec;
        }
        else if (_transform.position.x < -25f)
        {
            _transform.position += adjustVec;
        }
    }

    public void SetMovableObstacleInfomations(float moveSpeed, Vector3 spawnPosition, Transform transform)
    {
        _moveSpeed = moveSpeed;

        _transform = transform;
    }
}
