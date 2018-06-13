using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
    private PhotonView myPV;
    public GameObject[] child;

    private void Start()
    {
        myPV = GetComponent<PhotonView>();
        myPV.ObservedComponents.Add(child[0].GetComponent<PhotonAnimatorView>());
    }

    [PunRPC]
    public void RestoreAnimatorView(int index)
    {
        myPV.ObservedComponents.RemoveAt(1);
        myPV.ObservedComponents.Add(child[index].GetComponent<PhotonAnimatorView>());
    }
}
