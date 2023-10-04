using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOEEnemyController : HandleBaseEnemyState
{
    public Collider2D AttackCircleCollider;

    private void EnableAttackCircleCollider() => AttackCircleCollider.enabled = true;
    private void DisableAttackCircleCollider() => AttackCircleCollider.enabled = false;

}
