using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementGhost : MonoBehaviour
{
    public float speed;
    private float smoothedSpeed;
    private float m_horizontal, m_vertical;
    private PhotonView myPhotonView;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }


    private void Update()
    {
        m_horizontal = Input.GetAxis("Horizontal");
        m_vertical = Input.GetAxis("Vertical");
        if(m_horizontal == 0 && m_vertical == 0)
        {
            anim.SetFloat("speed_y", 0f);
            anim.SetFloat("speed_x", 0f);
        }
        else
        {
            anim.SetFloat("speed_y", m_vertical);
            anim.SetFloat("speed_x", m_horizontal);
        }

        if (m_vertical != 0)
        {
            smoothedSpeed = speed;
            if (m_horizontal != 0)
            {
                smoothedSpeed = speed * 0.7f;
            }
            transform.position = Vector3.Lerp(transform.position, transform.position + new Vector3(m_vertical, 0f, m_vertical), smoothedSpeed * Time.deltaTime);

        }
        if (m_horizontal != 0)
        {
            smoothedSpeed = speed;
            if (m_vertical != 0)
            {
                smoothedSpeed = speed * 0.7f;
            }
            transform.position = Vector3.Lerp(transform.position, transform.position + new Vector3(m_horizontal, 0f, -m_horizontal), smoothedSpeed * Time.deltaTime);
        }
    }
}