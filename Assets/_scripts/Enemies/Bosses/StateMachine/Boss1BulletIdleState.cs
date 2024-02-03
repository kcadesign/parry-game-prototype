using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1BulletIdleState : Boss1BaseState
{
    public override void EnterState(Boss1StateManager boss)
    {
        Debug.Log($"Entered {this.GetType().Name}");
        boss.Animator.SetBool("BulletIdle", true);
        boss.CanChangePhase = true;
    }

    public override void UpdateState(Boss1StateManager boss)
    {
        //Debug.Log($"In {this.GetType().Name} update");

        // If the boss can attack, switch to the appropriate attack state. If not, stay in idle state.
        /*if (boss.CanAttackLeft)
        {
            // switch to projectile attack left state
            boss.SwitchState(boss.BulletAttackLeftState);
        }
        else if (boss.CanAttackRight)
        {
            // switch to projectile attack right state
        }
        else if (boss.CanAttackBottom)
        {
            // switch to projectile attack bottom state
        }
        else if (boss.TrueIdle)
        {
            // switch to fists idle state
            boss.SwitchState(boss.TrueIdleState);
        }*/

        if (boss.FistsIdle)
        {
            boss.SwitchState(boss.TrueIdleState);
        }
        

    }

    public override void SwitchState(Boss1StateManager boss)
    {
        Debug.Log($"Switching from {this.GetType().Name}");

        //reset trigger for current animation
        boss.Animator.SetBool("BulletIdle", false);
    }
}
