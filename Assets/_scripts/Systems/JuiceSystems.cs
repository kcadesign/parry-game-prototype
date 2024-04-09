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

    private void Awake()
    {
        _impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    private void OnEnable()
    {
        HandleEnemyHealth.OnEnemyDeath += HandleEnemyHealth_OnEnemyDeath;
        HandlePlayerHealth.OnPlayerHurtBig += HandlePlayerHealth_OnPlayerHurtBig;
    }

    private void OnDisable()
    {
        HandleEnemyHealth.OnEnemyDeath -= HandleEnemyHealth_OnEnemyDeath;
        HandlePlayerHealth.OnPlayerHurtBig -= HandlePlayerHealth_OnPlayerHurtBig;
    }

    private void HandleEnemyHealth_OnEnemyDeath(GameObject deadEnemy)
    {
        // when an enemy is destroyed, pause time briefly (hit stop)
        StartCoroutine(HitStop(_enemyDeathStopDuration));
        ScreenShake(_screenShakeAmount);
    }

    private void HandlePlayerHealth_OnPlayerHurtBig(bool stunned)
    {
        Debug.Log("Player stunned");
        // when the player is damaged, pause time briefly (hit stop)
        StartCoroutine(HitStop(_playerDamageStopDuration));
        ScreenShake(_screenShakeAmount);
    }


    private IEnumerator HitStop(float hitStopDuration)
    {
        // pause time for hitStopDuration
        Time.timeScale = 0.4f;
        yield return new WaitForSecondsRealtime(hitStopDuration);
        // resume time
            Time.timeScale = 1f;
    }

    private void ScreenShake(float shakeAmount)
    {
        _impulseSource.GenerateImpulseWithForce(shakeAmount);
    }
}
