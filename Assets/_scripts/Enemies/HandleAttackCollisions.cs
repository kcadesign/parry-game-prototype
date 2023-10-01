using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleAttackCollisions : HandleCollisions, IDamager
{
    public event Action<GameObject, bool> OnDeflect;
    public event Action<GameObject> OnDamageCollision;

    //public HandleEnemyHealth EnemyHealth;

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

    protected override void HandleCollisionWithDamager(Collision2D collision)
    {
        base.HandleCollisionWithDamager(collision);
        if (collision.gameObject.CompareTag("Player"))
        {
            if (_parryActive)
            {
                _attackDeflected = true;
                OnDeflect?.Invoke(collision.gameObject, _attackDeflected);
            }
        }
    }

    protected override void HandleCollisionWithDamageable(Collision2D collision)
    {
        base.HandleCollisionWithDamageable(collision);
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!_parryActive)
            {
                _attackDeflected = false;
                OnDamageCollision?.Invoke(collision.gameObject);
            }
        }
    }
}
