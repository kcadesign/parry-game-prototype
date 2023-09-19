using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleEnemyDamageOutput : MonoBehaviour
{
    public delegate void OutputDamage(int damageAmount);
    public static event OutputDamage OnOutputDamage;

    [SerializeField] private int _playerDamageAmount = 5;

    private void OnEnable()
    {
        HandleEnemyCollisions.OnDamagePlayer += EnemyCollisionWithPlayer_OnDamagePlayer;
        HandleProjectileCollisions.OnProjectileDamagePlayer += HandleProjectileCollisions_OnProjectileDamagePlayer;
    }

    private void OnDisable()
    {
        HandleEnemyCollisions.OnDamagePlayer -= EnemyCollisionWithPlayer_OnDamagePlayer;
        HandleProjectileCollisions.OnProjectileDamagePlayer -= HandleProjectileCollisions_OnProjectileDamagePlayer;
    }

    private void EnemyCollisionWithPlayer_OnDamagePlayer()
    {
        SendDamage(_playerDamageAmount);
        //Debug.Log($"Send {_damageAmount} damage");
    }

    private void HandleProjectileCollisions_OnProjectileDamagePlayer()
    {
        SendDamage(_playerDamageAmount);
    }

    private void SendDamage(int damageAmount) => OnOutputDamage?.Invoke(damageAmount);
}