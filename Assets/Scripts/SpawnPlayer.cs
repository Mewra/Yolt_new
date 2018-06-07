using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    public GameObject prefab;

	void Start ()
    {
        GameObject go = PhotonNetwork.Instantiate(prefab.name, Vector3.zero, Quaternion.identity, 0);
        if(go.GetComponent<PhotonView>().isMine)
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
