using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleBossSounds : MonoBehaviour
{
    public SoundCollection BossSounds;

    private void OnEnable()
    {
        HandleBossHealth.OnBossHealthChange += HandleBossHealth_OnBossHealthChange;
        //HandleBossHealth.OnBossDeath += HandleBossHealth_OnBossDeath;
        HandleBossDeath.OnBossDeathAnimStart += HandleBossDeath_OnBossDeathAnimStart;
    }

    private void OnDisable()
    {
        HandleBossHealth.OnBossHealthChange -= HandleBossHealth_OnBossHealthChange;
        //HandleBossHealth.OnBossDeath -= HandleBossHealth_OnBossDeath;
        HandleBossDeath.OnBossDeathAnimStart -= HandleBossDeath_OnBossDeathAnimStart;
    }

    private void HandleBossHealth_OnBossHealthChange(int currentHealth, int maxHealth)
    {
        BossSounds.PlaySound("BossHurt", transform);
    }

    private void HandleBossDeath_OnBossDeathAnimStart()
    {
        BossSounds.PlaySound("BossDeath", transform);
    }
}
