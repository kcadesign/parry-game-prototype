using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleDeath : MonoBehaviour
{
    public GameObject DeathExplosion;
    public GameObject EnemyBodyParticles;

    private void OnEnable()
    {
        HandleEnemyHealth.OnEnemyDeath += HandleEnemyHealth_OnEnemyDeath;
    }

    private void OnDisable()
    {
        HandleEnemyHealth.OnEnemyDeath -= HandleEnemyHealth_OnEnemyDeath;
    }

    private void HandleEnemyHealth_OnEnemyDeath(GameObject deadEnemy)
    {
        if (deadEnemy == gameObject.transform.parent.gameObject)
        {
            Instantiate(DeathExplosion, transform.position, transform.rotation);
            Instantiate(EnemyBodyParticles, transform.position, transform.rotation);
        }
    }

}
