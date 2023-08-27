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

    private Rigidbody2D _rigidBody;

    private bool _isParrying = false;
    private bool _isBlocking = false;

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
        if(collision.gameObject.CompareTag("Enemy") && !_isParrying && !_isBlocking)
        {
            print("OUCH!!");
            StartCoroutine(SlowMovement());
        }
    }

    private IEnumerator SlowMovement()
    {
        _rigidBody.mass *= _hitStunMultiplier;
        _rigidBody.drag *= _hitStunMultiplier;
        _rigidBody.gravityScale *= _hitStunMultiplier;

        yield return new WaitForSeconds(_hitStunDuration);

        _rigidBody.mass = _originalMass;
        _rigidBody.drag = _originalLinearDrag;
        _rigidBody.gravityScale = _originalGravity;
    }
}
