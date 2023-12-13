using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleBossHealth : MonoBehaviour
{
    public static Action<GameObject> OnBossDeath;
    public static Action<int, int> OnBossHealthChange;

    private HealthSystem _bossHealth;
    public int MaxHealth;
    public int CurrentHealth;

    private void Awake()
    {
        _bossHealth = new HealthSystem(MaxHealth);
        CurrentHealth = _bossHealth.GetHealth();

        OnBossHealthChange?.Invoke(CurrentHealth, MaxHealth);

        Debug.Log($"{gameObject.transform.parent.name}'s health: {CurrentHealth}");
    }

    private void OnEnable()
    {
        HandleDamageOut.OnOutputDamage += HandleEnemyDamageOutput_OnOutputDamage;
    }

    private void OnDisable()
    {
        HandleDamageOut.OnOutputDamage -= HandleEnemyDamageOutput_OnOutputDamage;
    }

/*    private void Update()
    {
        OnBossHealthChange?.Invoke(CurrentHealth, MaxHealth);
    }
*/
    private void HandleEnemyDamageOutput_OnOutputDamage(GameObject objectDamager, GameObject damagedObject, int damageAmount)
    {
        if (damagedObject == gameObject)
        {
            _bossHealth.Damage(damageAmount);

            CurrentHealth = _bossHealth.GetHealth();
            OnBossHealthChange?.Invoke(CurrentHealth, MaxHealth);

            Debug.Log($"{gameObject.transform.parent.name}'s health: {CurrentHealth}");

            CheckHealth();
        }
    }

    private void CheckHealth()
    {
        if (CurrentHealth <= 0)
        {
            OnBossDeath?.Invoke(gameObject.transform.parent.gameObject);

            Destroy(gameObject.transform.parent.gameObject);

        }
    }
}
