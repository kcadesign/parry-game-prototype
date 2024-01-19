using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1BulletAttackLeftState : Boss1BaseState
{
    public override void EnterState(Boss1StateManager boss)
    {
        Debug.Log($"Entered {this.GetType().Name}");

        // Trigger attack left animation
        boss.Animator.SetTrigger("BulletAttackLeft");
    }

    public override void UpdateState(Boss1StateManager boss)
    {
        //Debug.Log($"In {this.GetType().Name} update");

        //Debug.Log($"Deflected: {boss.LeftHurtBoxCollisions.Deflected}");
        if (boss.BulletIdle) boss.SwitchState(boss.BulletIdleState);
    }

    public override void SwitchState(Boss1StateManager boss)
    {
        //Debug.Log($"Switching from {this.GetType().Name}");
        // Reset trigger for current animation
        boss.Animator.ResetTrigger("BulletAttackLeft");
    }
}
