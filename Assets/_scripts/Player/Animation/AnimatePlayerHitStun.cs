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
        HandlePlayerStun.OnStunned += HandlePlayerStun_OnStunned;
    }

    private void OnDisable()
    {
        HandlePlayerStun.OnStunned -= HandlePlayerStun_OnStunned;
    }

    private void HandlePlayerStun_OnStunned(bool stunned)
    {
        if (stunned)
        {
            _playerAnimator.SetBool("Stunned", true);
            _playerAnimator.SetTrigger("StunnedTrigger");
        }
        else if (!stunned)
        {
            _playerAnimator.SetBool("Stunned", false);
            _playerAnimator.ResetTrigger("StunnedTrigger");
        }
    }

}
