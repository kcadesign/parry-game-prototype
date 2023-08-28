using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    private PlayerParry _parryScript;
    private PlayerBlock _blockScript;
    private PlayerJump _jumpScript;

    private void Awake()
    {
        _parryScript = GetComponent<PlayerParry>();
        _blockScript = GetComponent<PlayerBlock>();
        _jumpScript = GetComponent<PlayerJump>();
    }

    private void OnEnable()
    {
        HandlePlayerCollisions.OnStunned += HandlePlayerCollisions_OnStunned;
    }

    private void OnDisable()
    {
        HandlePlayerCollisions.OnStunned -= HandlePlayerCollisions_OnStunned;
    }

    private void HandlePlayerCollisions_OnStunned(bool stunned)
    {
        if (stunned)
        {
            _parryScript.enabled = false;
            _blockScript.enabled = false;
            _jumpScript.enabled = false;
        }
        else
        {
            _parryScript.enabled = true;
            _blockScript.enabled = true;
            _jumpScript.enabled = true;

        }
    }
}
