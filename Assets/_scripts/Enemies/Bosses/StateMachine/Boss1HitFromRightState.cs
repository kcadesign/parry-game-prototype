using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1HitFromRightState : Boss1BaseState
{
    public override void EnterState(Boss1StateManager boss)
    {
        Debug.Log($"Entered {this.GetType().Name}");

        boss.Animator.SetTrigger("HitRight");
    }


    public override void UpdateState(Boss1StateManager boss)
    {
        if (boss.CanAttackRight) boss.SwitchState(boss.AttackRightState);
        else if (boss.FistsIdle) boss.SwitchState(boss.FistsIdleState);
        else if (boss.BossDead) boss.SwitchState(boss.FistsDeathState);
    }

    public override void SwitchState(Boss1StateManager boss)
    {
        Debug.Log($"Switching from {this.GetType().Name}");
        boss.Animator.ResetTrigger("HitRight");
    }

}
