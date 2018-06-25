using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    //ENEMY
    public Image healthBar;
    public float _health;
    public float currentHealth;
    //Lista dei player connessi


    private void Awake()
    {
        currentHealth = 100f;
    }
    // Use this for initialization
    void Start()
    {
        _health = 100;
        currentHealth = _health;
        // healthBar.fillAmount = calculateHealt();
    }


    public void AreaHeal(float heal)
    {
        _health += heal;
    }

    [PunRPC]
    public void TakeDamage(float dam)
    {
        currentHealth -= dam;
        // healthBar.fillAmount = calculateHealt();
        if(currentHealth <= 0 && PhotonNetwork.isMasterClient)
        {
            //if player.state==CLASSES.GHOST
            PhotonNetwork.InstantiateSceneObject(GameManager.Instance.luth.name, this.gameObject.transform.position, Quaternion.identity, 0, null);
            GameManager.Instance.enemyKilled();
            PhotonNetwork.Destroy(gameObject);
            
        }
    }
    float calculateHealt()
    {
        return currentHealth / _health;
    }

    public void OnTriggerEnter(Collider other)
    {
        
    }
}