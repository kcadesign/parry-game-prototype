using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleAttackCollisions : MonoBehaviour, IParryable
{
    public event Action<GameObject, bool> OnDeflect;

    public HandleEnemyHealth EnemyHealth;

    private bool _parryActive;
    private bool _attackDeflected;

    private void OnEnable()
    {
        PlayerParry.OnParryActive += PlayerParry_OnParryActive;
    }

    private void OnDisable()
    {
        PlayerParry.OnParryActive -= PlayerParry_OnParryActive;
    }

    private void PlayerParry_OnParryActive(bool parryPressed)
    {
        _parryActive = parryPressed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_parryActive)
        {
            // Attack deflected
            _attackDeflected = true;
            OnDeflect?.Invoke(collision.gameObject, _attackDeflected);
        }
    }
}
