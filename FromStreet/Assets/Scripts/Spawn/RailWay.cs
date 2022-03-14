using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailWay : MonoBehaviour, IObjectPoolMessage
{
    [SerializeField] private GameObject[] _child = null;

    private GameObject _pushedObstacle = null;

    private ObstacleSpawn _obstacleSpawn = null;

    private Vector3 _spawnPosition = Vector3.zero;

    public void OnPulled(float posZ)
    {
        SetCreatePosition();

        GameObject spawnManager = GameObject.Find(ConstantValue.OBSTACLE_SPAWN_MANAGER);

        _obstacleSpawn = spawnManager.GetComponent<ObstacleSpawn>();

        SetRailWayObstacle(posZ);
    }

    public void OnPushed()
    {
        _pushedObstacle.transform.position = Vector3.zero;

        _obstacleSpawn.ReturnObstacle(EObstacleTypes.Train, _pushedObstacle);
    }

    private void SetCreatePosition()
    {
        int randNum = Random.Range(0, 2);

        _spawnPosition = _child[randNum].transform.position;
    }

    private void SetRailWayObstacle(float posZ)
    {
        float moveAwayFromStartPoint = Random.Range(1f, 3f);

        _pushedObstacle = _obstacleSpawn.GiveObstacle(EObstacleTypes.Train);

        if (_spawnPosition.x >= 0)
        {
            _pushedObstacle.transform.rotation = Quaternion.Euler(0f, -90f, 0f);
        }
        else
        {
            _pushedObstacle.transform.rotation = Quaternion.Euler(0f, 90f, 0f);
        }

        _spawnPosition.x *= moveAwayFromStartPoint;

        Vector3 currPos = new Vector3(_spawnPosition.x, _spawnPosition.y, posZ);

        _pushedObstacle.transform.position = currPos;

        _pushedObstacle.gameObject.GetComponent<IMovableObstacleMessage>()?.SetMovableObstacleInfomations(20f, _spawnPosition, _pushedObstacle.gameObject.transform);
    }
}
