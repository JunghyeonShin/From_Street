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

    public int ObjectSize { get { return _objSize; } }
}

public class RandomTiles : MonoBehaviour
{
    [SerializeField] private List<TileInfomations> _tileInfos = null;

    private Dictionary<ETileTypes, ObjectPool> _tileDictionaries = new Dictionary<ETileTypes, ObjectPool>();

    private Queue<GameObject> _createdTiles = new Queue<GameObject>();

    private Queue<ETileTypes> _listTileType = new Queue<ETileTypes>();

    private Vector3 _currPos = Vector3.zero;

    private ETileTypes _lastTileType = ETileTypes.Pavement;

    private const float TILE_SIZE = 2f;

    private const int READY_TILE_NUMBER = 5;
    private const int MAX_TILE_NUMBER = 20;

    private void Start()
    {
        for (int i = 0; i < _tileInfos.Count; ++i)
        {
            ObjectPool _tempPool = new ObjectPool();

            _tileDictionaries[_tileInfos[i].TileType] = _tempPool;

            _tempPool.InitializeObjectPool(_tileInfos[i].ObjectSize, _tileInfos[i].Prefab);
        }

        CreateInitTiles();
    }

    public void ReturnTile(ETileTypes type, GameObject obj)
    {
        _createdTiles.Dequeue();

        _tileDictionaries[type].ReturnObject(obj);

        if (_listTileType.Count == 0)
        {
            ListUpTileType();
        }

        PushTile(_listTileType.Dequeue());
    }

    private void CreateInitTiles()
    {
        for (int i = 0; i < READY_TILE_NUMBER; ++i)
        {
            PushTile(ETileTypes.Pavement);
        }

        while (_createdTiles.Count <= MAX_TILE_NUMBER)
        {
            ListUpTileType();

            for (int i = 0; i < _listTileType.Count;)
            {
                PushTile(_listTileType.Dequeue());
            }
        }
    }

    private void ListUpTileType()
    {
        ETileTypes _type = SelectTileType();

        while (_lastTileType == _type)
        {
            _type = SelectTileType();
        }

        _lastTileType = _type;

        int _randomTileNumber = UnityEngine.Random.Range(_tileInfos[(int)_type].MinValue, _tileInfos[(int)_type].MaxValue);

        for (int i = 0; i < _randomTileNumber; ++i)
        {
            _listTileType.Enqueue(_type);
        }
    }

    private void PushTile(ETileTypes type)
    {
        GameObject _obj = _tileDictionaries[type].GiveObject();

        _obj.transform.position = _currPos;

        _createdTiles.Enqueue(_obj);

        _currPos += Vector3.forward * TILE_SIZE;
    }

    private ETileTypes SelectTileType()
    {
        float _total = 0f;

        for (int i = 0; i < _tileInfos.Count; ++i)
        {
            _total += _tileInfos[i].Weight;
        }

        float randomValue = UnityEngine.Random.value * _total;

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
