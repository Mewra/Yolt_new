
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
    private bool GetFound=false;
    public GameObject playerTarget;
    public bool targetInRange = false;
    //public AnimationClip clip;
   

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
        //AnimationEvent animationEvent = new AnimationEvent();
        //animationEvent.functionName = "DoDamage";
        //Debug.Log(clip.length*0.66);
        
        //animationEvent.time = (clip.length * 0.66f);
        //clip.AddEvent(animationEvent);


    }

    // Update is called once per frame
    void Update()
    {
        if(nav.remainingDistance<=nav.stoppingDistance && GetFound)
        {
            animator.SetBool("TargetInRange", true);
        }
        else
        {
            animator.SetBool("TargetInRange", false);
        }
    }

    public object Nearest(object o)
    {

        return true;
        
    }
    //messo il nearest in una couroutine
    IEnumerator UpdateNearest()
    {
        while (true)
        {
            float targetdistance = Mathf.Infinity;
            playerTarget = null;
            foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
            {
                float tmpDistance = Vector3.Distance(player.transform.position, transform.position);
                if (tmpDistance < targetdistance)
                {
                    targetdistance = tmpDistance;                  
                    playerTarget = player;
                }
            }
            if (playerTarget != null)
            {
                GetFound = true;
                TargetPlayer = playerTarget.transform;
                //yield return new WaitForSeconds(0.3f);

            }
            else
            {
                TargetPlayer = null;
                //yield return new WaitForSeconds(0.3f);
            }
            yield return new WaitForSeconds(0.3f);
        }
    }
    object  FollowNearest(object o)
    {        
    StopCoroutine("UpdateNearest");
    StartCoroutine("UpdateNearest");
    StartCoroutine("Follow");
    return null; 
    }
   
    IEnumerator Follow()
    {
        while (true)
        {
            if (TargetPlayer != null)
            {
                nav.SetDestination(TargetPlayer.transform.position);
            }
            yield return new WaitForSeconds(0.3f);
        }
    }

    IEnumerator Attack()
    {
        while(nav.remainingDistance <= nav.stoppingDistance)
        {
            yield return new WaitForSeconds(1f);
        }
    }

    object AttackDecision(object o)
    {
        if (nav.remainingDistance <= 4)
        {
            animator.SetBool("TargetInRange", true);
            return true;
        }
        animator.SetBool("TargetInRange", false);
        return false;
    }

    object AttackAction(object o)
    {
        StartCoroutine("Attack");
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
        GetFound = false;
        while (!GetFound)
        {
            dt.walk();//comment
            yield return new WaitForSeconds(0.3f);
        }
        StartCoroutine(UpdateNearest()); //messa ora  
    }
    
    public IEnumerator AttackControl()
    {
        while (true)
        {
            dtAttack.walk();
            yield return new WaitForSeconds(0.3f); 
            /*if (PhotonNetwork.isMasterClient || true)
            {
                Debug.Log(animator.GetCurrentAnimatorStateInfo(0).length);
                playerTarget.GetComponentInParent<PhotonView>().RPC("TakeDamageOnPlayer", PhotonTargets.AllViaServer, 10f);
                Debug.Log(playerTarget);
                playerTarget.GetComponentInParent<HealthPlayer>().TakeDamageOnPlayer(10f);
            }*/
            
        }
    }

    public void DoDamage()
    {
        if (PhotonNetwork.isMasterClient || true)
        {
            Debug.Log(animator.GetCurrentAnimatorStateInfo(0).length);
            playerTarget.GetComponentInParent<PhotonView>().RPC("TakeDamageOnPlayer", PhotonTargets.AllViaServer, 10f);
            playerTarget.GetComponentInParent<HealthPlayer>().TakeDamageOnPlayer(10f);
        }
    }

}
