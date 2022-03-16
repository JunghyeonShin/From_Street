using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private PlayerMovement _playerMovement = null;

    private Touch _touch;

    private Vector3[] _touchedPos = new Vector3[2];

    private const string INITIALIZE_TOUCH = "InitializeTouch";
    public bool MoveForward { get; private set; }
    public bool MoveBack { get; private set; }
    public bool MoveLeft { get; private set; }
    public bool MoveRight { get; private set; }


    private void Update()
    {
        if (GameManager.Instance.IsGameOver || _playerMovement.PlayerDie)
        {
            return;
        }

        MoveForward = Input.GetKeyDown(KeyCode.W);
        MoveBack = Input.GetKeyDown(KeyCode.S);
        MoveLeft = Input.GetKeyDown(KeyCode.A);
        MoveRight = Input.GetKeyDown(KeyCode.D);

        if (Input.touchCount > 0)
        {
            _touch = Input.GetTouch(0);

            if (TouchPhase.Began == _touch.phase)
            {
                _touchedPos[0] = Camera.main.ScreenToWorldPoint(_touch.position);
            }
            if (TouchPhase.Ended == _touch.phase)
            {
                _touchedPos[1] = Camera.main.ScreenToWorldPoint(_touch.position);
            }

            float _distanceY = _touchedPos[1].y - _touchedPos[0].y;
            float _distanceX = _touchedPos[1].x - _touchedPos[0].x;

            if (_distanceX <= 5f)
            {
                if (_distanceY >= 0f)
                {
                    MoveForward = true;
                }
                else
                {
                    MoveBack = true;
                }
            }
            else
            {
                if (_distanceY >= 0f)
                {
                    MoveRight = true;
                }
                else
                {
                    MoveLeft = true;
                }
            }
            
            Invoke(INITIALIZE_TOUCH, 10f);
        }
    }

    private void InitializeTouch()
    {
        MoveForward = false;
        MoveBack = false;
        MoveLeft = false;
        MoveRight = false;
    }
}
