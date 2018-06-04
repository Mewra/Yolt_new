using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoteProcedureCall : MonoBehaviour
{
    private Animator anim;
    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    #region PunRPC
    [PunRPC]
    public void Fire(Vector3 pos, Quaternion dir, float speed)
    {
        anim.SetTrigger("shoot");
        GameObject newBullet = Instantiate(Resources.Load("Bullet"), pos, dir) as GameObject;
        newBullet.GetComponent<Rigidbody>().velocity = newBullet.transform.forward * speed;
    }
    #endregion
}
