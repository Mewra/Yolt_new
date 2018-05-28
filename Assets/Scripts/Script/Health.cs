using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    public float _health;

    // Use this for initialization
    void Start()
    {
        _health = 100;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AreaHeal(float heal)
    {
        _health += heal;
    }


    public void TakeDamage(float dam)
    {
        _health -= dam;
    }


}