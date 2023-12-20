using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1AttackLeftState : Boss1BaseState
{
    public override void EnterState(Boss1StateManager boss)
    {
        Debug.Log($"Entered {this.GetType().Name}");

        // Trigger attack left animation
        boss.Animator.SetTrigger("AttackLeft");
    }

    public override void UpdateState(Boss1StateManager boss)
    {
        //Debug.Log($"In {this.GetType().Name} update");

        //Debug.Log($"Deflected: {boss.Deflected}");

        if (boss.Deflected) boss.SwitchState(boss.HitFromLeftState);

        if (boss.Idle) boss.SwitchState(boss.IdleState);
    }

    public override void SwitchState(Boss1StateManager boss)
    {
        //Debug.Log($"Switching from {this.GetType().Name}");
        // Reset trigger for current animation
        boss.Animator.ResetTrigger("AttackLeft");
    }

}
