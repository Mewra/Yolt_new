using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMerchant : MonoBehaviour
{
    public GameObject prefab;
    public Transform[] spawnPoints;
    GameObject go;

    void Start()
    {

    }

    public void Spawn()
    {
        if (PhotonNetwork.isMasterClient)
        {
            go = PhotonNetwork.InstantiateSceneObject(prefab.name, spawnPoints[0].position, Quaternion.identity, 0, null);
            
        }
    }

    public void Despawn() {

        PhotonNetwork.Destroy(go);

    }
}
