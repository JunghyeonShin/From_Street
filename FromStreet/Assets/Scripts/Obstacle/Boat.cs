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
    }

    public void SetMovableObstacleInfomations(float moveSpeed, Transform transform)
    {
        _moveSpeed = moveSpeed;

        _transform = transform;
    }
}
