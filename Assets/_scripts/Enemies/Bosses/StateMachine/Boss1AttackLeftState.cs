using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1AttackLeftState : Boss1BaseState
{
    public override void EnterState(Boss1StateManager boss)
    {
        Debug.Log($"Entered {this.GetType().Name}");

        // Trigger single play of attack left animation
    }

    public override void UpdateState(Boss1StateManager boss)
    {
        Debug.Log($"In {this.GetType().Name} update");

        // Check if attack animation has finished before deciding next state

        if (boss.CanAttackRight) boss.SwitchState(boss._attackRightState);
        else if (boss.CanAttackBottom) boss.SwitchState(boss._attackBottomState);
        else if (!boss.CanAttackLeft && !boss.CanAttackRight && !boss.CanAttackBottom) boss.SwitchState(boss._idleState);
    }

    public override void SwitchState(Boss1StateManager boss)
    {
        Debug.Log($"Switching from {this.GetType().Name}");
    }

}
