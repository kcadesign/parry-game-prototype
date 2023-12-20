using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1IdleState : Boss1BaseState
{
    public override void EnterState(Boss1StateManager boss)
    {
        Debug.Log($"Entered {this.GetType().Name}");
        boss.Animator.SetBool("Idle", true);
    }

    public override void UpdateState(Boss1StateManager boss)
    {
        //Debug.Log($"In {this.GetType().Name} update");

        // If the boss can attack, switch to the appropriate attack state. If not, stay in idle state.
        if (boss.CanAttackLeft)
        {
            boss.SwitchState(boss.AttackLeftState);
        }
        else if (boss.CanAttackRight)
        {
            boss.SwitchState(boss.AttackRightState);
        }
        else if (boss.CanAttackBottom)
        {
            boss.SwitchState(boss.AttackBottomState);
        }
    }

    public override void SwitchState(Boss1StateManager boss)
    {
        Debug.Log($"Switching from {this.GetType().Name}");

        //reset trigger for current animation
        boss.Animator.SetBool("Idle", false);
    }

}
