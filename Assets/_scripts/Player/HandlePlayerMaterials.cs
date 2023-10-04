using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandlePlayerMaterials : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private Collider2D _playerCollider;
    public PhysicsMaterial2D DefaultPlayerMaterial;
    public PhysicsMaterial2D BouncyMaterial;

    private bool _parryActive;
    private bool _blockActive;
    private bool _stunned;
    private bool _grounded;

    private bool _forceApplied;
    //[SerializeField] private float _reboundForce = 5;
    private bool _isBouncing;
    //private int _bounceCount = 0;

    private void Awake()
    {
        _playerCollider = GetComponent<Collider2D>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        PlayerParry.OnParryActive += PlayerParry_OnParryActive;
        PlayerBlock.OnBlock += PlayerBlockJump_OnBlock;
        HandlePlayerCollisions.OnStunned += HandlePlayerCollisions_OnStunned;
        CheckPlayerGrounded.OnGrounded += CheckPlayerGrounded_OnGrounded;
    }


    private void OnDisable()
    {
        PlayerParry.OnParryActive -= PlayerParry_OnParryActive;
        PlayerBlock.OnBlock -= PlayerBlockJump_OnBlock;
        HandlePlayerCollisions.OnStunned -= HandlePlayerCollisions_OnStunned;
        CheckPlayerGrounded.OnGrounded -= CheckPlayerGrounded_OnGrounded;
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
        else if (!_stunned)
        {
            _playerCollider.sharedMaterial = DefaultPlayerMaterial;
        }
    }

    private void CheckPlayerGrounded_OnGrounded(bool grounded)
    {
        _grounded = grounded;

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
