using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJump : MonoBehaviour
{
    protected PlayerControls playerControls;
    private Rigidbody2D _rigidBody;

    [SerializeField] private float _jumpPower = 5;

    private bool _canJump = false;
    [SerializeField] private bool _restrictJumpCount;
    private bool _isGrounded;

    private void Awake()
    {
        playerControls = new PlayerControls();
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        playerControls.Gameplay.Enable();

        playerControls.Gameplay.Jump.performed += Jump_performed;
        playerControls.Gameplay.Jump.canceled += Jump_canceled;

        PlayerBlock.OnBlock += PlayerBlock_OnBlock;
        CheckPlayerGrounded.OnGrounded += CheckPlayerGrounded_OnGrounded;
    }

    private void OnDisable()
    {
        playerControls.Gameplay.Disable();

        playerControls.Gameplay.Jump.performed -= Jump_performed;
        playerControls.Gameplay.Jump.canceled -= Jump_canceled;

        PlayerBlock.OnBlock -= PlayerBlock_OnBlock;
        CheckPlayerGrounded.OnGrounded -= CheckPlayerGrounded_OnGrounded;
    }

    private void PlayerBlock_OnBlock(bool isBlocking)
    {
        _canJump = isBlocking;
    }

    private void CheckPlayerGrounded_OnGrounded(bool grounded)
    {
        _isGrounded = grounded;
    }

    private void Jump_performed(InputAction.CallbackContext value)
    {
        return;
    }

    // On button release jump is performed
    private void Jump_canceled(InputAction.CallbackContext value)
    {

        if (_canJump)
        {
            HandleJump();
        }
        else
        {
            _canJump = false;
        }
    }

    private void HandleJump()
    {
        Vector2 jumpForce = new(0, 0)
        {
            y = _jumpPower
        };

        if (_restrictJumpCount)
        {
            if (_isGrounded)
            {
                _rigidBody.AddForce(jumpForce, ForceMode2D.Impulse);
            }
        }
        else if (!_restrictJumpCount)
        {
            _rigidBody.AddForce(jumpForce, ForceMode2D.Impulse);
        }
    }

}
