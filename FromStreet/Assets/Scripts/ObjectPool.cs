using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class ObjectPool
{
    private Queue<GameObject> _pooledObjects = new Queue<GameObject>();

    private GameObject _objectPrefab = null;

    public void InitializeObjectPool(int cnt, GameObject obj)
    {
        _objectPrefab = obj;

        for (int i = 0; i < cnt; ++i)
        {
            _pooledObjects.Enqueue(CreateNewObject());
        }
    }

    public GameObject GiveObject()
    {
        if (_pooledObjects.Count > ConstantValue.EMPTY)
        {
            GameObject pooledObject = _pooledObjects.Dequeue();
            pooledObject.SetActive(true);

            return pooledObject;
        }
        else
        {
            GameObject newObject = CreateNewObject();
            newObject.SetActive(true);

            return newObject;
        }
    }

    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);

        _pooledObjects.Enqueue(obj);
    }

    private GameObject CreateNewObject()
    {
        GameObject newObject = GameObject.Instantiate(_objectPrefab);
        newObject.SetActive(false);

        return newObject;
    }
}
