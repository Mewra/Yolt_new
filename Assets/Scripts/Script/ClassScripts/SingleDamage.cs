using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleDamage : MonoBehaviour
{
    //Change How much damage to do from the inspector
    public float damage;

	// Use this for initialization
	void Start ()
    {
        damage = 50f;	
	}

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.tag);
        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("stasera tutti a casa di Giacomo");
            other.gameObject.GetComponent<PhotonView>().RPC("TakeDamage", PhotonTargets.AllViaServer, damage);
            //other.GetComponent<Health>().TakeDamage(damage);
        }
    }
}
