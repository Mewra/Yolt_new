using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System;

public class PoolSystemPun : MonoBehaviour, IPunPrefabPool
{
    public void Start() // not sure for public access
    {
        PhotonNetwork.PrefabPool = this;
    }
    
    public GameObject Instantiate(string prefabId, Vector3 position, Quaternion rotation)
    {
        GameObject go = PoolSystem.Spawn(prefabId);
        go.transform.position = position;
        go.transform.rotation = rotation;
        return go;
    }

    public void Destroy(GameObject gameObject)
    {
        PoolSystem.Despawn(gameObject);
    }

    public GameObject InstantiateSceneObject(string prefabId, Vector3 position, Quaternion rotation)
    {
        GameObject go = PoolSystem.Spawn(prefabId);
        go.transform.position = position;
        go.transform.rotation = rotation;
        return go;
    }
}
