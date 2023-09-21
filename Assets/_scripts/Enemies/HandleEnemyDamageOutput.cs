using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleEnemyDamageOutput : MonoBehaviour
{
    public delegate void OutputDamage(int damageAmount);
    public static event OutputDamage OnOutputDamage;

    [SerializeField] private int _playerDamageAmount = 5;

    private IEnemyCollisionHandler _collisionHandler;

    private void Awake()
    {
        // Try to find the appropriate collision handler component at runtime
        _collisionHandler = GetComponent<IEnemyCollisionHandler>();

        if (_collisionHandler == null)
        {
            Debug.LogWarning("No collision handler component found on " + gameObject.name);
        }
    }

    private void OnEnable()
    {
        if (_collisionHandler != null)
        {
            _collisionHandler.OnDamagePlayer += HandleCollisionWithPlayer_OnDamagePlayer;
        }
    }

    private void OnDisable()
    {
        if (_collisionHandler != null)
        {
            _collisionHandler.OnDamagePlayer -= HandleCollisionWithPlayer_OnDamagePlayer;
        }
    }

    private void HandleCollisionWithPlayer_OnDamagePlayer()
    {
        SendDamage(_playerDamageAmount);
        Debug.Log($"{_playerDamageAmount} damage sent to player from {gameObject.name}");
    }

    private void SendDamage(int damageAmount) => OnOutputDamage?.Invoke(damageAmount);
}
