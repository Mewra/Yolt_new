﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSKybox : MonoBehaviour {

    public float RotateSpeed = 2.0f;
	
	// Update is called once per frame
	void Update ()
    {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * RotateSpeed);	
	}
}
