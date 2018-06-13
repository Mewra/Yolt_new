using System.Collections;
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
                // TODO : find all the components in the other childs
                go.GetComponentInChildren<MovementGhost>().enabled = true;
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
