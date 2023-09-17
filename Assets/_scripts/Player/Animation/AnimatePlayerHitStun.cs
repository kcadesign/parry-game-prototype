using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatePlayerHitStun : MonoBehaviour
{
    private Animator _playerAnimator;

    private void Awake()
    {
        _playerAnimator = GetComponent<Animator>();
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
        _playerAnimator.SetBool("Stunned", stunned);
    }

}
