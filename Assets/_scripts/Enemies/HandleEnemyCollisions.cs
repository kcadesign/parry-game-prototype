using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleEnemyCollisions : MonoBehaviour, IDealDamage
{
    public event Action<GameObject> OnDamage;

    protected bool _isParrying;
    protected bool _isBlocking;
    
    [HideInInspector] public bool EnemyHit = false;

    [SerializeField] private float _damageForce = 5;

    protected void OnEnable()
    {
        PlayerParry.OnParryActive += PlayerParry_OnParryActive;
        PlayerBlockJump.OnBlock += PlayerBlockJump_OnBlock;
    }

    protected void OnDisable()
    {
        PlayerParry.OnParryActive -= PlayerParry_OnParryActive;
        PlayerBlockJump.OnBlock -= PlayerBlockJump_OnBlock;
    }

    protected void PlayerParry_OnParryActive(bool parryPressed)
    {
        _isParrying = parryPressed;
    }

    private void PlayerBlockJump_OnBlock(bool isBlocking)
    {
        _isBlocking = isBlocking;
    }

    protected void Update()
    {
        EnemyHit = false;
        //Debug.Log($"Player is blocking: {_isBlocking}");

    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!_isParrying && !_isBlocking)
            {
                EnemyHit = false;

                HandleKnockBack(collision);
                OnDamage?.Invoke(collision.gameObject);
            }
            else if (!_isParrying && _isBlocking)
            {
                EnemyHit = false;
            }
            else if (_isParrying && !_isBlocking)
            {
                EnemyHit = true;
            }
        }
        //Debug.Log($"Damage condition met: {_damageConditionMet}");
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
    }
}
