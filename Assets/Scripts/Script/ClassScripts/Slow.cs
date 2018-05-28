using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slow : MonoBehaviour {

    public float _damage;
    public float _slow;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy") {
            other.GetComponent<Health>().TakeDamage(_damage);
            other.gameObject.GetComponent<EnemyManager>().Slow(_slow);
        }
    }
}
