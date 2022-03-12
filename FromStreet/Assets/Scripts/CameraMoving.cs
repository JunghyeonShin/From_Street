using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoving : MonoBehaviour
{
    [SerializeField] private GameObject _player = null;

    [SerializeField] private float _moveSpeed = 0f;

    private Transform _cameraTransform = null;

    private void Start()
    {
        _cameraTransform = GetComponent<Transform>();
    }

    private void Update()
    {
        //Vector3 cameraVec = _cameraTransform.transform.forward;
        //Vector3 targetVec = _deadLine.gameObject.transform.position - _cameraTransform.position;

        //float scalarAngleBetweenCameraAndDeadLine = Vector3.Dot(cameraVec.normalized, targetVec.normalized);
        //float angleBetweenCameraAndDeadLine = Vector3.Angle(cameraVec, targetVec);

        //Debug.Log($"Camera Forward Vec : {cameraVec}\n Target Vec : {targetVec}");
        //Debug.Log($"scalarAngleBetweenCameraAndDeadLine : {scalarAngleBetweenCameraAndDeadLine}");
        //Debug.Log($"angleBetweenCameraAndDeadLine : {angleBetweenCameraAndDeadLine}");

        Move();
    }

    private void Move()
    {
        Vector3 moveVec = new Vector3(0f, 0f, _moveSpeed * Time.deltaTime);

        _cameraTransform.position += moveVec;
    }
}
