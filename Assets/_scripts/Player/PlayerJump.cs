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
    [SerializeField] private bool _jumpDesired;
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

    private void Start()
    {
        //Time.timeScale = 0.25f;
    }

    private void Update()
    {
        if (_jumpDesired)
        {
            _jumpBufferCounter += Time.deltaTime;

            if (_isGrounded && _jumpBufferCounter <= JumpBufferLimit)
            {
                Debug.Log("Jumping from buffer");
                _jumpBufferCounter = 0;
                _jumpDesired = false;
                HandleJump();
            }
            else if (_jumpBufferCounter > JumpBufferLimit)
            {
                _jumpBufferCounter = 0;
                _jumpDesired = false;
            }
        }
        else
        {
            _jumpBufferCounter = 0;
        }
    }

    private void Jump_performed(InputAction.CallbackContext obj)
    {
        if (_canJump && _isGrounded)
        {
            HandleJump();
        }

        if (!_isGrounded)
        {
            _jumpDesired = true;
        }
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
        if (stunned)
        {
            _canJump = false;
        }
        else
        {
            _canJump = true;
        }
    }

    private void HandleJump()
    {
        Vector2 jumpForce = new(0, _jumpPower);

        _rigidbody.velocity = new(_rigidbody.velocity.x, 0);
        _rigidbody.AddForce(jumpForce, ForceMode2D.Impulse);
        OnJump?.Invoke(true);
    }
}
