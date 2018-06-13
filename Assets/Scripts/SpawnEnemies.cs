using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    public GameObject prefab;
    public Transform[] spawnPoints;

	void Start ()
    {

	}

    public void Spawn(int n)
    {
        if (PhotonNetwork.isMasterClient)
        {
            for (int i = 0; i < n; i++)
            {
                GameObject go = PhotonNetwork.InstantiateSceneObject(prefab.name, spawnPoints[Random.Range(0, 3)].position, Quaternion.identity, 0, null);
            }
        }
    }
}
