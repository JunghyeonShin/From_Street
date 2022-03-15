using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TileInfomations
{
    [SerializeField] private GameObject _obj = null;

    [SerializeField] private ETileTypes _type = ETileTypes.Pavement;

    [SerializeField] private float _weight = 0f;

    [SerializeField] private int _minValue = 0;
    [SerializeField] private int _maxValue = 0;
    [SerializeField] private int _objSize = 0;

    public GameObject Prefab { get { return _obj; } }

    public ETileTypes TileType { get { return _type; } }

    public float Weight { get { return _weight; } }

    public int MinValue { get { return _minValue; } }

    public int MaxValue { get { return _maxValue; } }

    public int PoolingObjectSize { get { return _objSize; } }
}

[Serializable]
public class SampleTileInfomations
{
    [SerializeField] private ETileTypes _sampleTileType = ETileTypes.Pavement;

    [SerializeField] private int _sampleTileCount = 0;

    public ETileTypes SampleTileType { get { return _sampleTileType; } }

    public int SampleTileCount { get { return _sampleTileCount; } }
}

public class TileSpawn : MonoBehaviour
{
    [SerializeField] private List<TileInfomations> _tileInfos = null;

    [SerializeField] private int _initTileNumber = 0;

    [SerializeField] private List<SampleTileInfomations> _sampleTileInfos = null;

    private Dictionary<ETileTypes, ObjectPool> _tileDictionaries = new Dictionary<ETileTypes, ObjectPool>();

    private Queue<GameObject> _createdTiles = new Queue<GameObject>();

    private Queue<ETileTypes> _listTileType = new Queue<ETileTypes>();

    private ETileTypes _createNextTileType = ETileTypes.Pavement;
    private ETileTypes _lastTileType = ETileTypes.Pavement;

    private float _currPosZ = 6f;


    public ETileTypes CreateNextTileType { get { return _createNextTileType; } }

    private void Start()
    {
        for (int i = 0; i < _tileInfos.Count; ++i)
        {
            ObjectPool tempPool = new ObjectPool();

            _tileDictionaries[_tileInfos[i].TileType] = tempPool;

            tempPool.InitializeObjectPool(_tileInfos[i].PoolingObjectSize, _tileInfos[i].Prefab);
        }

        CreateInitTiles();
    }

    public void ReturnTile(ETileTypes type, GameObject obj)
    {
        _createdTiles.Dequeue();

        _tileDictionaries[type].ReturnObject(obj);

        if (ConstantValue.EMPTY == _listTileType.Count)
        {
            ListUpTileType();
        }

        _createNextTileType = _listTileType.Dequeue();

        PushTile(_createNextTileType);
    }

    private void CreateInitTiles()
    {
        for (int i = 0; i < ConstantValue.READY_TILE_NUMBER; ++i)
        {
            PushTile(ETileTypes.Pavement);
        }

        for (int i = 0; i < _sampleTileInfos.Count; ++i)
        {
            for (int j = 0; j < _sampleTileInfos[i].SampleTileCount; ++j)
            {
                PushTile(_sampleTileInfos[i].SampleTileType);
            }
        }

        while (_createdTiles.Count <= _initTileNumber)
        {
            ListUpTileType();

            for (int i = 0; i < _listTileType.Count;)
            {
                _createNextTileType = _listTileType.Dequeue();

                PushTile(_createNextTileType);
            }
        }
    }

    private void ListUpTileType()
    {
        ETileTypes type = SelectTileType();

        while (type == _lastTileType)
        {
            type = SelectTileType();
        }

        _lastTileType = type;

        int _randomTileNumber = UnityEngine.Random.Range(_tileInfos[(int)type].MinValue, _tileInfos[(int)type].MaxValue);

        for (int i = 0; i < _randomTileNumber; ++i)
        {
            _listTileType.Enqueue(type);
        }
    }

    private void PushTile(ETileTypes type)
    {
        GameObject obj = _tileDictionaries[type].GiveObject(_currPosZ);

        Vector3 currPos = new Vector3(0f, 0f, _currPosZ);

        obj.transform.position = currPos;

        _createdTiles.Enqueue(obj);

        _currPosZ += ConstantValue.TILE_SIZE;
    }

    private ETileTypes SelectTileType()
    {
        float total = 0f;

        for (int i = 0; i < _tileInfos.Count; ++i)
        {
            total += _tileInfos[i].Weight;
        }

        float randomValue = UnityEngine.Random.value * total;

        for (int i = 0; i < _tileInfos.Count; ++i)
        {
            if (randomValue < _tileInfos[i].Weight)
            {
                return (ETileTypes)i;
            }
            else
            {
                randomValue -= _tileInfos[i].Weight;
            }
        }

        return ETileTypes.Pavement;
    }
}
