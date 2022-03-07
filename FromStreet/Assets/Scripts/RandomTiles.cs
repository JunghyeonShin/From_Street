using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTiles : MonoBehaviour
{
    private enum ETileTypes { Pavement, Road, RailWay, River, }

    private struct TileInfomations
    {
        public GameObject Object;
        public ETileTypes Type;
        public float PosZ;
    }

    private const int PREPARATION_TILE_NUM = 4;
    private const int MAX_TILE_NUM = 48;

    private const string PAVEMENT = "Prefabs/Pavement";
    private const string ROAD = "Prefabs/Road";
    private const string RAILWAY = "Prefabs/RailWay";
    private const string RIVER = "Prefabs/River";

    private TileInfomations[] _tileInfos = new TileInfomations[MAX_TILE_NUM];

    private Dictionary<ETileTypes, Object> _tileIndexs = new Dictionary<ETileTypes, Object>();

    private float[] _ratios = { 35f, 30f, 20f, 15f, };

    private void Start()
    {
        _tileIndexs.Add(ETileTypes.Pavement, Resources.Load(PAVEMENT));
        _tileIndexs.Add(ETileTypes.Road, Resources.Load(ROAD));
        _tileIndexs.Add(ETileTypes.RailWay,  Resources.Load(RAILWAY));
        _tileIndexs.Add(ETileTypes.River, Resources.Load(RIVER));

        for (int i = 0; i < PREPARATION_TILE_NUM; ++i)
        {
            _tileInfos[i].PosZ = i * 0.5f;
            _tileInfos[i].Type = ETileTypes.Pavement;
            _tileInfos[i].Object = (GameObject)Instantiate(_tileIndexs[ETileTypes.Pavement], new Vector3(0f, 0f, _tileInfos[i].PosZ), Quaternion.identity);
        }

        for (int i = PREPARATION_TILE_NUM; i < MAX_TILE_NUM; ++i)
        {
            ETileTypes _tileType = (ETileTypes)CreateWeightedRandomNumber(_ratios);

            _tileInfos[i].PosZ = i * 0.5f;
            _tileInfos[i].Type = _tileType;
            _tileInfos[i].Object = (GameObject)Instantiate(_tileIndexs[_tileType], new Vector3(0f, 0f, _tileInfos[i].PosZ), Quaternion.identity);
        }

        Debug.Log($"실행 완료\n");
    }

    private void Update()
    {

    }

    private float CreateWeightedRandomNumber(float[] datas)
    {
        float _total = 0f;

        for (int i = 0; i < datas.Length; ++i)
        {
            _total += datas[i];
        }

        float _randomPoint = Random.value * _total;

        for (int i = 0; i < datas.Length; ++i)
        {
            if(_randomPoint < datas[i])
            {
                return i;
            }
            else
            {
                _randomPoint -= datas[i];
            }
        }

        return datas.Length - 1f;
    }
}
