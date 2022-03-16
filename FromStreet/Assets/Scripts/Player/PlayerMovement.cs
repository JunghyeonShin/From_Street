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

    private Vector3 _bezierStartPoint = Vector3.zero;
    private Vector3 _bezierTempPoint = Vector3.zero;
    private Vector3 _bezierEndPoint = Vector3.zero;

    private EPlayerMoveDirections _playerDirection = EPlayerMoveDirections.None;

    private bool _isDie = false;
    private bool _isJumpMoving = false;

    private Transform _boatTransform = null;

    private bool _isOnBoat = false;

    private float _adjustForwardMiddlePosX = 0f;
    private float _adjustBackMiddlePosX = 0f;
    private float _boatSpeed = 0f;

    private const float MAX_RAY_DISTANCE = 20f;

    private const int LAYER_NON_MOVABLE_AREA = 6;
    private const int LAYER_TREE = 7;
    private const int LAYER_BOAT = 8;
    private const int LAYER_CAR = 9;
    private const int LAYER_TRAIN = 10;
    private const int LAYER_DEAD_LINE = 11;

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

    private void OnTriggerEnter(Collider other)
    {
        if (LAYER_NON_MOVABLE_AREA == other.gameObject.layer || LAYER_CAR == other.gameObject.layer || LAYER_TRAIN == other.gameObject.layer || LAYER_DEAD_LINE == other.gameObject.layer)
        {
            _isDie = true;

            GameManager.Instance.EndGame();

            _playerTransform.gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (LAYER_BOAT == collision.gameObject.layer)
        {
            _isOnBoat = true;

            _boatTransform = collision.gameObject.GetComponent<Boat>().TransForm;

            _boatSpeed = collision.gameObject.GetComponent<Boat>().MoveSpeed;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (LAYER_BOAT == collision.gameObject.layer)
        {
            _isOnBoat = false;
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

        if (_isOnBoat)
        {
            Vector3 takeBoatVec = _boatSpeed * Time.deltaTime * _boatTransform.forward;

            _playerTransform.position += takeBoatVec;
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

        float adjustPlayerPosX = _playerTransform.position.x;

        if (adjustPlayerPosX < -7)
        {
            adjustPlayerPosX = -8;
        }
        else if (-7 <= adjustPlayerPosX && adjustPlayerPosX < -5)
        {
            adjustPlayerPosX = -6;
        }
        else if (-5 <= adjustPlayerPosX && adjustPlayerPosX < -3)
        {
            adjustPlayerPosX = -4;
        }
        else if (-3 <= adjustPlayerPosX && adjustPlayerPosX < -3)
        {
            adjustPlayerPosX = -2;
        }
        else if (-1 <= adjustPlayerPosX && adjustPlayerPosX < 1)
        {
            adjustPlayerPosX = 0;
        }
        else if (1 <= adjustPlayerPosX && adjustPlayerPosX < 3)
        {
            adjustPlayerPosX = 2;
        }
        else if (3 <= adjustPlayerPosX && adjustPlayerPosX < 5)
        {
            adjustPlayerPosX = 4;
        }
        else if (5 <= adjustPlayerPosX && adjustPlayerPosX < 7)
        {
            adjustPlayerPosX = 6;
        }
        else if (7 <= adjustPlayerPosX)
        {
            adjustPlayerPosX = 8;
        }

        Vector3 _adjustPlayerPos = new Vector3(adjustPlayerPosX, _playerTransform.position.y, _playerTransform.position.z);


        switch (_playerDirection)
        {
            case EPlayerMoveDirections.Forward:
                _bezierEndPoint = _adjustPlayerPos + _nextForwardPoint;

                _adjustForwardMiddlePosX = (_adjustPlayerPos.x - _bezierEndPoint.x) / 2;

                Vector3 moveToForwardBetweenTwoPoints = new Vector3(_adjustForwardMiddlePosX, 1f, 1f);

                _bezierTempPoint = _adjustPlayerPos + moveToForwardBetweenTwoPoints;
                break;
            case EPlayerMoveDirections.Back:
                _bezierEndPoint = _adjustPlayerPos + _nextBackPoint;

                _adjustBackMiddlePosX = (_adjustPlayerPos.x - _bezierEndPoint.x) / 2;

                Vector3 moveToBackBetweenTwoPoints = new Vector3(_adjustBackMiddlePosX, 1f, -1f);

                _bezierTempPoint = _adjustPlayerPos + moveToBackBetweenTwoPoints;
                break;
            case EPlayerMoveDirections.Left:
                _bezierTempPoint = _adjustPlayerPos + _moveToLeftBetweenTwoPoints;

                _bezierEndPoint = _adjustPlayerPos + _nextLeftPoint;
                break;
            case EPlayerMoveDirections.Right:
                _bezierTempPoint = _adjustPlayerPos + _moveToRightBetweenTwoPoints;

                _bezierEndPoint = _adjustPlayerPos + _nextRightPoint;
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
