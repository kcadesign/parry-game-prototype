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
        HandleGameStateUI.OnGameRestart += HandleGameStateUI_OnGameRestart;
    }


    private void OnDisable()
    {
        HandleEnemyDamageOutput.OnOutputDamage += HandleEnemyDamageOutput_OnOutputDamage;
        PlayerTriggerEnter.OnAreaDamagePlayer -= PlayerTriggerEnter_OnAreaDamagePlayer;
        HandleGameStateUI.OnGameRestart -= HandleGameStateUI_OnGameRestart;

    }
    /*
    private void Update()
    {
        Debug.Log($"Current player health is: {PlayerHealth.GetHealth()}");
    }
    */
    private void HandleEnemyDamageOutput_OnOutputDamage(int damageAmount)
    {
        Debug.Log($"Damage player for {damageAmount}");

        PlayerHealth.Damage(damageAmount);

        _currentHealth = PlayerHealth.GetHealth();
        CheckPlayerAlive();

        OnHealthChange?.Invoke(_currentHealth, _playerAlive);

        //Debug.Log($"Player health is: {_currentHealth}");
    }


    private void PlayerTriggerEnter_OnAreaDamagePlayer(int damageAmount)
    {
        Debug.Log($"Area damage for {damageAmount}");

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
    
    private void HandleGameStateUI_OnGameRestart(Vector3 respawnPosition)
    {
        PlayerHealth.ChangeHealth(_maxHealth);
        _currentHealth = PlayerHealth.GetHealth();
        CheckPlayerAlive();
        OnHealthChange?.Invoke(_currentHealth, _playerAlive);
        //Debug.Log($"Player health is: {_currentHealth}");
    }

}
