using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public enum PoolExceededMode : int
{
    Ignore = 0,
    StopSpawning = 1,
    ReUse = 2
}

public class PoolSystem : MonoBehaviour
{
    static Dictionary<string, PoolSystem> myPools = new Dictionary<string, PoolSystem>();
    public string poolName;
    public bool dontDestroy = false;
    public bool prepareAtStart = true;
    public bool autoCull = true;
    public int allocationBlockSize = 1;
    public int minPoolSize = 1;
    public int maxPoolSize = 1;
    public int InStock { get { return poolStock.Count; } }
    public int Spawned { get { return poolSpawned.Count; } }
    public float cullingSpeed = 1.0f;
    float poolLastCullingTime;
    Stack<GameObject> poolStock = new Stack<GameObject>();
    List<GameObject> poolSpawned = new List<GameObject>();
    public PoolExceededMode OnMaxPoolSize = PoolExceededMode.Ignore;
    public GameObject prefab;

    #region Unity.MonoBehaviour Callbacks
    private void Awake()
    {
        if (poolName.Length == 0)
        {
            Debug.LogWarning("PoolSystem: Missing PoolName for pool belonging to '" + gameObject.name + "'!");
        }
        if (dontDestroy)
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void OnEnable()
    {
        if (prefab != null)
        {
            if (GetPoolByName(poolName) == null)
            {
                myPools.Add(poolName, this);
            }
        }
        else
        {
            Debug.LogError("PoolSystem: Pool '" + poolName + "' is missing it's prefab!");
        }
    }

    private void Start()
    {
        if (prepareAtStart)
        {
            Prepare();
        }
    }

    private void LateUpdate()
    {
        if (autoCull && Time.time - poolLastCullingTime > cullingSpeed)
        {
            poolLastCullingTime = Time.time;
            Cull(true);
        }
    }

    private void OnDisable()
    {
        if (!dontDestroy)
        {
            Clear();
            if (myPools.Remove(poolName))
            {
                Debug.Log("PoolSystem: Removing " + poolName + " from the pool dictionary!");
            }
        }

    }

    void Reset()
    {
        poolName = "";
        prefab = null;
        dontDestroy = false;
        allocationBlockSize = 1;
        minPoolSize = 1;
        maxPoolSize = 1;
        OnMaxPoolSize = PoolExceededMode.Ignore;
        autoCull = true;
        cullingSpeed = 1f;
        poolLastCullingTime = 0;
    }
    #endregion

    #region User Methods
    void Clear()
    {
        foreach (GameObject go in poolSpawned)
        {
            Destroy(go);
        }
        poolSpawned.Clear();
        foreach (GameObject go in poolStock)
        {
            Destroy(go);
        }
        poolStock.Clear();
    }

    public void Cull()
    {
        Cull(false);
    }

    public void Cull(bool smartCull)
    {
        int toCull = (smartCull) ? Mathf.Min(allocationBlockSize, poolStock.Count - maxPoolSize) : poolStock.Count - maxPoolSize;
        while (toCull-- > 0)
        {
            GameObject item = poolStock.Pop();
            Destroy(item);
        }
    }

    public void DespawnItem(GameObject item)
    {
        if (!item)
        {
            return;
        }
        if (IsSpawned(item))
        {
            item.SetActive(false);
            item.name = prefab.name + "_stock";
            poolSpawned.Remove(item);
            poolStock.Push(item);
        }
        else
        {
            GameObject.Destroy(item);
        }
    }

    public void DespawnAllItems()
    {
        while (poolSpawned.Count > 0)
        {
            DespawnItem(poolSpawned[0]);
        }
    }

    public void KillItem(GameObject item)
    {
        if (!item)
        {
            return;
        }
        poolSpawned.Remove(item);
        Destroy(item);
    }

    public bool IsManagedObject(GameObject item)
    {
        if (!item)
        {
            return false;
        }
        if (poolSpawned.Contains(item) || poolStock.Contains(item))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool IsSpawned(GameObject item)
    {
        if (!item)
        {
            return false;
        }
        return (poolSpawned.Contains(item));
    }

    void Populate(int n)
    {
        while (n > 0)
        {
            GameObject go = (GameObject)Instantiate(prefab);
            go.SetActive(false);
            go.transform.parent = transform;
            go.name = prefab.name + "_stock";
            poolStock.Push(go);
            n--;
        }
    }

    public void Prepare()
    {
        Clear();
        poolStock = new Stack<GameObject>(minPoolSize);
        Populate(minPoolSize);
    }

    public GameObject SpawnItem()
    {
        GameObject item = null;
        if (InStock == 0)
        {
            if (Spawned < maxPoolSize || OnMaxPoolSize == PoolExceededMode.Ignore)
            {
                Populate(allocationBlockSize);
            }
        }
        if (InStock > 0)
        {
            item = poolStock.Pop();
        }
        else if (OnMaxPoolSize == PoolExceededMode.ReUse)
        {
            item = poolSpawned[0];
            poolSpawned.RemoveAt(0);
        }
        if (item != null)
        {
            poolSpawned.Add(item);
            item.SetActive(true);
            item.name = prefab.name + "_clone";
            item.transform.localPosition = Vector3.zero;
        }
        return item;
    }
    #endregion

    #region Static User Methods
    public static GameObject Spawn(string poolName)
    {
        PoolSystem P;
        if (myPools.TryGetValue(poolName, out P))
        {
            return P.SpawnItem();
        }
        else
        {
            return null;
        }
    }

    public static void Despawn(GameObject item)
    {
        if (item)
        {
            PoolSystem P = GetPoolByItem(item);
            if (P != null)
            {
                P.Cull();
            }
            else
            {
                GameObject.Destroy(item);
            }
        }
    }

    public static void DespawnAllItems(string poolName)
    {
        PoolSystem P = GetPoolByName(poolName);
        if (P != null)
        {
            P.DespawnAllItems();
        }
    }

    public static PoolSystem GetPoolByItem(GameObject item)
    {
        foreach (PoolSystem P in myPools.Values)
        {
            if (P.IsManagedObject(item))
            {
                return P;
            }
        }
        return null;
    }

    public static PoolSystem GetPoolByName(string poolName)
    {
        PoolSystem P;
        myPools.TryGetValue(poolName, out P);
        return P;
    }

    public static void Kill(GameObject item)
    {
        if (item)
        {
            PoolSystem P = GetPoolByItem(item);
            if (P != null)
            {
                P.KillItem(item);
            }
            else
            {
                GameObject.Destroy(item);
            }
        }
    }

    public static void Prepare(string poolName)
    {
        PoolSystem P = GetPoolByName(poolName);
        if (P != null)
        {
            P.Prepare();
        }
    }

    public static void Cull(string poolName)
    {
        Cull(poolName, false);
    }

    public static void Cull(string poolName, bool smartCull)
    {
        PoolSystem P = GetPoolByName(poolName);
        if (P)
        {
            P.Cull();
        }
    }
    #endregion
}