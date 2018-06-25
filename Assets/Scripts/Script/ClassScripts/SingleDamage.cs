using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleDamage : MonoBehaviour {

    //Change How much damage to do from the inspector
    public int damage;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy") {
            other.gameObject.GetComponent<PhotonView>().RPC("TakeDamage", PhotonTargets.AllViaServer, damage);
            //other.GetComponent<Health>().TakeDamage(damage);
        }
    }
}
