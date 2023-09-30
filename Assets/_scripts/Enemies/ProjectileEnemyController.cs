using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileEnemyController : HandleBaseEnemyState
{
    [Header("Projectile Parameters")]
    public SpawnProjectile ProjectileSpawner;
    /*
    protected override void PerformIdleActions()
    {
        base.PerformIdleActions();
    }

    protected override void PerformAttackActions()
    {
        base.PerformAttackActions();
    }
    */
    private void EnableProjectiles() => ProjectileSpawner.InvokeProjectile();
    private void DisableProjectiles() => ProjectileSpawner.CancelInvokeProjectile();

}
