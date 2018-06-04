
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private Transform TargetPlayer = null;
    public NavMeshAgent nav;
    private DecisionTree dt;
    private DecisionTree dtAttack;
    public Vector3 Target;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        DTDecision d1 = new DTDecision(Nearest);
        DTDecision d2 = new DTDecision(AttackDecision);
        DTAction azione1 = new DTAction(FollowNearest);
        DTAction azione2 = new DTAction(WalkRandomly);
        DTAction attack = new DTAction(AttackAction);
        d1.AddLink(true, azione1);
        d1.AddLink(false, azione2);
        d2.AddLink(true, attack);
        dt = new DecisionTree(d1);
        dtAttack = new DecisionTree(d2);
        nav = gameObject.GetComponent<NavMeshAgent>();
        StartCoroutine(Patrol());
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("speed" , nav.velocity.magnitude);
    }

    public object Nearest(object o)
    {
        Debug.Log("Sono su Nearest");
        float targetdistance = Mathf.Infinity;
        GameObject playerTarget = null;
        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            float tmpDistance = Vector3.Distance( player.transform.position , transform.position);
            if (tmpDistance < targetdistance)
            {
                targetdistance = tmpDistance;
                playerTarget = player;
            }
        }
        if (playerTarget != null)
        {
            TargetPlayer = playerTarget.transform;
            return true;
            Debug.Log("Trovato");
        }
        else
        {
            TargetPlayer = null;
            return false;
        }
    }

    object  FollowNearest(object o)
    {
        if(TargetPlayer != null)
        {
            StartCoroutine(Follow());
        }

        return null; 
    }
    IEnumerator Follow()
    {   
        nav.SetDestination(TargetPlayer.transform.position);
        Debug.Log("Follow");
        yield return null;
    }
    IEnumerator Attack()
    {

        while(nav.remainingDistance <= 1)
        {
            Debug.Log("I'm attacking");
            yield return new WaitForSeconds(1f);

        }
        
    }
    object AttackDecision(object o)
    {
        if (nav.remainingDistance <= 1)
        {
             return true;
        }
        return false;
    }

    object AttackAction(object o)
    {
        StartCoroutine(Attack());
        return null;
    }

    object WalkRandomly(object o)
    {
        float myX = gameObject.transform.position.x;
        float myZ = gameObject.transform.position.z;
        float xPos = myX + Random.Range(myX - 100, myX + 100);
        float zPos = myZ + Random.Range(myZ - 100, myZ + 100);

        Target = new Vector3(xPos, gameObject.transform.position.y, zPos);

        nav.SetDestination(Target);
        return null;
    }
    public IEnumerator Patrol()
    {
        while (true)
        {
            dt.walk();
            yield return new WaitForSeconds(0.3f);
        }
    }
    public IEnumerator AttackControl()
    {
        while (true)
        {
            dtAttack.walk();
            yield return new WaitForSeconds(0.3f);
        }
    }
}
