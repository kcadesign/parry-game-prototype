using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOEEnemyController : HandleBaseEnemyState
{
    public GameObject AttackCircle;

    protected override void PerformAttackActions()
    {
        base.PerformAttackActions();

    }

    private void EnableAttackCircle() => AttackCircle.SetActive(true);
    private void DisableAttackCircle() => AttackCircle.SetActive(false);

}
