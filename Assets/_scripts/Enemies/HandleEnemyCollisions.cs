using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleEnemyCollisions : MonoBehaviour
{
    public delegate void DamagePlayer(bool damageConditionMet);
    public static event DamagePlayer OnDamagePlayer;

    protected bool _isParrying;
    [HideInInspector] public bool EnemyHit = false;

    protected bool _damageConditionMet;
    [SerializeField] private float _damageForce = 5;

    protected void OnEnable()
    {
        PlayerParry.OnParryActive += PlayerParry_OnParryActive;
    }

    protected void OnDisable()
    {
        PlayerParry.OnParryActive -= PlayerParry_OnParryActive;
    }

    protected void PlayerParry_OnParryActive(bool parryPressed)
    {
        _isParrying = parryPressed;
    }

    protected void Update()
    {
        EnemyHit = false;
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !_isParrying)
        {
            _damageConditionMet = true;
            OnDamagePlayer?.Invoke(_damageConditionMet);

            HandleKnockBack(collision);
        }
        else if (collision.gameObject.CompareTag("Player") && _isParrying)
        {
            EnemyHit = true;
            _damageConditionMet = false;
            OnDamagePlayer?.Invoke(_damageConditionMet);
        }
    }

    protected void HandleKnockBack(Collision2D collision)
    {
        float enemyCenterX = transform.position.x;
        float playerCenterX = collision.transform.position.x;

        // Determine the direction to apply force based on player's relative position to enemy
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
