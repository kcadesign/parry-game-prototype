using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandlePlayerHealth : MonoBehaviour
{
    public delegate void PlayerHealthChange(int currentHealth, bool playerAlive);
    public static event PlayerHealthChange OnHealthChange;

    public HealthSystem PlayerHealth;

    [SerializeField] private int _maxHealth = 5;
    private int _currentHealth;
    private bool _playerAlive = true;

    private void Awake()
    {
        PlayerHealth = new HealthSystem(_maxHealth);
        _currentHealth = PlayerHealth.GetHealth();

        OnHealthChange?.Invoke(_currentHealth, _playerAlive);
    }

    private void OnEnable()
    {
        HandleEnemyDamageOutput.OnOutputDamage += HandleEnemyDamageOutput_OnOutputDamage;
        PlayerTriggerEnter.OnAreaDamagePlayer += PlayerTriggerEnter_OnAreaDamagePlayer;
    }
    private void OnDisable()
    {
        HandleEnemyDamageOutput.OnOutputDamage += HandleEnemyDamageOutput_OnOutputDamage;
        PlayerTriggerEnter.OnAreaDamagePlayer -= PlayerTriggerEnter_OnAreaDamagePlayer;
    }

    private void HandleEnemyDamageOutput_OnOutputDamage(int damageAmount)
    {
        PlayerHealth.Damage(damageAmount);

        _currentHealth = PlayerHealth.GetHealth();
        CheckPlayerAlive();

        OnHealthChange?.Invoke(_currentHealth, _playerAlive);

        Debug.Log(_currentHealth);
    }


    private void PlayerTriggerEnter_OnAreaDamagePlayer(int damageAmount)
    {
        PlayerHealth.Damage(damageAmount);

        _currentHealth = PlayerHealth.GetHealth();
        CheckPlayerAlive();

        OnHealthChange?.Invoke(_currentHealth, _playerAlive);
    }

    private void CheckPlayerAlive()
    {
        if (_currentHealth <= 0)
        {
            _playerAlive = false;
        }
        else
        {
            _playerAlive = true;
        }
    }
}
