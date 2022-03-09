using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadLine : MonoBehaviour
{
    [SerializeField] private Camera _camera = null;
    [SerializeField] private float _moveSpeed = 0f;

    private Rigidbody _rigidBody;
    private Transform _transform;

    private Vector3 _distance = Vector3.zero;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _transform = GetComponent<Transform>();

        _distance = -_camera.transform.position;
    }

    private void Update()
    {
        Move();
    }

    private void LateUpdate()
    {
        _camera.transform.position = _transform.position - _distance;
    }

    private void OnTriggerExit(Collider other)
    {
        ETileTypes _type = ETileTypes.Pavement;

        switch (other.name)
        {
            case ConstantValue.PAVEMENT:
                _type = ETileTypes.Pavement;
                break;
            case ConstantValue.ROAD:
                _type = ETileTypes.Road;
                break;
            case ConstantValue.RAILWAY:
                _type = ETileTypes.RailWay;
                break;
            case ConstantValue.RIVER:
                _type = ETileTypes.River;
                break;
            default:
                break;
        }

        // 타입과 오브젝트를 전달하여 ReturnObject
        Debug.Log($"타일 타입 : {_type}");
    }

    private void Move()
    {
        Vector3 _moveVec = _moveSpeed * Time.deltaTime * transform.forward;

        _rigidBody.position += _moveVec;
    }
}
