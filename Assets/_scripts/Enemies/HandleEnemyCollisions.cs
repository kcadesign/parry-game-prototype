using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleEnemyCollisions : MonoBehaviour
{
    public delegate void DamagePlayer(bool damageConditionMet);
    public static event DamagePlayer OnDamagePlayer;

    protected bool _isParrying;
    protected bool _isBlocking;
    
    [HideInInspector] public bool EnemyHit = false;

    protected bool _damageConditionMet;
    [SerializeField] private float _damageForce = 5;

    protected void OnEnable()
    {
        PlayerParry.OnParryActive += PlayerParry_OnParryActive;
        PlayerBlock.OnBlock += PlayerBlock_OnBlock;
    }

    protected void OnDisable()
    {
        PlayerParry.OnParryActive -= PlayerParry_OnParryActive;
        PlayerBlock.OnBlock -= PlayerBlock_OnBlock;
    }

    protected void PlayerParry_OnParryActive(bool parryPressed)
    {
        _isParrying = parryPressed;
    }
    private void PlayerBlock_OnBlock(bool isBlocking)
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
        //Debug.Log($"Parrying on collision: {_isParrying}");
        //Debug.Log($"Blocking on collision: {_isBlocking}");

        if (collision.gameObject.CompareTag("Player") && !_isParrying && !_isBlocking)
        {
            EnemyHit = false;
            _damageConditionMet = true;

            HandleKnockBack(collision);
        }
        else if(collision.gameObject.CompareTag("Player") && !_isParrying && _isBlocking)
        {
            EnemyHit = false;
            _damageConditionMet = false;
        }
        else if (collision.gameObject.CompareTag("Player") && _isParrying && !_isBlocking)
        {
            EnemyHit = true;
            _damageConditionMet = false;
        }
        OnDamagePlayer?.Invoke(_damageConditionMet);
        Debug.Log($"Damage condition met: {_damageConditionMet}");

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
