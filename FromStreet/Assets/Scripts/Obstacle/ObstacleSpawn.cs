using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EObstacleTypes { Tree, Car, Boat, Train }

public interface IObjectPoolMessage
{
    public void OnPulled();

    public void OnPushed();
}

[Serializable]
public class ObstacleInfomations
{
    [SerializeField] private GameObject _obj;

    [SerializeField] private EObstacleTypes _type;

    [SerializeField] private int _objSize;

    public GameObject Prefab { get { return _obj; } }
    public EObstacleTypes ObstacleType { get { return _type; } }
    public int PoolingObjectSize { get { return _objSize; } }
}

public class ObstacleSpawn : MonoBehaviour
{
    [SerializeField] private List<ObstacleInfomations> _obstacleInfos;

    private Dictionary<EObstacleTypes, ObjectPool> _obstacleDitionaries = new Dictionary<EObstacleTypes, ObjectPool>();

    private void Awake()
    {
        for (int i=0;i<_obstacleInfos.Count; ++i)
        {
            ObjectPool tempPool = new ObjectPool();

            _obstacleDitionaries[_obstacleInfos[i].ObstacleType] = tempPool;

            tempPool.InitializeObjectPool(_obstacleInfos[i].PoolingObjectSize, _obstacleInfos[i].Prefab);
        }
    }
}
