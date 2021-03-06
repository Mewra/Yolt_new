﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;
    private RotatePlayer rt;
    private float m_horizontal, m_vertical;

    private void Start()
    {
        anim = GetComponentInParent<Animator>();
        rt = GetComponent<RotatePlayer>();
    }

    private void Update()
    {
        m_horizontal = Input.GetAxis("Horizontal");
        m_vertical = Input.GetAxis("Vertical");
        if (m_horizontal == 0 && m_vertical == 0)
        {
            anim.SetFloat("speed_y", 0f);
            anim.SetFloat("speed_x", 0f);
        }
        else
        {
            anim.SetFloat("speed_y", m_vertical * (rt.PointToLook - transform.position).normalized.z);
            anim.SetFloat("speed_x", m_horizontal * (rt.PointToLook - transform.position).normalized.x);


        }
        
    }
}
