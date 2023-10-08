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
    private bool _deflected = false;

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
        if (!_deflected)
        {
            if (_parryActive && !_blockActive)
            {
                _projectileSpriteRenderer.color = Color.white;
                _deflected = true;
                OnDeflect?.Invoke(gameObject, _deflected);

                Destroy(gameObject, 3f);
            }
            else if (!_parryActive && _blockActive)
            {
                Destroy(gameObject);
            }
            else if (!_parryActive && !_blockActive)
            {
                _deflected = false;
                
                OnDamageCollision?.Invoke(collidedObject);
                Destroy(gameObject);
            }
        }

    }

    protected override void HandleCollisionWithEnemyBody(GameObject collidedObject)
    {
        if (!_deflected)
        {
            return;
        }
        else if (_deflected)
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
