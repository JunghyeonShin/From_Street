using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedObstacleSpawn : MonoBehaviour
{
    [SerializeField] private GameObject _fixedObstacle = null;
    [SerializeField] private int _fixedObstacleSize = 0;

    private ObjectPool _fixedObstaclePool = new ObjectPool();
    
    private float _posZ = 6f;

    private void Start()
    {
        _fixedObstaclePool.InitializeObjectPool(_fixedObstacleSize, _fixedObstacle);
    }

    public void SetFixedObstacle(int pos)
    {
        if (0 != pos)
        {
            int temp = 1;

            for (int i = 0; i < ConstantValue.MAX_POSITION_INDEX; ++i)
            {
                if (temp == (pos & temp))
                {
                    GameObject obj = _fixedObstaclePool.GiveObject();

                    float posX = 6f - (i * 2f);

                    Vector3 positionVec = new Vector3(posX, 0f, _posZ);

                    obj.transform.position += positionVec;
                }

                temp <<= 1;
            }
        }

        _posZ += ConstantValue.TILE_SIZE;
    }
}
