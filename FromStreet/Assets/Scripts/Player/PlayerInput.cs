using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private PlayerMovement _playerMovement = null;

    private Vector2[] _touchedPositions = new Vector2[3];

    private float[] _dotVectors = new float[2];

    private bool _isTouch = false;

    private const float DOT_45_DEGREE = 0.7071068f;

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

        Touch _touch;

        if (Input.touchCount > 0)
        {
            _touch = Input.GetTouch(0);

            if (TouchPhase.Began == _touch.phase)
            {
                _touchedPositions[0] = _touch.position;
            }
            else if (TouchPhase.Ended == _touch.phase)
            {
                if (Vector2.zero == _touchedPositions[0])
                {
                    _touchedPositions[1] = Vector2.zero;
                    _touchedPositions[2] = Vector2.zero;

                    _isTouch = true;
                }
                else
                {
                    _touchedPositions[1] = _touch.position;

                    _touchedPositions[2] = _touchedPositions[1] - _touchedPositions[0];

                    _dotVectors[0] = Vector2.Dot(_touchedPositions[2].normalized, Vector2.up);
                    _dotVectors[1] = Vector2.Dot(_touchedPositions[2].normalized, Vector2.right);

                    _isTouch = true;
                }
            }
        }

        if (_isTouch)
        {
            if (_dotVectors[0] >= DOT_45_DEGREE)
            {
                MoveForward = true;
            }
            else if (_dotVectors[0] <= -DOT_45_DEGREE)
            {
                MoveBack = true;
            }
            else if (_dotVectors[1] < 0f)
            {
                MoveLeft = true;
            }
            else if (_dotVectors[1] > 0f)
            {
                MoveRight = true;
            }

            if (_touchedPositions[2].magnitude <= Screen.height * 0.05f)
            {
                MoveForward = true;
                MoveBack = false;
                MoveLeft = false;
                MoveRight = false;
            }
        }
    }
}
