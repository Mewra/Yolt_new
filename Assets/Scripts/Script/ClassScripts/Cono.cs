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
        t += Time.deltaTime;
        if (coll.gameObject.tag == "Enemy")
        {
            if (t > 0.5)
            {
                coll.gameObject.GetComponent<PhotonView>().RPC("TakeDamage", PhotonTargets.AllViaServer, 2.0f);
                t = 0;
            }
        }

    }
}
