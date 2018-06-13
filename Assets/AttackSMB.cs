using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSMB : StateMachineBehaviour {

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // animator.gameObject.GetComponent<EnemyController>().StopCoroutine("Patrol");
        animator.gameObject.GetComponent<EnemyController>().StartCoroutine("AttackControl");

    }
    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks


    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //animator.gameObject.GetComponent<EnemyController>().StopCoroutine(animator.gameObject.GetComponent<EnemyController>().AttackControl());
        animator.gameObject.GetComponent<EnemyController>().StopCoroutine("AttackControl");
        animator.gameObject.GetComponent<EnemyController>().StartCoroutine("Patrol");
    }
}

