using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunControler : MonoBehaviour
{
    public float bulletSpeed;
    public float timeBetweenShots;
    private float shotCounter;
    private PhotonView myView;
    private RotatePlayer rp;
    public Transform firepoint;

    private void Start()
    {
        myView = GetComponent<PhotonView>();
        rp = GetComponent<RotatePlayer>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && myView.isMine)
        {
            shotCounter -= Time.deltaTime;
            if (shotCounter <= 0f)
            {
                shotCounter = timeBetweenShots;
                Vector3 lookAkRotation = new Vector3(rp.PointToLook.x, firepoint.position.y, rp.PointToLook.z);
                myView.RPC("Fire", PhotonTargets.AllViaServer, firepoint.position, Quaternion.LookRotation(lookAkRotation, Vector3.forward));
            }
        }
        else
        {
            shotCounter = 0f;
        }
    }

    [PunRPC]
    public void Fire(Vector3 pos, Quaternion dir)
    {
        GameObject newBullet = Instantiate(Resources.Load("Bullet"), pos, dir) as GameObject;
        // newBullet.transform.Rotate(dir.eulerAngles, Space.World);
        newBullet.GetComponent<Rigidbody>().velocity = newBullet.transform.forward * 12f;
    }
}
