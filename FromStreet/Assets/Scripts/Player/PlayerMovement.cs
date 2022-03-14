using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _moveTime = 0f;

    private BezierCurve _bezierCurve = new BezierCurve();

    private PlayerInput _playerInput = null;

    private Transform _playerTransform = null;

    private RaycastHit _rayHit;

    private Vector3 _bezierStartPoint = new Vector3();
    private Vector3 _bezierTempPoint = new Vector3();
    private Vector3 _bezierEndPoint = new Vector3();

    private EPlayerMoveDirections _playerDirection = EPlayerMoveDirections.None;

    private bool _isDie = false;
    private bool _isJumpMoving = false;

    private const float MAX_RAY_DISTANCE = 20f;

    private const int LAYER_NON_MOVABLE_AREA = 6;
    private const int LAYER_TREE = 7;
    private const int LAYER_BOAT = 8;
    private const int LAYER_CAR = 9;
    private const int LAYER_TRAIN = 10;

    private readonly Vector3 _moveToForwardBetweenTwoPoints = new Vector3(0f, 1f, 1f);
    private readonly Vector3 _moveToBackBetweenTwoPoints = new Vector3(0f, 1f, -1f);
    private readonly Vector3 _moveToLeftBetweenTwoPoints = new Vector3(-1f, 1f, 0f);
    private readonly Vector3 _moveToRightBetweenTwoPoints = new Vector3(1f, 1f, 0f);

    private readonly Vector3 _nextForwardPoint = new Vector3(0f, 0f, 2f);
    private readonly Vector3 _nextBackPoint = new Vector3(0f, 0f, -2f);
    private readonly Vector3 _nextLeftPoint = new Vector3(-2f, 0f, 0f);
    private readonly Vector3 _nextRightPoint = new Vector3(2f, 0f, 0f);

    public EPlayerMoveDirections PlayerMoveDirection { get { return _playerDirection; } }

    public bool PlayerDie { get { return _isDie; } }

    private void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
        _playerTransform = GetComponent<Transform>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (LAYER_CAR == collision.gameObject.layer)
        {
            _isDie = true;

            GameManager.Instance.EndGame();

            _playerTransform.gameObject.SetActive(false);
        }
        else if (LAYER_TRAIN == collision.gameObject.layer)
        {
            _isDie = true;

            GameManager.Instance.EndGame();

            _playerTransform.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (false == _isJumpMoving && EPlayerMoveDirections.None == _playerDirection)
        {
            CheckInputMessage();

            MakeBezierPoint();

            CheckMovablePoint();

            StartCoroutine(JumpMoving());
        }     

        if (_isDie)
        {
            GameManager.Instance.EndGame();
        }
    }

    private void CheckInputMessage()
    {
        if (_playerInput.MoveForward)
        {
            _playerDirection = EPlayerMoveDirections.Forward;

            _isJumpMoving = true;
        }
        else if (_playerInput.MoveBack)
        {
            _playerDirection = EPlayerMoveDirections.Back;

            _isJumpMoving = true;
        }
        else if (_playerInput.MoveLeft)
        {
            _playerDirection = EPlayerMoveDirections.Left;

            _isJumpMoving = true;
        }
        else if (_playerInput.MoveRight)
        {
            _playerDirection = EPlayerMoveDirections.Right;

            _isJumpMoving = true;
        }
        else
        {
            _playerDirection = EPlayerMoveDirections.None;
        }
    }

    private void CheckMovablePoint()
    {
        Vector3 rayPosition = new Vector3(_bezierEndPoint.x, 12f, _bezierEndPoint.z);

        if (Physics.Raycast(rayPosition, Vector3.down, out _rayHit, MAX_RAY_DISTANCE))
        {
            if (LAYER_NON_MOVABLE_AREA == _rayHit.transform.gameObject.layer || LAYER_TREE == _rayHit.transform.gameObject.layer)
            {
                _isJumpMoving = false;

                _playerDirection = EPlayerMoveDirections.None;
            }
        }
    }

    private void MakeBezierPoint()
    {
        _bezierStartPoint = _playerTransform.position;

        switch (_playerDirection)
        {
            case EPlayerMoveDirections.Forward:
                _bezierTempPoint = _playerTransform.position + _moveToForwardBetweenTwoPoints;
                _bezierEndPoint = _playerTransform.position + _nextForwardPoint;
                break;
            case EPlayerMoveDirections.Back:
                _bezierTempPoint = _playerTransform.position + _moveToBackBetweenTwoPoints;
                _bezierEndPoint = _playerTransform.position + _nextBackPoint;
                break;
            case EPlayerMoveDirections.Left:
                _bezierTempPoint = _playerTransform.position + _moveToLeftBetweenTwoPoints;
                _bezierEndPoint = _playerTransform.position + _nextLeftPoint;
                break;
            case EPlayerMoveDirections.Right:
                _bezierTempPoint = _playerTransform.position + _moveToRightBetweenTwoPoints;
                _bezierEndPoint = _playerTransform.position + _nextRightPoint;
                break;
            default:
                break;
        }
    }

    private IEnumerator JumpMoving()
    {
        float elapsedTime = 0f;

        while (_isJumpMoving)
        {
            elapsedTime += Time.deltaTime;

            if (elapsedTime >= _moveTime)
            {
                elapsedTime = _moveTime;

                _isJumpMoving = false;
            }

            _playerTransform.position = _bezierCurve.OnePointBezierCurve(_bezierStartPoint, _bezierTempPoint, _bezierEndPoint, elapsedTime / _moveTime);

            yield return null;
        }

        _playerDirection = EPlayerMoveDirections.None;
    }
}
