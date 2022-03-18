using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pavement : MonoBehaviour, IObjectPoolMessage
{
    private List<GameObject> _listPushedObstacles = new List<GameObject>();

    private FixedObstaclePositioningMap _fixedObjectPositioningMap = new FixedObstaclePositioningMap();

    private ObstacleSpawn _obstacleSpawn = null;

    public void OnPulled(float posZ)
    {
        _fixedObjectPositioningMap.CreateFixedObstaclePosition();

        GameObject _remeberObject = GameObject.Find(ConstantValue.TILE_MAP);

        _remeberObject.gameObject.GetComponent<IRememberFixedObatclePosition>()?.RememberPoint(_fixedObjectPositioningMap.LastPosition);

        GameObject spawnManager = GameObject.Find(ConstantValue.OBSTACLE_SPAWN_MANAGER);

        _obstacleSpawn = spawnManager.GetComponent<ObstacleSpawn>();

        SetFixedObstacle(_fixedObjectPositioningMap.CreatablePosition, posZ);
    }

    public void OnPushed()
    {
        for (int i = 0; i < _listPushedObstacles.Count; ++i)
        {
            _listPushedObstacles[i].transform.position = Vector3.zero;

            _obstacleSpawn.ReturnObstacle(EObstacleTypes.Tree, _listPushedObstacles[i]);
        }

        _listPushedObstacles.Clear();
    }

    private void SetFixedObstacle(int pos, float posZ)
    {
        int pushedObstacleCount = 0;
        int temp = 1;

        for (int i = 0; i < ConstantValue.MAX_FIXED_OBSTACLE_POSITION_INDEX; ++i)
        {
            if (temp == (pos & temp))
            {
                _listPushedObstacles.Add(_obstacleSpawn.GiveObstacle(EObstacleTypes.Tree));

                float posX = ConstantValue.FIXED_OBSTACLE_INITIAL_POSITION_X - (i * 2f);

                Vector3 currPos = new Vector3(posX, 0f, posZ);

                _listPushedObstacles[pushedObstacleCount].transform.position = currPos;

                ++pushedObstacleCount;
            }

            temp <<= 1;
        }
    }
}
