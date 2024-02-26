using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatePlayerLanded : MonoBehaviour
{
    private Animator _playerAnimator;
    private bool _grounded;
    private bool _isGrounded = false;

    private void Awake()
    {
        _playerAnimator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        CheckPlayerGrounded.OnGrounded += CheckPlayerGrounded_OnGrounded;
    }

    private void OnDisable()
    {
        CheckPlayerGrounded.OnGrounded -= CheckPlayerGrounded_OnGrounded;
    }

    private void CheckPlayerGrounded_OnGrounded(bool grounded)
    {
        _grounded = grounded;
        if (_grounded && !_isGrounded)
        {
            _playerAnimator.SetTrigger("Landed");
            _isGrounded = true;
        }
        else if (!_grounded && _isGrounded)
        {
            _isGrounded = false;
        }
    }

}
