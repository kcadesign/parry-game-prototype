using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1AttackBottomState : Boss1BaseState
{
    public override void EnterState(Boss1StateManager boss)
    {
        Debug.Log($"Entered {this.GetType().Name}");

        // Trigger attack left animation
        boss.Animator.SetTrigger("AttackBottom");

    }

    public override void UpdateState(Boss1StateManager boss)
    {
        //Debug.Log($"In {this.GetType().Name} update");

        if (boss.LeftHurtBoxCollisions.Deflected || boss.RightHurtBoxCollisions.Deflected) 
        {
            boss.SwitchState(boss.HitGeneric);
        }

        if (boss.Idle) boss.SwitchState(boss.IdleState);

    }

    public override void SwitchState(Boss1StateManager boss)
    {
        Debug.Log($"Switching from {this.GetType().Name}");
        // Reset trigger for current animation
        boss.Animator.ResetTrigger("AttackBottom");
    }

}
