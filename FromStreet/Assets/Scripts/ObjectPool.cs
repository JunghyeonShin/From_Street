using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class ObjectPool
{
    private GameObject _objectPrefab = null;

    private int _totalCount = 0;

    private Queue<GameObject> _pooledObjects = new Queue<GameObject>();

    public void Initialize(int cnt, GameObject obj)
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
    }

    private GameObject CreateNewObject()
    {
        GameObject _newObject = GameObject.Instantiate(_objectPrefab);
        _newObject.SetActive(false);

        return _newObject;
    }
}
