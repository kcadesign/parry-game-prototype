using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleEnemyBodyCollisions : HandleCollisions, IParryable
{    
    public event Action<GameObject, bool> OnDeflect;
    public event Action<GameObject> OnDamageCollision;

    protected bool _parryActive;
    protected bool _blockActive;
    private bool _deflected;

    //[HideInInspector] public bool EnemyHit = false;
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

    protected override void HandleCollisionWithPlayer(GameObject collidedObject)
    {
        if (_parryActive)
        {
            //EnemyHit = true;
            OnDeflect?.Invoke(gameObject, _deflected);
        }
        else if (_blockActive)
        {
            return;
        }
        else if (!_parryActive && !_blockActive)
        {
            //EnemyHit = false;
            //Debug.Log($"Enemy collided with: {collidedObject}");
            OnDamageCollision?.Invoke(collidedObject);
        }
    }

    protected override void HandleCollisionWithProjectile(GameObject collidedObject)
    {
        Debug.Log($"Enemy collided with {collidedObject.tag}");

        // if deflected - damage enemy
        // else - ignore
    }

}
