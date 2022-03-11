using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pavement : MonoBehaviour, IObjectPoolMessage
{
    [SerializeField] private Transform _child = null;

    private FixedObjectPositioningMap _fixedObjectPositioningMap = new FixedObjectPositioningMap();

    private List<GameObject> _positioningObject = new List<GameObject>();

    private ObjectPool _treePool = new ObjectPool();

    private void SetFixedObstacle(int pos)
    {
        int temp = 1;

        for (int i = 0; i < ConstantValue.MAX_POSITION_INDEX; ++i)
        {
            if (temp == (pos & temp))
            {
                //_positioningObject.Add();

                float posX = 6f - (i * 2f);

                Vector3 currPos = new Vector3(posX, 0f, 0f);

                _positioningObject[i].transform.position = currPos;
            }

            temp <<= 1;
        }
    }

    public void OnPulled()
    {
        _child.gameObject.SetActive(true);

        _fixedObjectPositioningMap.CreateFixedObstaclePosition();

        SetFixedObstacle(_fixedObjectPositioningMap.CreatablePosition);
    }

    public void OnPushed()
    {
        for (int i = 0; i < _positioningObject.Count; ++i)
        {
            // »èÁ¦
        }

        _child.gameObject.SetActive(false);
    }
}
