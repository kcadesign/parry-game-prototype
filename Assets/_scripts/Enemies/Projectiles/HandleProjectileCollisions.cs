using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleProjectileCollisions : HandleCollisions, IParryable
{
    public event Action<GameObject, bool> OnDeflect;
    public event Action<GameObject> OnDamageCollision;

    private SpriteRenderer _projectileSpriteRenderer;
    private CycleColors _cycleColorsComponent;

    private bool _parryActive;
    public bool Deflected = false;

    private void Awake()
    {
        _projectileSpriteRenderer = GetComponent<SpriteRenderer>();
        _cycleColorsComponent = GetComponent<CycleColors>();
    }
    
    protected void OnEnable()
    {
        PlayerParry.OnParryActive += PlayerParry_OnParryActive;
    }

    protected void OnDisable()
    {
        PlayerParry.OnParryActive -= PlayerParry_OnParryActive;
    }

    private void PlayerParry_OnParryActive(bool parryPressed) => _parryActive = parryPressed;

    protected override void HandleCollisionWithPlayer(GameObject collidedObject)
    {
        //Debug.Log($"Projectile collided with {collidedObject.tag}");
        if (!Deflected)
        {
            if (_parryActive)
            {
                _cycleColorsComponent.enabled = false;
                _projectileSpriteRenderer.color = Color.white;
                Deflected = true;
                OnDeflect?.Invoke(gameObject, Deflected);

                Destroy(gameObject, 3f);
            }
            else if (!_parryActive)
            {
                _cycleColorsComponent.enabled = true;
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
