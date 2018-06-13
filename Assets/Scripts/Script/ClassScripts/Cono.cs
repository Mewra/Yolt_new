using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cono : MonoBehaviour
{

    float t = 0;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerStay(Collider coll)
    {
        //Debug.Log("t: " + t);
        t += Time.deltaTime;

        if (coll.gameObject.tag == "Enemy")
        {
            //Debug.Log("Sono dentro enemy");
            if (t > 0.5)
            {
                coll.gameObject.GetComponent<PhotonView>().RPC("TakeDamage", PhotonTargets.AllViaServer, 2.0f);
                //coll.gameObject.GetComponent<Health>().TakeDamage(2);
                //Debug.Log("Sto facendo 2 danni");
                t = 0;
            }
        }

    }
}
