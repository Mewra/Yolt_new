using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cono : MonoBehaviour
{

    float t = 0;
    private ParticleSystem _psQ;

    // Use this for initialization
    void Start()
    {
        _psQ = GetComponentInChildren<ParticleSystem>();
        Debug.Log(_psQ.name);
        _psQ.Clear();
        _psQ.Simulate(0.0f, true, true);
        _psQ.Play();
        StartCoroutine("Duration");
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerStay(Collider coll)
    {
        t += Time.deltaTime;
        if (coll.gameObject.tag == "Enemy")
        {
            if (t > 0.5)
            {
                coll.gameObject.GetComponent<PhotonView>().RPC("TakeDamage", PhotonTargets.AllViaServer, 5.0f);
                t = 0;
            }
        }

    }

    public IEnumerator Duration()
    {

        yield return new WaitForSeconds(5.0f);
        if (PhotonNetwork.isMasterClient)
        {
            Debug.Log("Distruggo W");
            _psQ.Clear();
            _psQ.Simulate(0.0f, true, true);
            _psQ.Stop();
            PhotonNetwork.Destroy(gameObject);
        }

    }


}
