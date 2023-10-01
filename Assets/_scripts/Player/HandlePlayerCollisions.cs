using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandlePlayerCollisions : HandleCollisions, IDamager, IDamageable
{
    public event Action<GameObject> OnDamageCollision;

    public delegate void Stunned(bool stunned);
    public static event Stunned OnStunned;

    private Rigidbody2D _rigidBody;

    private bool _parryActive = false;
    private bool _blockActive = false;

    private bool _playerStunned = false;

    private float _originalMass;
    private float _originalLinearDrag;
    private float _originalGravity;

    [SerializeField] private float _hitStunMultiplier = 2;
    [SerializeField] private float _hitStunDuration = 3;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _originalMass = _rigidBody.mass;
        _originalLinearDrag = _rigidBody.drag;
        _originalGravity = _rigidBody.gravityScale;
    }

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

    private void PlayerParry_OnParryActive(bool parryPressed)
    {
        _parryActive = parryPressed;
    }

    private void PlayerBlockJump_OnBlock(bool isBlocking)
    {
        _blockActive = isBlocking;
    }
    /*
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && !_isParrying && !_isBlocking && !_playerStunned)
        {
            //Debug.Log($"Player hurt by {collision.gameObject.tag}");

            StartCoroutine(StunActions());
        }
        
        else if (collision.gameObject.CompareTag("Enemy") && _isParrying)
        {
            OnDamageCollision?.Invoke(collision.gameObject);
        }
    }*/

    protected override void HandleCollisionWithDamager(Collision2D collision)
    {
        //Debug.Log(collision);
        base.HandleCollisionWithDamager(collision);
        if (!_parryActive && !_blockActive && !_playerStunned)
        {
            StartCoroutine(StunActions());
        }
    }

    protected override void HandleCollisionWithDamageable(Collision2D collision)
    {
        //Debug.Log(collision);
        base.HandleCollisionWithDamageable(collision);
        if (_parryActive)
        {
            OnDamageCollision?.Invoke(collision.gameObject);
        }
    }

    protected override void HandleCollisionWithStandard(Collision2D collision)
    {
        //Debug.Log(collision);
        base.HandleCollisionWithStandard(collision);
    }
    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnvironmentHazard"))
        {
            //Debug.Log($"Player hurt by {collision.gameObject.tag}");
        }
    }
    */
    private IEnumerator StunActions()
    {
        _playerStunned = true;
        OnStunned?.Invoke(_playerStunned);

        _rigidBody.mass *= _hitStunMultiplier;
        _rigidBody.drag *= _hitStunMultiplier;
        _rigidBody.gravityScale *= _hitStunMultiplier;

        yield return new WaitForSeconds(_hitStunDuration);

        _playerStunned = false;
        OnStunned?.Invoke(_playerStunned);

        _rigidBody.mass = _originalMass;
        _rigidBody.drag = _originalLinearDrag;
        _rigidBody.gravityScale = _originalGravity;
    }
}
