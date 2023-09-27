using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJump : MonoBehaviour
{
    protected PlayerControls playerControls;

    private Rigidbody2D _rigidBody;

    [SerializeField] private float _jumpPower = 5;

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
        Debug.Log("Jump pressed");
        HandleJump();
    }    
    
    private void Jump_canceled(InputAction.CallbackContext obj)
    {
        Debug.Log("Jump released");
        return;
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
