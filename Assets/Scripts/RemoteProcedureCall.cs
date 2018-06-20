using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoteProcedureCall : MonoBehaviour
{
    private Animator anim;
    public GameObject parent;
    private PlayerController pc; 
    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        pc = parent.GetComponent<PlayerController>();
    }

    #region PunRPC
    [PunRPC]
    public void Fire(Vector3 pos, Quaternion dir, float speed)
    {
        anim.SetTrigger("shoot");
        GameObject newBullet = Instantiate(Resources.Load("Bullet"), pos, dir) as GameObject;
        newBullet.GetComponent<Rigidbody>().velocity = newBullet.transform.forward * speed;
    }

    [PunRPC]
    public void DecreaseLùth(float i)
    {
        if (pc.state == PlayerController.CLASSES.assassin || pc.state == PlayerController.CLASSES.support || pc.state == PlayerController.CLASSES.tank)
        {
            pc._lùth -= i;

            if (pc._lùth < 0)
            {
                pc._lùth = 0;

                pc.lùthfinito = true;
            }
        }
    }

    [PunRPC]
    public void DestroyObject(int ID)
    {
        PhotonView view = PhotonView.Find(ID);
        if (view != null)
        {
            PhotonNetwork.Destroy(view.gameObject);
        }
    }
    #endregion
}
