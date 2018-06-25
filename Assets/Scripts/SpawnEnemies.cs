using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    public GameObject prefabVeloce;
    public GameObject prefabLento;
    public Transform[] spawnPoints;
   

	void Start ()
    {
       
	}

    public void Spawn()
    {

        if (PhotonNetwork.isMasterClient)
        {
            for (int i = 0; i < GameManager.Instance.numberOfEnemiesToSpawn / 2; i++)
            {
                GameObject go = PhotonNetwork.InstantiateSceneObject(prefabVeloce.name, spawnPoints[Random.Range(0, 4)].position, Quaternion.identity, 0, null);
                
            }
            for (int i = 0; i < GameManager.Instance.numberOfEnemiesToSpawn / 2; i++)
            {
                GameObject go = PhotonNetwork.InstantiateSceneObject(prefabLento.name, spawnPoints[Random.Range(0, 4)].position, Quaternion.identity, 0, null);
                
            }

        }
    }
}
