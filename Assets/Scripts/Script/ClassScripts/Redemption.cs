using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Redemption : MonoBehaviour {

    public float _heal;
    private ParticleSystem _psQ;

    // Use this for initialization
    void Start () {
        _psQ = GetComponentInChildren<ParticleSystem>();
        _psQ.Clear();
        _psQ.Simulate(0.0f, true, true);
        _psQ.Play();
        StartCoroutine("Duration");
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") {

            other.gameObject.GetComponentInParent<PhotonView>().RPC("AreaHeal", PhotonTargets.AllViaServer, _heal);

            //other.GetComponent<Health>().AreaHeal(_heal);
        }
    }

    public IEnumerator Duration()
    {

        yield return new WaitForSeconds(0.5f);
        if (PhotonNetwork.isMasterClient)
        {
            PhotonNetwork.Destroy(gameObject);
        }

    }
}
