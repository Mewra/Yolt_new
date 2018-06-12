using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public Image healthBar;
    public float _health;
    public float currentHealth;

    // Use this for initialization
    void Start()
    {
        _health = 100;
        currentHealth = _health;
        healthBar.fillAmount = calculateHealt();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.X))
        {
            TakeDamage(6f);
        }
    }

    public void AreaHeal(float heal)
    {
        _health += heal;
    }


    public void TakeDamage(float dam)
    {
        currentHealth -= dam;
        healthBar.fillAmount = calculateHealt();
    }
    float calculateHealt()
    {
        return currentHealth / _health;
    } 
    



}