using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandlePlayerHealth : MonoBehaviour
{
    public delegate void PlayerHealthChange(int currentHealth, bool playerAlive);
    public static event PlayerHealthChange OnHealthChange;

    public static event Action<GameObject> OnDamageRecieved;

    public HealthSystem PlayerHealth;

    [SerializeField] private int _maxHealth = 5;
    private int _currentHealth;
    private bool _playerAlive = true;

    private float _autoHealTimer;
    [SerializeField] private float _autoHealWaitTime = 5f;
    private bool _isCounting = false;

    private bool _canBeDamaged = true;

    private void Awake()
    {
        if (PlayerHealth == null)
        {
            PlayerHealth = new HealthSystem(_maxHealth);
            _currentHealth = PlayerHealth.GetHealth();
            OnHealthChange?.Invoke(_currentHealth, _playerAlive);
        }
    }

    private void OnEnable()
    {
        HandleDamageOut.OnOutputDamage += HandleDamageOutput_OnOutputDamage;
        PlayerTriggerEnter.OnAreaDamagePlayer += PlayerTriggerEnter_OnAreaDamagePlayer;
        HandleGameStateUI.OnGameRestart += HandleGameStateUI_OnGameRestart;
        HandlePlayerStun.OnStunned += HandlePlayerStun_OnStunned;
    }

    private void OnDisable()
    {
        HandleDamageOut.OnOutputDamage -= HandleDamageOutput_OnOutputDamage;
        PlayerTriggerEnter.OnAreaDamagePlayer -= PlayerTriggerEnter_OnAreaDamagePlayer;
        HandleGameStateUI.OnGameRestart -= HandleGameStateUI_OnGameRestart;
        HandlePlayerStun.OnStunned -= HandlePlayerStun_OnStunned;

    }

    private void Update()
    {
        if (_isCounting)
        {
            _autoHealTimer += Time.deltaTime;
            if (_autoHealTimer >= _autoHealWaitTime)
            {
                PlayerHealth.ChangeHealth(_maxHealth);
                _currentHealth = PlayerHealth.GetHealth();
                OnHealthChange?.Invoke(_currentHealth, _playerAlive);

                StopHealTimer();
                ResetHealTimer();
            }
        }
        else if (!_isCounting)
        {
            ResetHealTimer();
        }
    }

    private void HandleDamageOutput_OnOutputDamage(GameObject objectDamager, GameObject collisionObject, int damageAmount)
    {
        Debug.Log($"Damage recieved by player");
        Debug.Log($"SUBSCRIBER - Can be damaged: {_canBeDamaged}");

        if (_canBeDamaged)
        {
            if (collisionObject == gameObject)
            {
                PlayerHealth.Damage(damageAmount);

                _currentHealth = PlayerHealth.GetHealth();
                CheckPlayerAlive();

                ResetHealTimer();
                StartHealTimer();

                OnDamageRecieved?.Invoke(objectDamager);
                OnHealthChange?.Invoke(_currentHealth, _playerAlive);
                
            }
        }
    }

    private void PlayerTriggerEnter_OnAreaDamagePlayer(int damageAmount)
    {
        if (_canBeDamaged)
        {
            Debug.Log($"Area damage for {damageAmount}");

            PlayerHealth.Damage(damageAmount);

            _currentHealth = PlayerHealth.GetHealth();
            CheckPlayerAlive();

            ResetHealTimer();
            StartHealTimer();

            OnHealthChange?.Invoke(_currentHealth, _playerAlive);
        }
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
    }

    private void HandlePlayerStun_OnStunned(bool stunned)
    {
        Debug.Log($"Player stunned: {stunned}");
        if (stunned)
        {
            _canBeDamaged = false;
        }
        else if (!stunned)
        {
            _canBeDamaged = true;
        }
    }


    public void StartHealTimer() => _isCounting = true;
    public void StopHealTimer() => _isCounting = false;
    public void ResetHealTimer() => _autoHealTimer = 0f;

}