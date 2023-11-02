using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandlePlayerStun : MonoBehaviour
{
    public delegate void Stunned(bool stunned);
    public static event Stunned OnStunned;

    private Rigidbody2D _rigidbody;
    
    private float _originalMass;
    private float _originalLinearDrag;
    private float _originalGravity;
    
    private bool _playerStunned;

    [SerializeField] private float _hitStunMultiplier = 2;
    [SerializeField] private float _hitStunDuration = 3;

    [SerializeField] private float _knockbackForce = 30;

    //private GameObject _collidedObject;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _originalMass = _rigidbody.mass;
        _originalLinearDrag = _rigidbody.drag;
        _originalGravity = _rigidbody.gravityScale;
    }

    private void OnEnable()
    {
        HandlePlayerHealth.OnDamageRecieved += HandlePlayerHealth_OnDamageRecieved;
    }

    private void OnDisable()
    {
        HandlePlayerHealth.OnDamageRecieved -= HandlePlayerHealth_OnDamageRecieved;
    }

    private void HandlePlayerHealth_OnDamageRecieved(GameObject objectDamager)
    {
        //Debug.Log($"DAMAGED");
        VulnerableCollisionActions(objectDamager);
    }

    private void VulnerableCollisionActions(GameObject collidedObject)
    {
        //Debug.Log($"Collided object: {collidedObject.tag}");
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
        //Debug.Log($"Handle knockback against {collision.tag}");

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
