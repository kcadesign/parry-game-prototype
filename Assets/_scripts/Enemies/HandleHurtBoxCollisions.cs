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
    public bool Deflected = false;

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

    private void Update()
    {
        //Debug.Log($"Deflected: {Deflected}");

        // Instantly reset deflected bool
        if (Deflected) Deflected = false;
    }

    protected override void HandleCollisionWithPlayer(GameObject collidedObject)
    {
        if (!Deflected)
        {
            if (_parryActive)
            {
                Deflected = true;
                OnDeflect?.Invoke(gameObject, Deflected);

                // Damage enemy health component
                OnDamageCollision?.Invoke(HealthHandlerObject);
            }
            else if (_blockActive)
            {
                return;
            }
            else if (!_parryActive && !_blockActive)
            {
                // Damage player
                OnDamageCollision?.Invoke(collidedObject);
            }
        }
    }

}
