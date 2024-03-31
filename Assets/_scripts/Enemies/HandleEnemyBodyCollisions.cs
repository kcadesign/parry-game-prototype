using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleEnemyBodyCollisions : HandleCollisions, IParryable
{
    public event Action<GameObject, bool> OnDeflect;
    public event Action<GameObject> OnDamageCollision;
    public static event Action OnProjectileHit;

    protected bool _parryActive;
    protected bool _blockActive;
    private bool _deflected;
    //public bool ProjectileHit = false;


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

    private void Update()
    {
/*        if (ProjectileHit)
        {
            Debug.Log("Reset projectile hit");
            ProjectileHit = false;
        }
*/    }

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
        if (collidedObject.GetComponent<HandleProjectileCollisions>().Deflected)
        {
            //ProjectileHit = true;

            Debug.Log(gameObject.transform.parent.name + " projectile hit");
            OnProjectileHit?.Invoke();
        }

    }
}
