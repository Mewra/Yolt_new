using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    Image healthBar;
    public GameObject fisting;
    public float health;
    

    // Use this for initialization
    void Start()
    {
       
        health = fisting.GetComponent<Health>().currentHealth;
        healthBar = this.gameObject.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        health = fisting.GetComponent<Health>().currentHealth;
        healthBar.rectTransform.sizeDelta = new Vector2((health * 2.5f) / 100f, healthBar.rectTransform.sizeDelta.y);
    }
   
}
