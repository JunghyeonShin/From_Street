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

    private TileInfomations[] _tileInfos = new TileInfomations[48];
    private int[] _randomTileNum = new int[48];

    private float[] _ratios = { 35f, 30f, 20f, 15f, };

    private void Start()
    {
        for (int i = 0; i < 4; ++i)
        {
            _randomTileNum[i] = 0;
            _tileInfos[i].PosZ = i * 0.5f;
            _tileInfos[i].Type = ETileTypes.Pavement;
            _tileInfos[i].Object = (GameObject)Instantiate(Resources.Load("Prefabs/Pavement"), new Vector3(0f, 0f, _tileInfos[i].PosZ), Quaternion.identity);
        }

        for (int i = 4; i < 48; ++i)
        {
            _randomTileNum[i] = (int)CreateWeightedRandomNumber(_ratios);

            switch (_randomTileNum[i])
            {
                case 0:
                    _tileInfos[i].PosZ = i * 0.5f;
                    _tileInfos[i].Type = ETileTypes.Pavement;
                    _tileInfos[i].Object = (GameObject)Instantiate(Resources.Load("Prefabs/Pavement"), new Vector3(0f, 0f, _tileInfos[i].PosZ), Quaternion.identity);
                    break;
                case 1:
                    _tileInfos[i].PosZ = i * 0.5f;
                    _tileInfos[i].Type = ETileTypes.Road;
                    _tileInfos[i].Object = (GameObject)Instantiate(Resources.Load("Prefabs/Road"), new Vector3(0f, 0f, _tileInfos[i].PosZ), Quaternion.identity);
                    break;
                case 2:
                    _tileInfos[i].PosZ = i * 0.5f;
                    _tileInfos[i].Type = ETileTypes.RailWay;
                    _tileInfos[i].Object = (GameObject)Instantiate(Resources.Load("Prefabs/RailWay"), new Vector3(0f, 0f, _tileInfos[i].PosZ), Quaternion.identity);
                    break;
                case 3:
                    _tileInfos[i].PosZ = i * 0.5f;
                    _tileInfos[i].Type = ETileTypes.River;
                    _tileInfos[i].Object = (GameObject)Instantiate(Resources.Load("Prefabs/River"), new Vector3(0f, 0f, _tileInfos[i].PosZ), Quaternion.identity);
                    break;
            }
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
