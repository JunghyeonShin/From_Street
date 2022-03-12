using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour, IObjectPoolMessage
{
    [SerializeField] private GameObject[] _child = null;

    [SerializeField] private float[] _intervals = null;

    private List<GameObject> _listPushedObstacles = new List<GameObject>();

    private ObstacleSpawn _obstacleSpawn = null;

    private Vector3 _spawnPosition = Vector3.zero;

    private const int POOLING_MAX_ROAD_OBSTACLE_NUM = 3;

    public void OnPulled(float posZ)
    {
        SetCreatePosition();

        GameObject spawnManager = GameObject.Find(ConstantValue.OBSTACLE_SPAWN_MANAGER);

        _obstacleSpawn = spawnManager.GetComponent<ObstacleSpawn>();

        SetRoadObstacle(posZ);
    }

    public void OnPushed()
    {
        for (int i = 0; i < _listPushedObstacles.Count; ++i)
        {
            _listPushedObstacles[i].transform.position = Vector3.zero;

            _obstacleSpawn.ReturnObstacle(EObstacleTypes.Car, _listPushedObstacles[i]);
        }

        _listPushedObstacles.Clear();
    }

    private void SetCreatePosition()
    {
        int randNum = Random.Range(0, 2);

        _spawnPosition = _child[randNum].transform.position;
    }

    private void SetRoadObstacle(float posZ)
    {
        for (int i = 0; i < POOLING_MAX_ROAD_OBSTACLE_NUM; ++i)
        {
            _listPushedObstacles.Add(_obstacleSpawn.GiveObstacle(EObstacleTypes.Car));

            float randomNum = Random.Range(_intervals[ConstantValue.MIN_INTERVAL_NUM], _intervals[ConstantValue.MAX_INTERVAL_NUM]);

            if (_spawnPosition.x >= 0)
            {
                _spawnPosition.x += (i * randomNum);

                _listPushedObstacles[i].transform.rotation = Quaternion.Euler(0f, -180f, 0f);
            }
            else
            {
                _spawnPosition.x -= (i * randomNum);

                _listPushedObstacles[i].transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            }

            Vector3 currPos = new Vector3(_spawnPosition.x, 0f, posZ);

            _listPushedObstacles[i].transform.position = currPos;
        }
    }
}