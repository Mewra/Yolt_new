using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lùth : MonoBehaviour
{

    public float _lùth;
    private PhotonView myPV;

    private void Start()
    {
        myPV = GetComponent<PhotonView>();
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Ghost")
        {
            _lùth = Random.Range(10, 21);
            coll.gameObject.GetComponentInParent<PlayerController>().IncreaseLùth(_lùth);
            // build an rpc here to destroy the object
            coll.GetComponentInParent<PhotonView>().RPC("DestroyObject", PhotonTargets.MasterClient, myPV.viewID);
            // Destroy(gameObject);
        }
    }
}
