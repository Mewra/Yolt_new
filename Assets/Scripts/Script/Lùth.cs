﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lùth : MonoBehaviour {

    public float _lùth; 

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider coll)
    {

        if (coll.gameObject.tag == "Ghost")
        {   

            ////////////////RPC/////////////////
            coll.gameObject.GetComponentInParent<PlayerController>().IncreaseLùth(_lùth);
            // coll.GetComponentInParent<PhotonView>().RPC("IncreaseLùth", )

            Destroy(gameObject);
        }
    }
}
