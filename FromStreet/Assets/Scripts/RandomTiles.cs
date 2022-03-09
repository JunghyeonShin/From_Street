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

    private Vector3 _currPos = Vector3.zero;

    private Queue<GameObject> _createdTiles = new Queue<GameObject>();

    private const float TILE_SIZE = 1f;

    private const int READY_TILE_NUMBER = 5;
    private const int MAX_TILE_NUMBER = 50;

    private void Start()
    {
        for (int i = 0; i < _tileInfos.Count; ++i)
        {
            ObjectPool _tempPool = new ObjectPool();

            _tileDictionaries[_tileInfos[i].TileType] = _tempPool;

            _tempPool.Initialize(_tileInfos[i].ObjectSize, _tileInfos[i].Prefab);
        }

        CreateRandomTile();
    }

    private void Update()
    {
        // 타일이 삭제되는지 Space바 클릭으로 확인 (삭제 꼐정)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ReturnTile();
        }
    }

    private void CreateRandomTile()
    {
        for (int i = 0; i < READY_TILE_NUMBER; ++i)
        {
            RenderTile(ETileTypes.Pavement);
        }

        do
        {
            ETileTypes _type = SelectTile();

            int _randomTileNumber = UnityEngine.Random.Range(_tileInfos[(int)_type].MinValue, _tileInfos[(int)_type].MaxValue);

            for (int i = 0; i < _randomTileNumber; ++i)
            {
                RenderTile(_type);
            }
        }
        while (_createdTiles.Count <= MAX_TILE_NUMBER);
    }

    private void RenderTile(ETileTypes type)
    {
        GameObject _obj = _tileDictionaries[type].GiveObject();
        _obj.transform.position = _currPos;

        _createdTiles.Enqueue(_obj);

        _currPos += Vector3.forward * TILE_SIZE;
    }

    private void ReturnTile()
    {
        ETileTypes _type = ETileTypes.Pavement;

        GameObject _obj = _createdTiles.Dequeue();

        switch(_obj.name)
        {
            case ConstantValue.PAVEMENT:
                _type = ETileTypes.Pavement;
                break;
            case ConstantValue.ROAD:
                _type = ETileTypes.Road;
                break;
            case ConstantValue.RAILWAY:
                _type = ETileTypes.RailWay;
                break;
            case ConstantValue.RIVER:
                _type = ETileTypes.River;
                break;
            default:
                break;
        }

        _tileDictionaries[_type].ReturnObject(_obj);
    }

    private ETileTypes SelectTile()
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
