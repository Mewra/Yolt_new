using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunControler : MonoBehaviour
{
    public float bulletSpeed;
    public float timeBetweenShots;
    private float shotCounter;
    private PhotonView myView;
    private Animator anim;
    public Transform firepoint;

    #region Unity.MonoBehaviour Callbacks
    private void Start()
    {
        anim = GetComponent<Animator>();
        myView = GetComponent<PhotonView>();
    }

    private void Update()
    {
        shotCounter -= Time.deltaTime;
        if (Input.GetMouseButton(0) && myView.isMine)
        {
            if (shotCounter <= 0f)
            {
                Debug.Log(shotCounter);
                shotCounter = timeBetweenShots;
                // myView.RPC("Fire", PhotonTargets.AllViaServer, firepoint.position, transform.rotation);
            }
        }
    }
    #endregion

    #region PunRPC
    [PunRPC]
    public void Fire(Vector3 pos, Quaternion dir)
    {
        anim.SetTrigger("shoot");
        GameObject newBullet = Instantiate(Resources.Load("Bullet"), pos, dir) as GameObject;
        newBullet.GetComponent<Rigidbody>().velocity = newBullet.transform.forward * bulletSpeed;
    }
    #endregion
}
