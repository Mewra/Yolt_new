using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossAI : MonoBehaviour {

    private Animator animatorBoss;
    private NavMeshAgent navBoss;
    private bool GetPlayer = false;
    private float BossHp;
    
    // Use this for initialization
    void Start()
    {
        animatorBoss = GetComponent<Animator>();
        navBoss = gameObject.GetComponent<NavMeshAgent>();
        BossHp = 400f;
    }

    // Update is called once per frame
    void Update() {
   
    }

    IEnumerator searchStrongestPLayer()
    {
        while (true)
        {
            GameObject playerTarget = null;
            float hpPLayer = 0;
            foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
            {
                if (hpPLayer < player.transform.GetComponent<PlayerController>().returnHp())
                {
                    hpPLayer = player.transform.GetComponent<PlayerController>().returnHp();
                    playerTarget = player;
                }
            }
            if (playerTarget != null)
            {
                    GetPlayer = true;
                    navBoss.SetDestination(playerTarget.transform.position);
                    //yield return new WaitForSeconds(0.3f);
            }
            else
            {
                    GetPlayer = false;
                    playerTarget = null;
                    //yield return new WaitForSeconds(0.3f);
            }
            yield return new WaitForSeconds(0.3f);
        }
    }

}
