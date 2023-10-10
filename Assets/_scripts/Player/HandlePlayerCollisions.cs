using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandlePlayerCollisions : HandleCollisions, IParryable
{    
    public event Action<GameObject, bool> OnDeflect;
    public event Action<GameObject> OnDamageCollision;

    public static event Action OnCollision;

    public delegate void Stunned(bool stunned);
    public static event Stunned OnStunned;

    private Rigidbody2D _rigidbody;

    private bool _parryActive = false;
    private bool _blockActive = false;

    private bool _playerStunned = false;

    private float _originalMass;
    private float _originalLinearDrag;
    private float _originalGravity;

    [SerializeField] private float _hitStunMultiplier = 2;
    [SerializeField] private float _hitStunDuration = 3;

    [SerializeField] private float _knockbackForce = 30;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _originalMass = _rigidbody.mass;
        _originalLinearDrag = _rigidbody.drag;
        _originalGravity = _rigidbody.gravityScale;
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

    private void PlayerParry_OnParryActive(bool parryPressed) => _parryActive = parryPressed;
    private void PlayerBlockJump_OnBlock(bool isBlocking) => _blockActive = isBlocking;

    protected override void HandleCollisionWithEnemyBody(GameObject collidedObject)
    {
        //Debug.Log($"Enemy correctly collided with player");

        if (!_parryActive && !_blockActive && !_playerStunned)
        {
            VulnerableCollisionActions(collidedObject);
        }
        OnCollision?.Invoke();
    }

    protected override void HandleCollisionWithProjectile(GameObject collidedObject)
    {
        if (!_parryActive && !_blockActive && !_playerStunned)
        {
            VulnerableCollisionActions(collidedObject);
        }
        OnCollision?.Invoke();

    }

    protected override void HandleCollisionWithHurtBox(GameObject collidedObject)
    {
        if (!_parryActive && !_blockActive && !_playerStunned)
        {
            VulnerableCollisionActions(collidedObject);
        }
        OnCollision?.Invoke();

    }

    private void VulnerableCollisionActions(GameObject collidedObject)
    {
        HandleKnockBack(collidedObject);
        StartCoroutine(StunActions());
    }

    private IEnumerator StunActions()
    {
        _playerStunned = true;
        OnStunned?.Invoke(_playerStunned);

        _rigidbody.mass *= _hitStunMultiplier;
        _rigidbody.drag *= _hitStunMultiplier;
        _rigidbody.gravityScale *= _hitStunMultiplier;

        yield return new WaitForSeconds(_hitStunDuration);

        _playerStunned = false;
        OnStunned?.Invoke(_playerStunned);

        _rigidbody.mass = _originalMass;
        _rigidbody.drag = _originalLinearDrag;
        _rigidbody.gravityScale = _originalGravity;
    }

    protected void HandleKnockBack(GameObject collision)
    {
        // Determine the direction to apply force based on player's relative position to enemy
        float enemyCenterX = collision.transform.position.x;
        float playerCenterX = transform.position.x;
        float approachDirectionX = (playerCenterX - enemyCenterX);

        if (approachDirectionX > 0)
        {
            _rigidbody.velocity = new(_rigidbody.velocity.x, 0);
            _rigidbody.AddForce(new Vector2(1, 1) * _knockbackForce, ForceMode2D.Impulse);
        }
        else if (approachDirectionX < 0)
        {
            _rigidbody.velocity = new(_rigidbody.velocity.x, 0);
            _rigidbody.AddForce(new Vector2(-1, 1) * _knockbackForce, ForceMode2D.Impulse);
        }
    }

}
