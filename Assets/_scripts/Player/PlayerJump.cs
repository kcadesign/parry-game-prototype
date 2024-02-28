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
    private float _jumpBufferCounter;

    private bool _jumpDesired;
    private bool _isGrounded;
    private bool _canJump = true;

    [Header("Gravity")]
    [SerializeField] private float _baseGravity = 1;
    [SerializeField][Range(0f, 5f)] private float _jumpMultiplier = 0;
    [SerializeField][Range(0f, 5f)] private float _fallMultiplier = 0;

    private void Awake()
    {
        playerControls = new PlayerControls();
        _rigidbody = GetComponent<Rigidbody2D>();
        _baseGravity = _rigidbody.gravityScale;
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
        SetJumpGravity();
    }


    private void Jump_performed(InputAction.CallbackContext obj)
    {
        if (_canJump && _isGrounded) DoJump();
        if (!_isGrounded) _jumpDesired = true;
    }

    private void Jump_canceled(InputAction.CallbackContext obj)
    {
        OnJump?.Invoke(false);
    }

    private void CheckPlayerGrounded_OnGrounded(bool grounded)
    {
        _isGrounded = grounded;
        ResetGravity();
    }

    private void HandlePlayerStun_OnStunned(bool stunned)
    {
        // when the player is stunned, they cannot jump
        if (stunned) _canJump = false;
        else _canJump = true;
    }

    private void HandleJumpBuffering()
    {
        if (!_jumpDesired)
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
                _jumpDesired = false;
                DoJump();
            }
            else if (_jumpBufferCounter > JumpBufferLimit)
            {
                _jumpBufferCounter = 0;
                _jumpDesired = false;
            }
        }
    }
    private void SetJumpGravity()
    {
        if (_rigidbody.velocity.y > 0 && !_isGrounded) SetNewGravity(_jumpMultiplier);
        else if (_rigidbody.velocity.y < 0 && !_isGrounded) SetNewGravity(_fallMultiplier);
        else ResetGravity();
    }

    private void SetNewGravity(float gravityMultiplier)
    {
        _rigidbody.gravityScale = gravityMultiplier;
    }

    private void ResetGravity()
    {
        _rigidbody.gravityScale = _baseGravity;
    }

    private void DoJump()
    {
        Vector2 jumpForce = new(0, _jumpPower);

        OnJump?.Invoke(true);
        _rigidbody.velocity = new(_rigidbody.velocity.x, 0);
        _rigidbody.AddForce(jumpForce, ForceMode2D.Impulse);
    }
}
