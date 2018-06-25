using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using CRBT;

public class BossAI : MonoBehaviour
{

    private Animator animatorBoss;
    private NavMeshAgent navBoss;
    private bool GetPlayer = false;
    private float BossHp;
    private bool withGhost;
    private DecisionTree dtBoss;
    private BehaviorTree btTree;
    private bool variousAttack = false;
    public GameObject playerTarget = null;


    // Use this for initialization
    void Start()
    {

        DTDecision d1Boss = new DTDecision(StrongestPlayer);
        DTAction azione1 = new DTAction(FollowNearest);
        //DTAction azione2 = new DTAction(WalkRandomly);
        //DTAction attack = new DTAction(AttackAction);
        dtBoss = new DecisionTree(d1Boss);
        d1Boss.AddLink(true, azione1);
        animatorBoss = GetComponent<Animator>();
        navBoss = gameObject.GetComponent<NavMeshAgent>();
        BossHp = 400f;
        BTAction normalAttack = new BTAction(NormalAttack);
        BTAction jumpAttack = new BTAction(JumpAttack);
        BTAction rollAttack = new BTAction(RollAttack);


        BTCondition enemyHasGhosts = new BTCondition(SearchPlayerWithGhost);
        BTCondition bossHpless25 = new BTCondition(HpBossLessThan25);
        BTCondition hpPLayerless50 = new BTCondition(HpTargetMoreThan50);

        BTSequence attack = new BTSequence(new IBTTask[] { enemyHasGhosts, hpPLayerless50, normalAttack, jumpAttack, bossHpless25, jumpAttack, rollAttack });
        BTDecorator decorator = new BTDecoratorUntilFail(attack);

        btTree = new BehaviorTree(attack);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(navBoss.remainingDistance);
    }

    public object StrongestPlayer(object o)
    {
        return true;
    }

    object FollowNearest(object o)
    {
        StopCoroutine("SearchStrongestPLayer");
        StartCoroutine("SearchStrongestPLayer");
        //StartCoroutine("Follow");
        return null;
    }

    IEnumerator SearchStrongestPLayer()
    {
        while (!animatorBoss.GetBool("EnemyFound"))
        {
            Debug.Log("SearchStrongestPLayer");
            float hpPlayer = 0;
            foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
            {
                if (hpPlayer <= player.transform.GetComponent<PlayerController>().ReturnHp())
                {
                    hpPlayer = player.transform.GetComponent<PlayerController>().ReturnHp();
                    playerTarget = player;
                    animatorBoss.SetBool("EnemyFound", true);
                }
            }
            if (playerTarget != null)
            {
                GetPlayer = true;
                navBoss.SetDestination(playerTarget.transform.position);
            }
            else
            {
                GetPlayer = false;
                playerTarget = null;
            }
            yield return new WaitForSeconds(0.3f);
        }
    }

    //actions

    public bool JumpAttack()
    {
        Debug.Log("JumpAttack");
        JumpAttackAction();
        return true;
    }
    public bool RollAttack()
    {
        Debug.Log("RollAttack");
        return true;
    }
    public bool NormalAttack()
    {
        Debug.Log("Normal Attack");
        NormalBossAttack();
        return true;
    }
    // Conditions 
    public bool SearchPlayerWithGhost()
    {
        Debug.Log("Searchplayerwithghost");
        bool haIlGhosth = false;
        for (int i = 0; i < playerTarget.transform.GetComponent<PlayerController>()._Slots.Length; i++)
        {
            if (playerTarget.transform.GetComponent<PlayerController>()._Slots[i] == true)
            {
                haIlGhosth = true;

                break;
            }

        }
        return haIlGhosth;
    }

    public bool HpTargetMoreThan50()  
    {
        Debug.Log("HpMorethan50");
        if (playerTarget.GetComponent<HealthPlayer>()._health <= playerTarget.GetComponent<HealthPlayer>().totalHealth / 2)
        {
            variousAttack = true;
        }
        else
        {
            //variousAttack = false;
        }
        return true;
    }

    public bool HpBossLessThan25()
    {Debug.Log("Ho");
        if (BossHp <= 100)
        {
            return true;
        }
        return false;
    }
    //attacchi
    void NormalBossAttack()
    {
        Debug.Log("Normal attack void");

        if (/*navBoss.remainingDistance <= 2f */animatorBoss.GetBool("enemyHpMoreThan50") && animatorBoss.GetBool("enemyInRange") && !variousAttack)
        {
            Debug.Log("Normal attack");
        }
    }
    void JumpAttackAction()
    {
        Debug.Log("jump attack void");
        if (animatorBoss.GetBool("enemyHpMoreThan50") && animatorBoss.GetBool("enemyInRange") && variousAttack)
        {
            Debug.Log("jump attack");
        }

    }

    IEnumerator Attack()
    {
        Debug.Log("Coroutine attacco");

        while (animatorBoss.GetBool("enemyInRange"))
        {
            if (playerTarget == null)
            {
                animatorBoss.SetBool("enemyFound", false);
                Debug.Log("Sono qui");
            }
            btTree.Step();

            yield return new WaitForSeconds(0.5f);
        }

    }
}
