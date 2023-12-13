using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1IdleState : Boss1BaseState
{
    public override void EnterState(Boss1StateManager boss)
    {
        Debug.Log($"Entered {this.GetType().Name}");
        boss.Animator.SetTrigger("Idle");
    }

    public override void UpdateState(Boss1StateManager boss)
    {
        Debug.Log($"In {this.GetType().Name} update");

        // Play loop idle animation

        // Switch state if player has triggered an attack
        if(boss.CanAttackLeft) boss.SwitchState(boss._attackLeftState);
        else if (boss.CanAttackRight) boss.SwitchState(boss._attackRightState);
        else if (boss.CanAttackBottom) boss.SwitchState(boss._attackBottomState);
    }

    public override void SwitchState(Boss1StateManager boss)
    {
        Debug.Log($"Switching from {this.GetType().Name}");

        throw new System.NotImplementedException();
    }

}
