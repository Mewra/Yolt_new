using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slow : MonoBehaviour {

    public float _damage;

	// Use this for initialization
	void Start () {
        StartCoroutine("Duration");

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy") {
            //si trova in health
            other.gameObject.GetComponent<PhotonView>().RPC("TakeDamage", PhotonTargets.AllViaServer, _damage);
            //si trova in enemy controller
            other.gameObject.GetComponent<PhotonView>().RPC("Slow", PhotonTargets.AllViaServer);
            
        }
    }

    public IEnumerator Duration() {

        yield return new WaitForSeconds(5.0f);
        if (PhotonNetwork.isMasterClient) {
            PhotonNetwork.Destroy(gameObject);
        }

    }
}
