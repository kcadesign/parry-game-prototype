using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Unity.VisualScripting;
using UnityEngine;

public class JuiceSystems : MonoBehaviour
{
    private CinemachineImpulseSource _impulseSource;

    [SerializeField] private float _enemyDeathStopDuration = 0.1f;
    [SerializeField] private float _playerDamageStopDuration = 0.1f;

    [SerializeField] private float _screenShakeAmount = 1;

    private bool _playerDead = false;

    private void Awake()
    {
        _impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    private void OnEnable()
    {
        HandleEnemyHealth.OnEnemyDeath += HandleEnemyHealth_OnEnemyDeath;
        HandlePlayerHealth.OnPlayerHurtBig += HandlePlayerHealth_OnPlayerHurtBig;
        HandlePlayerHealth.OnPlayerDead += () => _playerDead = true;
        HandleBossHealth.OnBossHealthChange += HandleBossHealth_OnBossHealthChange;
        HandleBossHealth.OnBossDeath += HandleBossHealth_OnBossDeath;
    }

    private void OnDisable()
    {
        HandleEnemyHealth.OnEnemyDeath -= HandleEnemyHealth_OnEnemyDeath;
        HandlePlayerHealth.OnPlayerHurtBig -= HandlePlayerHealth_OnPlayerHurtBig;
        HandleBossHealth.OnBossHealthChange -= HandleBossHealth_OnBossHealthChange;
        HandleBossHealth.OnBossDeath -= HandleBossHealth_OnBossDeath;
    }

    private void HandleEnemyHealth_OnEnemyDeath(GameObject deadEnemy)
    {
        // when an enemy is destroyed, pause time briefly (hit stop)
        StartCoroutine(HitStop(_enemyDeathStopDuration));
        ScreenShake(_screenShakeAmount);
    }

    private void HandlePlayerHealth_OnPlayerHurtBig(bool stunned)
    {
        if (_playerDead)
        {
            return;
        }
        else
        {
            Debug.Log("Player stunned");
            // when the player is damaged, pause time briefly (hit stop)
            StartCoroutine(HitStop(_playerDamageStopDuration));
            ScreenShake(_screenShakeAmount);
        }
    }

    private void HandleBossHealth_OnBossHealthChange(int currentHealth, int maxHealth)
    {
        // when the boss is damaged, pause time briefly (hit stop)
        StartCoroutine(HitStop(_enemyDeathStopDuration));
        ScreenShake(_screenShakeAmount);
    }

    private void HandleBossHealth_OnBossDeath()
    {
        Debug.Log("Boss dead, shake screen");
        // shake screen violently when boss is destroyed
        StartCoroutine(MultiShake(3, 10, 0.3f));
    }

    private IEnumerator HitStop(float hitStopDuration)
    {
        //Debug.Log("Hit stop coroutine started");
        // pause time for hitStopDuration
        Time.timeScale = 0.4f;
        yield return new WaitForSecondsRealtime(hitStopDuration);
        // resume time
        Time.timeScale = 1f;
        //Debug.Log("Hit stop coroutine ended");
    }

    private void ScreenShake(float shakeAmount)
    {
        _impulseSource.GenerateImpulseWithForce(shakeAmount);
    }

    private IEnumerator MultiShake(float shakeAmount, int shakeCount, float interval)
    {
        for (int i = 0; i < shakeCount; i++)
        {
            ScreenShake(shakeAmount);
            yield return new WaitForSeconds(interval);
        }
    }
}
