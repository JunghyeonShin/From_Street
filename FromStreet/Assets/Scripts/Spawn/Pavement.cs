using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pavement : MonoBehaviour, IObjectPoolMessage
{
    private List<GameObject> _listPushedObstacles = new List<GameObject>();

    private FixedObjectPositioningMap _fixedObjectPositioningMap = new FixedObjectPositioningMap();

    private ObstacleSpawn _obstacleSpawn = null;

    public void OnPulled(float posZ)
    {
        _fixedObjectPositioningMap.CreateFixedObstaclePosition();

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

                float posX = 6f - (i * 2f);

                Vector3 currPos = new Vector3(posX, 0f, posZ);

                _listPushedObstacles[pushedObstacleCount].transform.position = currPos;

                ++pushedObstacleCount;
            }

            temp <<= 1;
        }
    }
}
