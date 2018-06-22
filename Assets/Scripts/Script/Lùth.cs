using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lùth : MonoBehaviour
{

    private float _lùth;
    private PhotonView myPV;

    private void Start()
    {
        myPV = GetComponent<PhotonView>();
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Ghost")
        {
            _lùth = Random.Range(80, 90);
            coll.gameObject.GetComponentInParent<PhotonView>().RPC("IncreaseLùth", PhotonTargets.AllViaServer, _lùth);
            //coll.gameObject.GetComponentInParent<PlayerController>().IncreaseLùth(_lùth);
            coll.GetComponentInParent<PhotonView>().RPC("DestroyObject", PhotonTargets.MasterClient, myPV.viewID);
            
        }
    }
}
