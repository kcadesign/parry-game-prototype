using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1HitGenericState : Boss1BaseState
{
    public override void EnterState(Boss1StateManager boss)
    {
        Debug.Log($"Entered {this.GetType().Name}");

        boss.Animator.SetTrigger("HitGeneric");
    }


    public override void UpdateState(Boss1StateManager boss)
    {
        if (boss.CanAttackBottom) boss.SwitchState(boss.AttackBottomState);
        if (!boss.LeftHurtBox.Deflected || !boss.RightHurtBox.Deflected)
        {
            boss.SwitchState(boss.IdleState);
        }
        if (boss.FistsIdle) boss.SwitchState(boss.IdleState);
        if (boss.BossDead) boss.SwitchState(boss.FistsDeathState);
    }

    public override void SwitchState(Boss1StateManager boss)
    {
        Debug.Log($"Switching from {this.GetType().Name}");
        boss.Animator.ResetTrigger("HitGeneric");
    }
}
