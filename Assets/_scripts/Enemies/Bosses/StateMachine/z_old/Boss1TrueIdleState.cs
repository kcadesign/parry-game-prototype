using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1TrueIdleState : Boss1BaseState
{
    public override void EnterState(Boss1StateManager boss)
    {
        Debug.Log($"Entered {this.GetType().Name}");
        boss.Animator.SetBool("TrueIdle", true);
        boss.CanChangePhase = true;
    }

    public override void UpdateState(Boss1StateManager boss)
    {
        //Debug.Log($"In {this.GetType().Name} update");

        // If the boss can attack, switch to the appropriate attack state. If not, stay in idle state.
        if (boss.BulletIdle)
        {
            // switch to projectile idle phase
            boss.SwitchState(boss.BulletIdleState);
        }
        else if (boss.FistsIdle)
        {
            // switch to fists idle phase
            boss.SwitchState(boss.FistsIdleState);
        }


    }

    public override void SwitchState(Boss1StateManager boss)
    {
        Debug.Log($"Switching from {this.GetType().Name}");
        //reset trigger for current animation
        boss.Animator.SetBool("TrueIdle", false);
    }
}
