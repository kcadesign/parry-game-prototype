using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleEnemyHealth : MonoBehaviour
{
    public HealthSystem EnemyHealth;

    [SerializeField] private int _maxHealth = 2;
    private int _currentHealth;

    private void Awake()
    {
        EnemyHealth = new HealthSystem(_maxHealth);
        _currentHealth = EnemyHealth.GetHealth();
    }

    private void OnEnable()
    {
        HandlePlayerCollisions.OnDamageEnemy += HandlePlayerCollisions_OnDamageEnemy;
    }

    private void OnDisable()
    {
        HandlePlayerCollisions.OnDamageEnemy -= HandlePlayerCollisions_OnDamageEnemy;

    }

    private void HandlePlayerCollisions_OnDamageEnemy(int damageAmount)
    {
        EnemyHealth.Damage(damageAmount);

        _currentHealth = EnemyHealth.GetHealth();
        Debug.Log($"Enemy health: {_currentHealth}");

        CheckHealth();
    }

    private void CheckHealth()
    {
        if(_currentHealth <= 0)
        {
            Destroy(gameObject.transform.parent.gameObject);
        }
    }
}
