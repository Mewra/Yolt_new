using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<PhotonView>().ObservedComponents.Add(GetComponentInChildren<PhotonAnimatorView>());
    }
}
