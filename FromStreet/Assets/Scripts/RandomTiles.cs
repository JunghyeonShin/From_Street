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

    private void Start()
    {
        for (int i = 0; i < _tileInfos.Count; ++i)
        {
            ObjectPool _tempPool = new ObjectPool();

            _tileDictionaries[_tileInfos[i].TileType] = _tempPool;

            _tempPool.Initialize(_tileInfos[i].ObjectSize, _tileInfos[i].Prefab);
        }

        CreateRandomTile();

        Debug.Log($"실행 완료\n");
    }

    private void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            ReturnTile();
        }
    }

    private void CreateRandomTile()
    {
        for (int i = 0; i < ConstantValue.READY_TILE_NUMBER; ++i)
        {
            CreateTile(ETileTypes.Pavement);
        }

        do
        {
            ETileTypes _type = SelectTile();

            int _randomTileNumber = UnityEngine.Random.Range(_tileInfos[(int)_type].MinValue, _tileInfos[(int)_type].MaxValue);

            for (int i = 0; i < _randomTileNumber; ++i)
            {
                CreateTile(_type);
            }
        }
        while (_createdTiles.Count <= ConstantValue.MAX_TILE_NUMBER);

        Debug.Log($"실행 완료\n");
    }

    private void CreateTile(ETileTypes type)
    {
        GameObject _obj = _tileDictionaries[type].GiveObject();
        _obj.transform.position = _currPos;

        _createdTiles.Enqueue(_obj);

        _currPos += Vector3.forward * 0.5f;
    }

    private void ReturnTile()
    {
        GameObject _obj = _createdTiles.Dequeue();
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
