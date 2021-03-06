﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    public GameObject prefab;
    public Transform[] spawnTransform;

	void Start ()
    {
        List<PhotonPlayer> listOfPlayer = new List<PhotonPlayer>(PhotonNetwork.playerList);
        listOfPlayer.Sort(SortByID);
        int i = 0;
        foreach(PhotonPlayer pp in listOfPlayer)
        {
            
            if(pp.NickName.Equals(PhotonNetwork.playerName))
            {
                break;
            }
            i++;
        }
        GameObject go = PhotonNetwork.Instantiate(prefab.name, spawnTransform[i].position, Quaternion.identity, 0);
        if (go.GetComponent<PhotonView>().isMine)
        {
            go.GetComponentInChildren<MovementGhost>().enabled = true;
            // TODO : find all the components in the other childs
            for (int j = 1; j < go.transform.childCount; j++)
            {
                if(!go.transform.GetChild(j).gameObject.GetActive())
                {
                    go.transform.GetChild(j).gameObject.SetActive(true);
                    if(go.transform.GetChild(j).gameObject.GetComponent<MovementGhost>() != null
                       && go.transform.GetChild(j).gameObject.GetComponent<PlayerAnimation>() != null
                       && go.transform.GetChild(j).gameObject.GetComponent<RotatePlayer>() != null)
                    {
                        go.transform.GetChild(j).gameObject.GetComponent<RotatePlayer>().enabled = true;
                        go.transform.GetChild(j).gameObject.GetComponent<PlayerAnimation>().enabled = true;
                        go.transform.GetChild(j).gameObject.GetComponent<MovementGhost>().enabled = true;
                    }
                    go.transform.GetChild(j).gameObject.SetActive(false);
                }
            }
           go.GetComponentInChildren<RotatePlayer>().enabled = true;
           go.GetComponentInChildren<GunControler>().enabled = true;
           go.GetComponentInChildren<PlayerAnimation>().enabled = true;
           Camera.main.GetComponent<IsometricCamera>().SetTarget(go.transform);
        }
        /*
        foreach (Transform t in spawnTransform)
        {
            if(!t.gameObject.GetComponent<DummyScript>().isUsed)
            {
                t.gameObject.GetComponent<DummyScript>().isUsed = true;
                GameObject go = PhotonNetwork.Instantiate(prefab.name, Vector3.zero, Quaternion.identity, 0);
                if (go.GetComponent<PhotonView>().isMine)
                {
                    // TODO : find all the components in the other childs
                    go.GetComponentInChildren<MovementGhost>().enabled = true;
                    go.GetComponentInChildren<RotatePlayer>().enabled = true;
                    go.GetComponentInChildren<GunControler>().enabled = true;
                    go.GetComponentInChildren<PlayerAnimation>().enabled = true;
                    Camera.main.GetComponent<IsometricCamera>().SetTarget(go.transform);
                }
            }
        }
        */

    }

    public static int SortByID(PhotonPlayer a, PhotonPlayer b)
    {
        return a.ID.CompareTo(b.ID);
    }
}
