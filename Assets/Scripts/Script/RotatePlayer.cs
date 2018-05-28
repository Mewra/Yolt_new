﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePlayer : MonoBehaviour
{
    private Ray cameraRay;
    private Camera myCamera;
    public float rayLength;
    public Vector3 PointToLook { get; set; }
    private Plane groundPlane;

    #region MonoBehaviour Callbakcs
    private void Start()
    {
        myCamera = Camera.main;
        // a way to found the groundPlane in the scene
    }

    private void Update()
    {
        groundPlane = new Plane(Vector3.up, transform.position);
        cameraRay = myCamera.ScreenPointToRay(Input.mousePosition);

        if (groundPlane.Raycast(cameraRay, out rayLength))
        {
            PointToLook = cameraRay.GetPoint(rayLength);
            Debug.DrawLine(cameraRay.origin, PointToLook, Color.red);
            transform.LookAt(new Vector3(PointToLook.x, transform.position.y, PointToLook.z));
        }
    }
    #endregion

}
