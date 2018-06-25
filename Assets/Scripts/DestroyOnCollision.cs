using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnCollision : MonoBehaviour
{   
    public float _damage = 10;


    private void OnCollisionEnter(Collision other) 
    {
        Destroy(this.gameObject);
        if(other.gameObject.tag == "Enemy")
        {
            if(PhotonNetwork.isMasterClient)
            {
                other.gameObject.GetComponent<PhotonView>().RPC("TakeDamage", PhotonTargets.AllViaServer, _damage);
            }
        }
    }
}
