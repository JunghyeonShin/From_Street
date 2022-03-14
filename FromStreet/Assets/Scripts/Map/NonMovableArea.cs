using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonMovableArea : MonoBehaviour
{
    [SerializeField] private GameObject _player = null;

    private Transform _nonMovableAreaTransform = null;

    private float _distance = 0f;

    private readonly Vector3 ADJUST_POSITION_VECTOR = new Vector3(0f, 0f, 5f);

    void Start()
    {
        _nonMovableAreaTransform = GetComponent<Transform>();
    }

    private void LateUpdate()
    {
        _distance = _player.transform.position.z - _nonMovableAreaTransform.transform.position.z;

        if (_distance > 10f)
        {
            _nonMovableAreaTransform.transform.position += ADJUST_POSITION_VECTOR;
        }
    }
}
