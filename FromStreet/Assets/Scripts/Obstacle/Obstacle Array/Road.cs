using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour, IObjectPoolMessage
{
    [SerializeField] private GameObject[] _child = null;

    [SerializeField] private int _poolingMaxRoadObstacleNum = 0;

    [SerializeField] private float[] _intervals = null;

    private List<GameObject> _listPushedObstacles = new List<GameObject>();

    private ObstacleSpawn _obstacleSpawn = null;

    private Vector3 _spawnPosition = Vector3.zero;

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
        float randomSpeed = Random.Range(4f, 9f);

        for (int i = 0; i < _poolingMaxRoadObstacleNum; ++i)
        {
            _listPushedObstacles.Add(_obstacleSpawn.GiveObstacle(EObstacleTypes.Car));

            float randomNum = Random.Range(_intervals[ConstantValue.MIN_INTERVAL_NUM], _intervals[ConstantValue.MAX_INTERVAL_NUM]);

            if (_spawnPosition.x >= 0)
            {
                _spawnPosition.x += (i * randomNum);

                _listPushedObstacles[i].transform.rotation = Quaternion.Euler(0f, -90f, 0f);
            }
            else
            {
                _spawnPosition.x -= (i * randomNum);

                _listPushedObstacles[i].transform.rotation = Quaternion.Euler(0f, 90f, 0f);
            }

            Vector3 currPos = new Vector3(_spawnPosition.x, _spawnPosition.y, posZ);

            _listPushedObstacles[i].transform.position = currPos;

            _listPushedObstacles[i].gameObject.GetComponent<IMovableObstacleMessage>()?.SetMovableObstacleInfomations(randomSpeed, _spawnPosition, _listPushedObstacles[i].gameObject.transform);
        }
    }
}
