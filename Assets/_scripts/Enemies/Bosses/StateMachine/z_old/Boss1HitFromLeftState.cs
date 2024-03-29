using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1HitFromLeftState : Boss1BaseState
{
    public override void EnterState(Boss1StateManager boss)
    {
        Debug.Log($"Entered {this.GetType().Name}");

        boss.Animator.SetTrigger("HitLeft");
    }


    public override void UpdateState(Boss1StateManager boss)
    {
        if (boss.CanAttackLeft) boss.SwitchState(boss.AttackLeftState);
        else if (boss.BossDead) boss.SwitchState(boss.FistsDeathState);
        else boss.SwitchState(boss.FistsIdleState);
    }

    public override void SwitchState(Boss1StateManager boss)
    {
        Debug.Log($"Switching from {this.GetType().Name}");
        boss.Animator.ResetTrigger("HitLeft");
    }

}
