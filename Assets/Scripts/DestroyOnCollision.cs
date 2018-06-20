using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnCollision : MonoBehaviour
{   
    public float _damage = 50;


    private void OnTriggerEnter(Collider other) 
    {
        Destroy(gameObject);
        if (other.gameObject.CompareTag("Enemy"))
        {
            if(PhotonNetwork.isMasterClient && other.gameObject.GetComponent<PhotonView>() != null)
            {
                other.gameObject.GetComponent<PhotonView>().RPC("TakeDamage", PhotonTargets.AllViaServer, _damage);
            }
        }
    }
}
