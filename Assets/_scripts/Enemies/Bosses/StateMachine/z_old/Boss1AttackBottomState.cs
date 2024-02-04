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
        boss.CanChangePhase = false;

    }

    public override void UpdateState(Boss1StateManager boss)
    {
        //Debug.Log($"In {this.GetType().Name} update");

        if (boss.LeftHurtBox.Deflected || boss.RightHurtBox.Deflected)
        {
            boss.SwitchState(boss.HitGenericState);
        }
        else if (boss.FistsIdle) boss.SwitchState(boss.FistsIdleState);
        

    }

    public override void SwitchState(Boss1StateManager boss)
    {
        Debug.Log($"Switching from {this.GetType().Name}");
        // Reset trigger for current animation
        boss.Animator.ResetTrigger("AttackBottom");
    }

}
