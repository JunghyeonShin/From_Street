using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private PlayerMovement _playerMovement = null;

    private Vector2[] _touchedPositions = new Vector2[3];

    private float[] _dotVectors = new float[2];

    private Touch _touch;

    private bool _isTouch = false;

    public bool MoveForward { get; private set; }
    public bool MoveBack { get; private set; }
    public bool MoveLeft { get; private set; }
    public bool MoveRight { get; private set; }


    private void Update()
    {
        if (GameManager.Instance.IsGameOver || _playerMovement.PlayerDie || _playerMovement.PlayerJumpMoving)
        {
            return;
        }

        if (_isTouch)
        {
            _touchedPositions[0] = Vector2.zero;
            _touchedPositions[1] = Vector2.zero;
            _touchedPositions[2] = Vector2.zero;
        }

        _isTouch = false;

        MoveForward = Input.GetKeyDown(KeyCode.W);
        MoveBack = Input.GetKeyDown(KeyCode.S);
        MoveLeft = Input.GetKeyDown(KeyCode.A);
        MoveRight = Input.GetKeyDown(KeyCode.D);

        if (Input.touchCount > 0)
        {
            _touch = Input.GetTouch(0);

            if (TouchPhase.Began == _touch.phase)
            {
                _touchedPositions[0] = Camera.main.ScreenToWorldPoint(_touch.position);
            }
            else if (TouchPhase.Ended == _touch.phase)
            {
                _touchedPositions[1] = Camera.main.ScreenToWorldPoint(_touch.position);

                _touchedPositions[2] = _touchedPositions[1] - _touchedPositions[0];

                _dotVectors[0] = Vector2.Dot(_touchedPositions[2].normalized, Vector2.up);
                _dotVectors[1] = Vector2.Dot(_touchedPositions[2].normalized, Vector2.right);

                _isTouch = true;

                Debug.Log($"_touchedPositions[2].magnitude : {_touchedPositions[2].magnitude}");
            }
        }

        if (_isTouch)
        {
            if (_dotVectors[0] >= 0.7)
            {
                MoveForward = true;
            }
            else if (_dotVectors[0] <= -0.7)
            {
                MoveBack = true;
            }
            else if (_dotVectors[1] < 0.7)
            {
                MoveLeft = true;
            }
            else if (_dotVectors[1] > 0.7)
            {
                MoveRight = true;
            }

            if (_touchedPositions[2].magnitude <= 0.15f)
            {
                MoveForward = true;
                MoveBack = false;
                MoveLeft = false;
                MoveRight = false;
            }
        }
    }
}
