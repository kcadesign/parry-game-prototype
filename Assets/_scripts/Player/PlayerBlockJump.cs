using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBlockJump : MonoBehaviour
{
    public delegate void Block(bool isBlocking);
    public static event Block OnBlock;

    protected PlayerControls playerControls;
    private Rigidbody2D _rigidBody;

    [SerializeField] private float _jumpPower = 5;

    private bool _blockActive;
    private bool _canJump;
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

        playerControls.Gameplay.BlockJump.performed += BlockJump_performed;
        playerControls.Gameplay.BlockJump.canceled += BlockJump_canceled;

        CheckPlayerGrounded.OnGrounded += CheckPlayerGrounded_OnGrounded;
    }

    private void OnDisable()
    {
        playerControls.Gameplay.Disable();

        playerControls.Gameplay.BlockJump.performed -= BlockJump_performed;
        playerControls.Gameplay.BlockJump.canceled -= BlockJump_canceled;

        CheckPlayerGrounded.OnGrounded -= CheckPlayerGrounded_OnGrounded;
    }

    private void BlockJump_performed(InputAction.CallbackContext value)
    {
        _blockActive = true;
        OnBlock?.Invoke(_blockActive);
        if (_blockActive)
        {
            _canJump = true;
        }
    }

    private void BlockJump_canceled(InputAction.CallbackContext value)
    {
        _blockActive = false;
        OnBlock?.Invoke(_blockActive);

        if (_canJump)
        {
            HandleJump();
        }
        _canJump = false;
    }

    private void CheckPlayerGrounded_OnGrounded(bool grounded)
    {
        _isGrounded = grounded;
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
