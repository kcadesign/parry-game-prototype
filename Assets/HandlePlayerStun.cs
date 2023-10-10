using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandlePlayerStun : MonoBehaviour
{/*
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
            _rigidbody.AddForce(new Vector2(1, 1) * _knockbackForce, ForceMode2D.Impulse);
        }
        else if (approachDirectionX < 0)
        {
            _rigidbody.AddForce(new Vector2(-1, 1) * _knockbackForce, ForceMode2D.Impulse);
        }
    }
    */
}
