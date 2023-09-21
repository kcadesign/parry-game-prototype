using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandlePlayerMaterials : MonoBehaviour
{
    private Collider2D _playerCollider;
    public PhysicsMaterial2D DefaultPlayerMaterial;
    public PhysicsMaterial2D BouncyMaterial;

    private bool _parryActive;
    private bool _blockActive;
    private bool _stunned;

    private void Awake()
    {
        _playerCollider = GetComponent<Collider2D>();
    }

    private void OnEnable()
    {
        PlayerParry.OnParryActive += PlayerParry_OnParryActive;
        PlayerBlockJump.OnBlock += PlayerBlockJump_OnBlock;
        HandlePlayerCollisions.OnStunned += HandlePlayerCollisions_OnStunned;
    }

    private void OnDisable()
    {
        PlayerParry.OnParryActive -= PlayerParry_OnParryActive;
        PlayerBlockJump.OnBlock -= PlayerBlockJump_OnBlock;
        HandlePlayerCollisions.OnStunned -= HandlePlayerCollisions_OnStunned;
    }

    private void PlayerParry_OnParryActive(bool parryPressed)
    {
        _parryActive = parryPressed;
    }

    private void PlayerBlockJump_OnBlock(bool isBlocking)
    {
        _blockActive = isBlocking;
    }

    private void HandlePlayerCollisions_OnStunned(bool stunned)
    {
        //Debug.Log($"Player stunned: {stunned}");
        _stunned = stunned;
        if (_stunned)
        {
            _playerCollider.sharedMaterial = null;
        }
        else if(!_stunned)
        {
            _playerCollider.sharedMaterial = DefaultPlayerMaterial;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_parryActive && !_blockActive && !_stunned)
        {
            _playerCollider.sharedMaterial = BouncyMaterial;
        }
        else if (!_parryActive && (_blockActive || _stunned)) 
        {
            _playerCollider.sharedMaterial = null;
        }
        else if (!_parryActive && !_blockActive && !_stunned)
        {
            _playerCollider.sharedMaterial = DefaultPlayerMaterial;
        }
    }
}
