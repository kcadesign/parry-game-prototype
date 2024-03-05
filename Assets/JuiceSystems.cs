using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

public class JuiceSystems : MonoBehaviour
{
    [SerializeField] private float _enemyDeathStopDuration = 0.1f;

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
        // when an enemy is destroyed, pause time briefly (hit stop)
        StartCoroutine(HitStop(_enemyDeathStopDuration));
    }

    private IEnumerator HitStop(float hitStopDuration)
    {
        // pause time for hitStopDuration
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(hitStopDuration);
        // resume time
        Time.timeScale = 1f;
    }
}
