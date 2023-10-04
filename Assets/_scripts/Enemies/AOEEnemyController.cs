using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOEEnemyController : HandleBaseEnemyState
{
    public Collider2D AttackCircleCollider;

    protected override void PerformAttackActions()
    {
        base.PerformAttackActions();

    }

    private void EnableAttackCircle() => AttackCircleCollider.enabled = true;
    private void DisableAttackCircle() => AttackCircleCollider.enabled = false;

}
