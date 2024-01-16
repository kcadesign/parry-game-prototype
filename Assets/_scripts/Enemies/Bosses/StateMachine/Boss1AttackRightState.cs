using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1AttackRightState : Boss1BaseState
{
    public override void EnterState(Boss1StateManager boss)
    {
        Debug.Log($"Entered {this.GetType().Name}");

        boss.Animator.SetTrigger("AttackRight");

    }

    public override void UpdateState(Boss1StateManager boss)
    {
        //Debug.Log($"In {this.GetType().Name} update");

        //Debug.Log($"Deflected: {boss.RightHurtBoxCollisions.Deflected}");
        if (boss.RightHurtBoxCollisions.Deflected) boss.SwitchState(boss.HitFromRightState);

        if (boss.Idle) boss.SwitchState(boss.IdleState);
    }

    public override void SwitchState(Boss1StateManager boss)
    {
        Debug.Log($"Switching from {this.GetType().Name}");

        boss.Animator.ResetTrigger("AttackRight");
    }

}
