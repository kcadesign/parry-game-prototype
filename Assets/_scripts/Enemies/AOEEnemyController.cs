using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOEEnemyController : HandleBaseEnemyState
{
    public static Action<GameObject> OnEnemyAttack;
    public Collider2D AttackCircleCollider;

    private void EnableAttackCircleCollider()
    {
        AttackCircleCollider.enabled = true;
        OnEnemyAttack?.Invoke(gameObject);
    }
    private void DisableAttackCircleCollider() => AttackCircleCollider.enabled = false;

}
