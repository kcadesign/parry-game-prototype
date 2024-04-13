using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleBossSounds : MonoBehaviour
{
    public SoundCollection BossSounds;

    private void OnEnable()
    {
        HandleBossHealth.OnBossHealthChange += HandleBossHealth_OnBossHealthChange;
        HandleBossHealth.OnBossDeath += HandleBossHealth_OnBossDeath;
    }

    private void OnDisable()
    {
        HandleBossHealth.OnBossHealthChange -= HandleBossHealth_OnBossHealthChange;
        HandleBossHealth.OnBossDeath -= HandleBossHealth_OnBossDeath;
    }

    private void HandleBossHealth_OnBossHealthChange(int currentHealth, int maxHealth)
    {
        BossSounds.PlaySound("BossHurt", transform);
    }

    private void HandleBossHealth_OnBossDeath()
    {
        BossSounds.PlaySound("BossDeath", transform);
    }
}
