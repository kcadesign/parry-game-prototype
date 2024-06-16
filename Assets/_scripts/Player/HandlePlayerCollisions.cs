using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandlePlayerCollisions : HandleCollisions, IParryable
{    
    public event Action<GameObject, bool> OnDeflect;
    public event Action<GameObject> OnDamageCollision;

    public static event Action<GameObject> OnCollision;

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
