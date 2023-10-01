using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleEnemyCollisions : MonoBehaviour, IDamager, IDamageable
{
    public event Action<GameObject> OnDamageCollision;

    protected bool _parryActive;
    protected bool _blockActive;
    //private bool _deflected;
    
    [HideInInspector] public bool EnemyHit = false;

    [SerializeField] private float _damageForce = 5;

    protected void OnEnable()
    {
        PlayerParry.OnParryActive += PlayerParry_OnParryActive;
        PlayerBlock.OnBlock += PlayerBlockJump_OnBlock;
        //HandleProjectileCollisions.OnDeflect += HandleProjectileCollisions_OnDeflect;
        //HandleDamageOutput.OnOutputDamage += HandleDamageOutput_OnOutputDamage;
    }

    protected void OnDisable()
    {
        PlayerParry.OnParryActive -= PlayerParry_OnParryActive;
        PlayerBlock.OnBlock -= PlayerBlockJump_OnBlock;
        //HandleProjectileCollisions.OnDeflect -= HandleProjectileCollisions_OnDeflect;
        //HandleDamageOutput.OnOutputDamage -= HandleDamageOutput_OnOutputDamage;
    }

    protected void PlayerParry_OnParryActive(bool parryPressed)
    {
        _parryActive = parryPressed;
    }

    protected void PlayerBlockJump_OnBlock(bool isBlocking)
    {
        _blockActive = isBlocking;
    }
    /*
    protected void HandleProjectileCollisions_OnDeflect(GameObject projectile, bool deflected)
    {
        Debug.Log($"Projectile is deflcted: {deflected}");
        _deflected = deflected;
    }
    
    private void HandleDamageOutput_OnOutputDamage(GameObject collisionObject, int damageAmount)
    {
        if (collisionObject == gameObject)
        {
            if (_deflected)
            {
                EnemyHit = true;
            }
            else
            {
                EnemyHit = false;
            }
        }
    }

    
    protected void Update()
    {
        EnemyHit = false;
    }
    */
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!_parryActive && !_blockActive)
            {
                EnemyHit = false;

                HandleKnockBack(collision);
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
        /*
        if (collision.gameObject.CompareTag("Projectile"))
        {
            if (_deflected)
            {
                EnemyHit = true;
            }
            else
            {
                EnemyHit = false;
            }
        }*/
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
