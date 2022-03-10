using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadLine : MonoBehaviour
{
    [SerializeField] private RandomTiles _randomTiles = null;
    [SerializeField] private Camera _camera = null;
    [SerializeField] private float _moveSpeed = 0f;

    private Rigidbody _rigidBody;
    private Transform _transform;

    private Vector3 _distance = Vector3.zero;

    private const int LAYER_TILE = 3;

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
        ETileTypes type = ETileTypes.Pavement;

        switch (other.name)
        {
            case ConstantValue.PAVEMENT:
                type = ETileTypes.Pavement;
                break;
            case ConstantValue.ROAD:
                type = ETileTypes.Road;
                break;
            case ConstantValue.RAILWAY:
                type = ETileTypes.RailWay;
                break;
            case ConstantValue.RIVER:
                type = ETileTypes.River;
                break;
            default:
                break;
        }

        if (other.gameObject.layer == LAYER_TILE)
        {
            _randomTiles.ReturnTile(type, other.gameObject);
        }
    }

    private void Move()
    {
        Vector3 moveVec = _moveSpeed * Time.deltaTime * transform.forward;

        _rigidBody.position += moveVec;
    }
}
