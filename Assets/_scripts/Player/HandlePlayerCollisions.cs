using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandlePlayerCollisions : HandleCollisions, IParryable
{    
    public event Action<GameObject, bool> OnDeflect;
    public event Action<GameObject> OnDamageCollision;

    public static event Action<GameObject> OnCollision;

    private bool _parryActive = false;
    private bool _blockActive = false;

    private void OnEnable()
    {
        PlayerParry.OnParryActive += PlayerParry_OnParryActive;
        PlayerBlock.OnBlock += PlayerBlockJump_OnBlock;
    }

    private void OnDisable()
    {
        PlayerParry.OnParryActive -= PlayerParry_OnParryActive;
        PlayerBlock.OnBlock -= PlayerBlockJump_OnBlock;
    }

    private void PlayerParry_OnParryActive(bool parryPressed) => _parryActive = parryPressed;
    private void PlayerBlockJump_OnBlock(bool isBlocking) => _blockActive = isBlocking;

    protected override void HandleCollisionWithEnemyBody(GameObject collidedObject)
    {
        OnCollision?.Invoke(collidedObject);
    }

    protected override void HandleCollisionWithProjectile(GameObject collidedObject)
    {
        OnCollision?.Invoke(collidedObject);
    }

    protected override void HandleCollisionWithHurtBox(GameObject collidedObject)
    {
        OnCollision?.Invoke(collidedObject);
    }
    
}
