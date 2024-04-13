using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleEnemySounds : MonoBehaviour
{
    public SoundCollection EnemySounds;

    private void OnEnable()
    {
        HandleEnemyHealth.OnEnemyDeath += HandleEnemyHealth_OnEnemyDeath;
        AOEEnemyController.OnEnemyAttack += AOEEnemyController_OnEnemyAttack;
    }

    private void OnDisable()
    {
        HandleEnemyHealth.OnEnemyDeath -= HandleEnemyHealth_OnEnemyDeath;
        AOEEnemyController.OnEnemyAttack -= AOEEnemyController_OnEnemyAttack;
    }

    private void HandleEnemyHealth_OnEnemyDeath(GameObject deadEnemy)
    {
        if (deadEnemy == gameObject)
        {
            EnemySounds.PlaySound("Death", transform);
        }
    }

    private void AOEEnemyController_OnEnemyAttack(GameObject attackingEnemy)
    {
        if (attackingEnemy == gameObject)
        {
            EnemySounds.PlaySound("Attack", transform);
        }
    }

}
