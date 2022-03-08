using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTiles : MonoBehaviour
{
    private Dictionary<ETileTypes, Object> _tileIndexs = new Dictionary<ETileTypes, Object>();

    private ObjectPool[] _tileObjects = new ObjectPool[ConstantValue.PREPARATION_TILE_NUM];

    private void Start()
    {
        _tileIndexs.Add(ETileTypes.Pavement, Resources.Load(ConstantValue.PAVEMENT));
        _tileIndexs.Add(ETileTypes.Road, Resources.Load(ConstantValue.ROAD));
        _tileIndexs.Add(ETileTypes.RailWay, Resources.Load(ConstantValue.RAILWAY));
        _tileIndexs.Add(ETileTypes.River, Resources.Load(ConstantValue.RIVER));

        for (int i = 0; i < _tileObjects.Length; ++i)
        {
            _tileObjects[i] = new ObjectPool();
            _tileObjects[i].Initialize(ConstantValue.CREATE_TILE_NUM, _tileIndexs[(ETileTypes)i]);
        }

        Debug.Log($"실행 완료\n");
    }

    private void Update()
    {

    }
}
