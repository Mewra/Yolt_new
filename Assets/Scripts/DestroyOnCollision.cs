using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnCollision : MonoBehaviour
{   
    public float _damage = 10;

    private void Start()
    {
        
    }

    private void OnCollisionEnter(Collision other) //cambiato da collision a collider
    {   
        Destroy(this.gameObject);
    }

    private void OnColliderEnter(Collider other) {
        if (other.gameObject.tag == "Enemy")
        {
            other.GetComponent<Health>().TakeDamage(_damage);
        }
    }
}
