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
            Quaternion quat = new Quaternion(0, -90, -90, 1);
            go = PhotonNetwork.InstantiateSceneObject(prefab.name, spawnPoints[0].position, Quaternion.identity, 0, null);
            //go.transform.rotation = quat;
            go.transform.Rotate(new Vector3(-90, -90, -180));
        }
    }

    public void Despawn() {

        PhotonNetwork.Destroy(go);

    }
}
