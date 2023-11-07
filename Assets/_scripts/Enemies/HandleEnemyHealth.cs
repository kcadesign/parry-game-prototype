using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleEnemyHealth : MonoBehaviour
{
    public static Action<GameObject> OnEnemyDeath;

    private HealthSystem _enemyHealth;
    public int MaxHealth = 2;
    private int currentHealth;

    private void Awake()
    {
        _enemyHealth = new HealthSystem(MaxHealth);
        currentHealth = _enemyHealth.GetHealth();
    }

    private void OnEnable()
    {
        HandleDamageOut.OnOutputDamage += HandleEnemyDamageOutput_OnOutputDamage;
    }

    private void OnDisable()
    {
        HandleDamageOut.OnOutputDamage -= HandleEnemyDamageOutput_OnOutputDamage;
    }

    private void HandleEnemyDamageOutput_OnOutputDamage(GameObject objectDamager, GameObject collisionObject, int damageAmount)
    {
        if(collisionObject == gameObject)
        {
            _enemyHealth.Damage(damageAmount);

            currentHealth = _enemyHealth.GetHealth();
            Debug.Log($"{gameObject.transform.parent.name}'s health: {currentHealth}");

            CheckHealth();
        }
    }
    
    private void CheckHealth()
    {
        if (currentHealth <= 0)
        {
            OnEnemyDeath?.Invoke(gameObject.transform.parent.gameObject);

            Destroy(gameObject.transform.parent.gameObject);

        }
    }
}
