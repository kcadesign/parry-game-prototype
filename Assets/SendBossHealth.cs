using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendBossHealth : MonoBehaviour
{
    public static event Action<int, int> OnBossHealthChange;

    private HandleEnemyHealth _bossHealth;

    private void Awake()
    {
        _bossHealth = GetComponent<HandleEnemyHealth>();
        OnBossHealthChange?.Invoke(_bossHealth.CurrentHealth, _bossHealth.MaxHealth);
    }

    private void Update()
    {
        OnBossHealthChange?.Invoke(_bossHealth.CurrentHealth, _bossHealth.MaxHealth);
    }
}
