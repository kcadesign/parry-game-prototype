using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatePlayerJump : MonoBehaviour
{
    private Animator _playerAnimator;

    private bool _grounded;

    private void Awake()
    {
        _playerAnimator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        PlayerJump.OnJump += PlayerJump_OnJump;
        CheckPlayerGrounded.OnGrounded += CheckPlayerGrounded_OnGrounded;

    }

    private void OnDisable()
    {
        PlayerJump.OnJump -= PlayerJump_OnJump;
        CheckPlayerGrounded.OnGrounded -= CheckPlayerGrounded_OnGrounded;

    }

    private void CheckPlayerGrounded_OnGrounded(bool grounded)
    {
        _grounded = grounded;
    }

    private void PlayerJump_OnJump(bool jumping)
    {
        if (_grounded)
        {
            _playerAnimator.SetTrigger("Jumping");
        }
/*        if (jumping)
        {
            _playerAnimator.SetTrigger("Jumping");
        }
*/    }

}
