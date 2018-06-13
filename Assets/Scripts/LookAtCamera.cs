using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    Quaternion quat;

    void Start()
    {
        quat = transform.rotation;
    }

    private void LateUpdate()
    {
        transform.rotation = quat;
    }
}
