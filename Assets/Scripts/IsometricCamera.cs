using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsometricCamera : MonoBehaviour
{

    private Transform playerTransform;
    public float distance = 10f;
    public float height = 8f;


    private void Update ()
    {
        if(playerTransform != null)
        {
            transform.position = playerTransform.position + new Vector3(-distance, height, -distance);
        }
	}

    public void SetTarget(Transform target)
    {
        playerTransform = target;
    }
}
