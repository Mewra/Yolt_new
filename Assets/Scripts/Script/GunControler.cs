using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunControler : MonoBehaviour
{
    public float bulletSpeed;
    public float timeBetweenShots;
    private float shotCounter;
    public PhotonView myView;
    private Animator anim;
    public Transform firepoint;

    #region Unity.MonoBehaviour Callbacks
    private void Start()
    {
        anim = GetComponent<Animator>();
        myView = GetComponentInParent<PhotonView>();
    }

    private void Update()
    {
        shotCounter -= Time.deltaTime;
        if (Input.GetMouseButton(0) && myView.isMine)
        {
            if (shotCounter <= 0f)
            {
                shotCounter = timeBetweenShots;
                myView.RPC("Fire", PhotonTargets.AllViaServer, firepoint.position, transform.rotation, bulletSpeed);
            }
        }
    }
    #endregion
}
