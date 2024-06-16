using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HandlePlayerHealth : MonoBehaviour
{
    public static event Action<int> OnHealthInitialise;
    public delegate void PlayerHealthChange(int currentHealth);
    public static event PlayerHealthChange OnHealthChange;
    public static Action OnPlayerDead;
    public static event Action OnPlayerHealthReplenish;

    public static event Action<GameObject> OnDamageRecieved;
    public static event Action<bool> OnPlayerHurtBig;
    public static event Action<bool> OnPlayerHurtSmall;

    public HealthSystem PlayerHealth;

    [SerializeField] private int _maxHealth = 5;
    public int _currentHealth;
    //private bool _playerAlive = true;

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
            OnHealthChange?.Invoke(_currentHealth);
        }
    }

    private void Start()
    {
        OnHealthInitialise?.Invoke(_maxHealth);
    }

    private void OnEnable()
    {
        HandleDamageOut.OnOutputDamage += HandleDamageOutput_OnOutputDamage;
        PlayerTriggerEnter.OnAreaDamagePlayer += PlayerTriggerEnter_OnAreaDamagePlayer;
        //HandleGameStateUI.OnRestartButtonPressed += HandleGameStateUI_OnGameRestart;
        HandlePlayerStun.OnStunned += HandlePlayerStun_OnStunned;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        HandleDamageOut.OnOutputDamage -= HandleDamageOutput_OnOutputDamage;
        PlayerTriggerEnter.OnAreaDamagePlayer -= PlayerTriggerEnter_OnAreaDamagePlayer;
        //HandleGameStateUI.OnRestartButtonPressed -= HandleGameStateUI_OnGameRestart;
        HandlePlayerStun.OnStunned -= HandlePlayerStun_OnStunned;
        SceneManager.sceneLoaded -= OnSceneLoaded;

    }

    private void Update()
    {
        HandleAutoHealTimer();
    }

    private void HandleAutoHealTimer()
    {
        if (_isCounting)
        {
            _autoHealTimer += Time.deltaTime;
            if (_autoHealTimer >= _autoHealWaitTime && _currentHealth < _maxHealth)
            {
                PlayerHealth.ChangeHealth(_maxHealth);
                _currentHealth = PlayerHealth.GetHealth();
                OnHealthChange?.Invoke(_currentHealth);
                OnPlayerHealthReplenish?.Invoke();

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
        if (_canBeDamaged)
        {
            if (collisionObject == gameObject)
            {
                PlayerHealth.Damage(damageAmount);
                OnPlayerHurtBig?.Invoke(true);

                _currentHealth = PlayerHealth.GetHealth();
                Debug.Log($"Player health: {_currentHealth}");
                CheckPlayerAlive();

                ResetHealTimer();
                StartHealTimer();

                OnDamageRecieved?.Invoke(objectDamager);
                OnHealthChange?.Invoke(_currentHealth);
                
                OnPlayerHurtBig?.Invoke(false);
            }
        }
    }

    private void PlayerTriggerEnter_OnAreaDamagePlayer(int damageAmount)
    {
        if (_canBeDamaged)
        {
            Debug.Log($"Area damage for {damageAmount}");

            PlayerHealth.Damage(damageAmount);
            OnPlayerHurtSmall?.Invoke(true);

            _currentHealth = PlayerHealth.GetHealth();
            CheckPlayerAlive();

            ResetHealTimer();
            StartHealTimer();

            OnHealthChange?.Invoke(_currentHealth);

            OnPlayerHurtSmall?.Invoke(false);
        }
    }

    private void CheckPlayerAlive()
    {
        if (_currentHealth <= 0)
        {
            //_playerAlive = false;
            OnPlayerDead?.Invoke();
            //Debug.Log("Player is dead");
        }
/*        else
        {
            _playerAlive = true;
        }
*/    }

/*    private void HandleGameStateUI_OnGameRestart()
    {
        PlayerHealth.ChangeHealth(_maxHealth);
        _currentHealth = PlayerHealth.GetHealth();
        CheckPlayerAlive();
        OnHealthChange?.Invoke(_currentHealth);
    }
*/
    private void HandlePlayerStun_OnStunned(bool stunned)
    {
        //Debug.Log($"Player stunned: {stunned}");
        if (stunned)
        {
            _canBeDamaged = false;
        }
        else if (!stunned)
        {
            _canBeDamaged = true;
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayerHealth.ChangeHealth(_maxHealth);
        _currentHealth = PlayerHealth.GetHealth();
        CheckPlayerAlive();
        OnHealthChange?.Invoke(_currentHealth);
    }

    public void StartHealTimer() => _isCounting = true;
    public void StopHealTimer() => _isCounting = false;
    public void ResetHealTimer() => _autoHealTimer = 0f;

}