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
            //pc.imgluth.fillAmount = pc._lùth / 100;

            if (pc._lùth <= 0)
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

    /*[PunRPC]
    public void StartParticleSystem(int classes, int spell)
    {
        switch(classes)
        {
            case 0:
                switch(spell)
                {
                    case 0:
                        Debug.Log("sono qua");
                        gameObject.GetComponentInChildren<Assassin>().QAbility();
                        break;
                }
                break;

            case 1:
                gameObject.GetComponentInChildren<Tank>().Invoke(name, 0f);
                break;

            case 2:
                gameObject.GetComponentInChildren<Support>().Invoke(name, 0f);
                break;
        }
    }*/
    #endregion

    [PunRPC]
    public void InstanceQRAS(string prefab, Vector3 pos, Quaternion rot) {

        //Debug.Log("Instanzio Q");
        GameObject go = Instantiate(Resources.Load(prefab), pos, rot) as GameObject;
    }

    [PunRPC]
    public void InstanceWAS(string prefab, Vector3 pos, Quaternion rot)
    {
        //Debug.Log("Instanzio Q");
        GameObject go = Instantiate(Resources.Load(prefab), pos, rot) as GameObject;
        go.transform.parent = gameObject.GetComponentInChildren<Assassin>()._cono.transform;
    }






}
