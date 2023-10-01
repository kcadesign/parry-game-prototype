using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleEnemyCollisions : HandleCollisions, IDamager, IDamageable
{
    public event Action<GameObject> OnDamageCollision;

    protected bool _parryActive;
    protected bool _blockActive;
    
    [HideInInspector] public bool EnemyHit = false;

    //[SerializeField] private float _damageForce = 5;

    protected void OnEnable()
    {
        PlayerParry.OnParryActive += PlayerParry_OnParryActive;
        PlayerBlock.OnBlock += PlayerBlockJump_OnBlock;
    }

    protected void OnDisable()
    {
        PlayerParry.OnParryActive -= PlayerParry_OnParryActive;
        PlayerBlock.OnBlock -= PlayerBlockJump_OnBlock;
    }

    protected void PlayerParry_OnParryActive(bool parryPressed)
    {
        _parryActive = parryPressed;
    }

    protected void PlayerBlockJump_OnBlock(bool isBlocking)
    {
        _blockActive = isBlocking;
    }

    protected override void HandleCollisionWithDamager(Collision2D collision)
    {
        base.HandleCollisionWithDamager(collision);
        if (collision.gameObject.CompareTag("Player"))
        {
            if (_parryActive && !_blockActive)
            {
                EnemyHit = true;
            }
        }
    }

    protected override void HandleCollisionWithDamageable(Collision2D collision)
    {
        base.HandleCollisionWithDamageable(collision);
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!_parryActive && !_blockActive)
            {
                EnemyHit = false;

                //HandleKnockBack(collision);
                OnDamageCollision?.Invoke(collision.gameObject);
            }
        }
    }
    /*
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!_parryActive && !_blockActive)
            {
                EnemyHit = false;

                //HandleKnockBack(collision);
                OnDamageCollision?.Invoke(collision.gameObject);
            }
            else if (!_parryActive && _blockActive)
            {
                EnemyHit = false;
            }
            else if (_parryActive && !_blockActive)
            {
                EnemyHit = true;
            }
        }
    }
    
    protected void HandleKnockBack(Collision2D collision)
    {
        // Determine the direction to apply force based on player's relative position to enemy
        float enemyCenterX = transform.position.x;
        float playerCenterX = collision.transform.position.x;
        float approachDirectionX = (playerCenterX - enemyCenterX);

        if (approachDirectionX > 0)
        {
            collision.rigidbody.AddForce(new Vector2(1, 1) * _damageForce, ForceMode2D.Impulse);
        }
        else if (approachDirectionX < 0)
        {
            collision.rigidbody.AddForce(new Vector2(-1, 1) * _damageForce, ForceMode2D.Impulse);
        }
    }*/
}
