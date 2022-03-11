using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EMovingObstacleTypes { Car, Boat, };

[Serializable]
public class MovingObstacleInfomations
{
    [SerializeField] private GameObject _obj = null;
    [SerializeField] private EMovingObstacleTypes _type = EMovingObstacleTypes.Car;

    [SerializeField] private int _objSize = 0;

    public GameObject Prefab { get { return _obj; } }

    public EMovingObstacleTypes MovingObstacleType { get { return _type; } }

    public int PoolingObjectSize { get { return _objSize; } }
}

public class MovingObstacleSpawn : MonoBehaviour
{
    [SerializeField] private List<MovingObstacleInfomations> _movingObstacleInfos = null;

    private Dictionary<EMovingObstacleTypes, ObjectPool> _movingObstacleDictionaries = new Dictionary<EMovingObstacleTypes, ObjectPool>();

    private void Start()
    {
        for (int i = 0; i < _movingObstacleInfos.Count; ++i)
        {
            ObjectPool tempPool = new ObjectPool();

            _movingObstacleDictionaries[_movingObstacleInfos[i].MovingObstacleType] = tempPool;

            tempPool.InitializeObjectPool(_movingObstacleInfos[i].PoolingObjectSize, _movingObstacleInfos[i].Prefab);
        }

        CreateInitMovingObstacle();
    }

    private void CreateInitMovingObstacle()
    {

    }


}
