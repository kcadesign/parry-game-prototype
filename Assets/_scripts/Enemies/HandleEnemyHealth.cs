using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleEnemyHealth : MonoBehaviour
{
    public static Action<GameObject> OnEnemyDeath;

    private HealthSystem _enemyHealth;
    public int MaxHealth = 25;
    public int CurrentHealth;

    [SerializeField] private bool _reportDeath = true;

    private void Awake()
    {
        _enemyHealth = new HealthSystem(MaxHealth);
        CurrentHealth = _enemyHealth.GetHealth();
    }

    private void OnEnable()
    {
        HandleDamageOut.OnOutputDamage += HandleEnemyDamageOutput_OnOutputDamage;
    }

    private void OnDisable()
    {
        HandleDamageOut.OnOutputDamage -= HandleEnemyDamageOutput_OnOutputDamage;
    }

    private void HandleEnemyDamageOutput_OnOutputDamage(GameObject objectDamager, GameObject damagedObject, int damageAmount)
    {
        if(damagedObject == gameObject)
        {
            _enemyHealth.Damage(damageAmount);

            CurrentHealth = _enemyHealth.GetHealth();
            Debug.Log($"{gameObject.transform.parent.name}'s health: {CurrentHealth}");

            CheckHealth();
        }
    }
    
    private void CheckHealth()
    {
        if (CurrentHealth <= 0)
        {
            if (_reportDeath)
            {
                Debug.Log($"{gameObject.transform.parent.name} has died");
                OnEnemyDeath?.Invoke(gameObject.transform.parent.gameObject);

            }
            Destroy(gameObject.transform.parent.gameObject);

        }
    }
}
