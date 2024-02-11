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

    [SerializeField] private bool _restrictJumpCount;
    private bool _isJumping;
    private bool _isGrounded;

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
    }

    private void OnDisable()
    {
        playerControls.Gameplay.Disable();

        playerControls.Gameplay.Jump.performed -= Jump_performed;
        playerControls.Gameplay.Jump.canceled -= Jump_canceled;

        CheckPlayerGrounded.OnGrounded -= CheckPlayerGrounded_OnGrounded;
    }

    private void Jump_performed(InputAction.CallbackContext obj)
    {
        //Debug.Log("Jump pressed");
        _isJumping = true;
        HandleJump();
        OnJump?.Invoke(_isJumping);
        _isJumping = false;
        //OnJump?.Invoke(_isJumping);
    }

    private void Jump_canceled(InputAction.CallbackContext obj)
    {
        //Debug.Log("Jump released");
        _isJumping = false;
        OnJump?.Invoke(_isJumping);
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
                _rigidbody.velocity = new(_rigidbody.velocity.x, 0);
                _rigidbody.AddForce(jumpForce, ForceMode2D.Impulse);
            }
        }
        else if (!_restrictJumpCount)
        {
            _rigidbody.velocity = new(_rigidbody.velocity.x, 0);
            _rigidbody.AddForce(jumpForce, ForceMode2D.Impulse);
        }
    }

}
