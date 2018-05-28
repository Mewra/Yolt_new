using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Redemption : MonoBehaviour {

    public float _heal;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") {
            other.GetComponent<Health>().AreaHeal(_heal);
        }
    }
}
