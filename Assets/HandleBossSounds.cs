using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleBossSounds : MonoBehaviour
{
    public SoundCollection BossSounds;

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
        BossSounds.PlaySound("BossHurt", transform);
    }
}
