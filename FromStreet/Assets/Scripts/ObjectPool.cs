using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class ObjectPool
{
    private Object _objectPrefab = null;

    private int _totalCount = 0;

    private Queue<GameObject> _pooledObjects = new Queue<GameObject>();

    public void Initialize(int cnt, Object obj)
    {
        _totalCount = cnt;
        _objectPrefab = obj;

        for (int i = 0; i < cnt; ++i)
        {
            _pooledObjects.Enqueue(CreateNewObject());
        }
    }

    public GameObject GiveObject()
    {
        if (_pooledObjects.Count > 0)
        {
            GameObject _object = _pooledObjects.Dequeue();
            _object.SetActive(true);

            return _object;
        }
        else
        {
            GameObject _newObject = CreateNewObject();
            _newObject.SetActive(true);

            return _newObject;
        }
    }

    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
        _pooledObjects.Enqueue(CreateNewObject());
    }

    private GameObject CreateNewObject()
    {
        GameObject _newObject = GameObject.Instantiate((GameObject)_objectPrefab);
        _newObject.SetActive(false);

        return _newObject;
    }
}
