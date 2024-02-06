using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleProjectileCollisions : HandleCollisions, IParryable
{
    public event Action<GameObject, bool> OnDeflect;
    public event Action<GameObject> OnDamageCollision;

    private SpriteRenderer _projectileSpriteRenderer;

    private bool _parryActive;
    private bool _blockActive;
    public bool Deflected = false;

    private void Awake()
    {
        _projectileSpriteRenderer = GetComponent<SpriteRenderer>();
    }
    
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

    private void PlayerParry_OnParryActive(bool parryPressed) => _parryActive = parryPressed;
    private void PlayerBlock_OnBlock(bool isBlocking) => _blockActive = isBlocking;

    protected override void HandleCollisionWithPlayer(GameObject collidedObject)
    {
        //Debug.Log($"Projectile collided with {collidedObject.tag}");
        if (!Deflected)
        {
            if (_parryActive && !_blockActive)
            {
                _projectileSpriteRenderer.color = Color.white;
                Deflected = true;
                OnDeflect?.Invoke(gameObject, Deflected);

                Destroy(gameObject, 3f);
            }
            else if (!_parryActive && _blockActive)
            {
                Destroy(gameObject);
            }
            else if (!_parryActive && !_blockActive)
            {
                Deflected = false;
                
                OnDamageCollision?.Invoke(collidedObject);
                Destroy(gameObject);
            }
        }

    }

    protected override void HandleCollisionWithEnemyBody(GameObject collidedObject)
    {
        if (!Deflected)
        {
            return;
        }
        else if (Deflected)
        {
            OnDamageCollision?.Invoke(collidedObject);
            Destroy(gameObject);
        }
    }
    /*
    protected override void HandleCollisionWithEnvironment(GameObject collidedObject)
    {
        Destroy(gameObject);
    }
    */
}
