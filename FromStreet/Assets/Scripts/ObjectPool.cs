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

    public GameObject GiveObject(float currPosZ)
    {
        GameObject pulledObject;

        if (_pooledObjects.Count > ConstantValue.EMPTY)
        {
            pulledObject = _pooledObjects.Dequeue();
        }
        else
        {
            pulledObject = CreateNewObject();
        }

        pulledObject.SetActive(true);

        pulledObject.GetComponent<IObjectPoolMessage>()?.OnPulled(currPosZ);

        return pulledObject;
    }

    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);

        obj.GetComponent<IObjectPoolMessage>()?.OnPushed();

        _pooledObjects.Enqueue(obj);
    }

    private GameObject CreateNewObject()
    {
        GameObject newObject = GameObject.Instantiate(_objectPrefab);
        newObject.SetActive(false);

        return newObject;
    }
}
