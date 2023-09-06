using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleEnemyHealth : MonoBehaviour
{
    private HealthSystem _enemyHealth;
    [SerializeField] private int maxHealth = 2;
    private int currentHealth;

    private void Awake()
    {
        _enemyHealth = new HealthSystem(maxHealth);
        currentHealth = _enemyHealth.GetHealth();
    }

    private void OnEnable()
    {
        HandlePlayerCollisions.OnDamageEnemy += HandlePlayerCollisions_OnDamageEnemy;
    }

    private void OnDisable()
    {
        HandlePlayerCollisions.OnDamageEnemy -= HandlePlayerCollisions_OnDamageEnemy;
    }

    // This code is causing all subscribed enemies to take the same damage at the same time
    private void HandlePlayerCollisions_OnDamageEnemy(GameObject collisionObject, int damageAmount)
    {
        if(collisionObject == gameObject)
        {
            _enemyHealth.Damage(damageAmount);

            currentHealth = _enemyHealth.GetHealth();
            Debug.Log($"{gameObject.transform.parent.gameObject.transform.parent.name}'s health: {currentHealth}");

            CheckHealth();
        }
    }

    private void CheckHealth()
    {
        if (currentHealth <= 0)
        {
            Destroy(gameObject.transform.parent.gameObject);
        }
    }
}
