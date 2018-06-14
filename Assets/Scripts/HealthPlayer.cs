using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthPlayer : MonoBehaviour
{
    public GameObject healthBar;
    private Image _healthBar;
    public float _health;

    // Use this for initialization
    void Start()
    {
        
        healthBar = GameObject.FindGameObjectWithTag("Health Bar");
        _healthBar = healthBar.GetComponent<Image>();
        _health = 100f;
        if(_healthBar != null)
        {
            _healthBar.fillAmount = CalculateHealth();
        }
    }

    [PunRPC]
    public void AreaHeal(float heal)
    {
        _health += heal;
    }

    [PunRPC]
    public void TakeDamageOnPlayer(float dam)
    {
        _health -= dam;
        if(_healthBar != null)
        {
            _healthBar.fillAmount = CalculateHealth();
        }
    }

    float CalculateHealth()
    {
        return _health / 100f;
    }

}