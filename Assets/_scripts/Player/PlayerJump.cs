using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJump : MonoBehaviour
{
    protected PlayerControls playerControls;

    public delegate void Jump(bool jumping);
    public static event Jump OnJump;

    private Rigidbody2D _rigidbody;

    [SerializeField] private float _jumpPower = 5;
    [Range(0f, 0.3f)] public float JumpBufferLimit = 0.15f;
    [SerializeField] private float _jumpBufferCounter;

    [SerializeField] private bool _consecutiveJumpDesired;
    private bool _isGrounded;
    private bool _canJump = true;

    private void Awake()
    {
        playerControls = new PlayerControls();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        playerControls.Gameplay.Enable();

        playerControls.Gameplay.Jump.performed += Jump_performed;
        playerControls.Gameplay.Jump.canceled += Jump_canceled;

        CheckPlayerGrounded.OnGrounded += CheckPlayerGrounded_OnGrounded;
        HandlePlayerStun.OnStunned += HandlePlayerStun_OnStunned;
    }

    private void OnDisable()
    {
        playerControls.Gameplay.Disable();

        playerControls.Gameplay.Jump.performed -= Jump_performed;
        playerControls.Gameplay.Jump.canceled -= Jump_canceled;

        CheckPlayerGrounded.OnGrounded -= CheckPlayerGrounded_OnGrounded;
        HandlePlayerStun.OnStunned -= HandlePlayerStun_OnStunned;
    }

    private void Update()
    {
        HandleJumpBuffering();
    }

    private void Jump_performed(InputAction.CallbackContext obj)
    {
        if (_canJump && _isGrounded) DoJump();
        if (!_isGrounded) _consecutiveJumpDesired = true;
    }

    private void Jump_canceled(InputAction.CallbackContext obj)
    {
        OnJump?.Invoke(false);
    }

    private void CheckPlayerGrounded_OnGrounded(bool grounded)
    {
        _isGrounded = grounded;
    }

    private void HandlePlayerStun_OnStunned(bool stunned)
    {
        // when the player is stunned, they cannot jump
        if (stunned) _canJump = false;
        else _canJump = true;
    }

    private void HandleJumpBuffering()
    {
        if (!_consecutiveJumpDesired)
        {
            _jumpBufferCounter = 0;
        }
        else
        {
            _jumpBufferCounter += Time.deltaTime;

            if (_isGrounded && _jumpBufferCounter <= JumpBufferLimit)
            {
                //Debug.Log("Jumping from buffer");
                _jumpBufferCounter = 0;
                _consecutiveJumpDesired = false;
                DoJump();
            }
            else if (_jumpBufferCounter > JumpBufferLimit)
            {
                _jumpBufferCounter = 0;
                _consecutiveJumpDesired = false;
            }
        }
    }

    private void DoJump()
    {
        Vector2 jumpForce = new(0, _jumpPower);

        OnJump?.Invoke(true);
        _rigidbody.velocity = new(_rigidbody.velocity.x, 0);
        _rigidbody.AddForce(jumpForce, ForceMode2D.Impulse);
    }
}
