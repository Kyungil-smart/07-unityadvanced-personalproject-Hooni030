using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance { get; private set; }
    Dictionary<GameObject, Queue<GameObject>> _pools = new Dictionary<GameObject, Queue<GameObject>>();



    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        // 객체 사전 생성

    }

    // 객체 사전 생성
    private void Init()
    {

    }

    private void CreateObj()
    {
        
    }

    public GameObject Get(GameObject prefab, Vector3 pos, Quaternion rot)
    {
        if (!_pools.ContainsKey(prefab))
        {
            _pools.Add(prefab, new Queue<GameObject>());
        }

        GameObject obj;

        if (_pools[prefab].Count > 0)
        {
            obj = _pools[prefab].Dequeue();
        }
        else
        {
            obj = Instantiate(prefab, pos, rot);
            obj.name = prefab.name;
        }
        
        obj.SetActive(true);

        if (obj.TryGetComponent(out IPoolable poolable))
        {
            poolable.OnSpawn();
        }
        return obj;
    }

    public void Release(GameObject prefab, GameObject obj)
    {
        if (!_pools.ContainsKey(prefab))
        {
            _pools.Add(prefab, new Queue<GameObject>());
        }

        if (obj.TryGetComponent(out IPoolable poolable))
        {
            poolable.OnDespawn();
        }
        
        obj.SetActive(false);
        _pools[prefab].Enqueue(obj);
    }
}
