using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laserone : MonoBehaviour
{
    //Change How much damage to do from the inspector
    public float damage;
    private ParticleSystem _psQ;

    // Use this for initialization
    void Start()
    {
        //damage = 10f;
        _psQ = GetComponentInChildren<ParticleSystem>();
        _psQ.Clear();
        _psQ.Simulate(0.0f, true, true);
        _psQ.Play();
        StartCoroutine("Duration");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("stasera tutti a casa di Giacomo");
            other.gameObject.GetComponent<PhotonView>().RPC("TakeDamage", PhotonTargets.AllViaServer, damage);
        }
    }

    public IEnumerator Duration()
    {

        yield return new WaitForSeconds(0.5f);
        if (PhotonNetwork.isMasterClient)
        {
            Debug.Log("Distruggo R");
            PhotonNetwork.Destroy(gameObject);
        }

    }


}
