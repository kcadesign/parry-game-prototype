using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandlePlayerCollisions : MonoBehaviour
{
    // Types of collision
    // - no parry with enemy
    // - parry with enemy
    // - no parry with attack
    // - parry with attack
    // - no parry with bounce pad
    // - parry with bounce pad

    public delegate void Stunned(bool stunned);
    public static event Stunned OnStunned;

    public delegate void DamageEnemy(GameObject collisionObject, int damageAmount);
    public static event DamageEnemy OnDamageEnemy;

    [SerializeField] private int _damageAmount = 1;

    private Rigidbody2D _rigidBody;

    private bool _isParrying = false;
    private bool _isBlocking = false;

    private bool _playerStunned = false;

    private float _originalMass;
    //[SerializeField] private float _hitStunMass = 10;

    private float _originalLinearDrag;
    //[SerializeField] private float _hitStunLinearDrag = 5;

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
        PlayerBlock.OnBlock += PlayerBlock_OnBlock;
    }

    private void OnDisable()
    {
        PlayerParry.OnParryActive -= PlayerParry_OnParryActive;
        PlayerBlock.OnBlock -= PlayerBlock_OnBlock;
    }

    private void PlayerParry_OnParryActive(bool parryPressed)
    {
        _isParrying = parryPressed;
    }

    private void PlayerBlock_OnBlock(bool isBlocking)
    {
        _isBlocking = isBlocking;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && !_isParrying && !_isBlocking && !_playerStunned)
        {
            Debug.Log($"Player hurt by {collision.gameObject.tag}");

            StartCoroutine(StunActions());
        }
        else if (collision.gameObject.CompareTag("Enemy") && _isParrying)
        {
            OnDamageEnemy?.Invoke(collision.gameObject, _damageAmount);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnvironmentHazard"))
        {
            Debug.Log($"Player hurt by {collision.gameObject.tag}");
        }
    }

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
