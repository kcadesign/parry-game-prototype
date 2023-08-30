using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileEnemyController : EnemyControllerBase
{
    [Header("Projectile Parameters")]
    public GameObject ProjectileSpawner;

    protected override void PerformIdleActions()
    {
        base.PerformIdleActions();
        ProjectileSpawner.SetActive(false);
    }

    protected override void PerformAttackActions()
    {
        base.PerformAttackActions();
        
    }

    private void EnableProjectiles()
    {
        ProjectileSpawner.SetActive(true);
    }
}
