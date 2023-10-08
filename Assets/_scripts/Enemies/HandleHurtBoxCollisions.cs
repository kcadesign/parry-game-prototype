using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleHurtBoxCollisions : HandleCollisions, IParryable
{    
    public event Action<GameObject, bool> OnDeflect;
    public event Action<GameObject> OnDamageCollision;

    public GameObject HealthHandlerObject;

    private bool _parryActive;
    private bool _blockActive;
    private bool _deflected;

    private void OnEnable()
    {
        PlayerParry.OnParryActive += PlayerParry_OnParryActive;
        PlayerBlock.OnBlock += PlayerBlock_OnBlock;
    }

    private void OnDisable()
    {
        PlayerParry.OnParryActive -= PlayerParry_OnParryActive;
        PlayerBlock.OnBlock -= PlayerBlock_OnBlock;
    }

    private void PlayerParry_OnParryActive(bool parryPressed)
    {
        _parryActive = parryPressed;
    }

    private void PlayerBlock_OnBlock(bool isBlocking)
    {
        _blockActive = isBlocking;
    }

    protected override void HandleCollisionWithPlayer(GameObject collidedObject)
    {
        //Debug.Log($"{gameObject} collided with {collidedObject}");
        //Debug.Log($"Block active: {_blockActive}");
        if (!_deflected)
        {
            if (_parryActive)
            {
                // Damage enemy
                _deflected = true;
                OnDeflect?.Invoke(gameObject, _deflected);
                // damage enemy health component
                OnDamageCollision?.Invoke(HealthHandlerObject);
            }
            else if (_blockActive)
            {
                return;
            }
            else if (!_parryActive && !_blockActive)
            {
                // Damage player
                _deflected = false;
                OnDamageCollision?.Invoke(collidedObject);
            }
            _deflected = false;
        }

    }

}
