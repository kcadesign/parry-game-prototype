using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1BulletIdle : StateMachineBehaviour
{
    private Boss1Controller _boss1Controller;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _boss1Controller = animator.GetComponent<Boss1Controller>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_boss1Controller.BossDead)
        {
            animator.SetTrigger("BulletDeath");
        }
        else if (_boss1Controller.FistsIdle)
        {
            animator.SetBool("FistsIdle", true);
        }
        else if (_boss1Controller.ProjectileHit)
        {
            animator.SetTrigger("BulletHitGeneric");
        }
        else if (_boss1Controller.CanAttackLeft)
        {
            animator.SetBool("BulletIdle", false);
            animator.SetTrigger("BulletAttackLeft");
        }
        else if (_boss1Controller.CanAttackRight)
        {
            animator.SetBool("BulletIdle", false);
            animator.SetTrigger("BulletAttackRight");
        }
        else if (_boss1Controller.CanAttackBottom)
        {
            animator.SetBool("BulletIdle", false);
            animator.SetTrigger("BulletAttackBottom");
        }

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("BulletIdle", false);
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
