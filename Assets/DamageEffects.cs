using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEffects : MonoBehaviour
{
    public GameObject DamageParticlePrefab;

    private void OnEnable()
    {
        HandleBossHealth.OnBossHealthChange += HandleBossHealth_OnBossHealthChange;
    }

    private void OnDisable()
    {
        HandleBossHealth.OnBossHealthChange -= HandleBossHealth_OnBossHealthChange;
    }

    private void HandleBossHealth_OnBossHealthChange(int currentHealth, int maxHealth)
    {
        Instantiate(DamageParticlePrefab, transform.position, Quaternion.identity);
    }
}
