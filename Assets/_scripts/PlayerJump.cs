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
    }

    private void OnDisable()
    {
        playerControls.Gameplay.Disable();

        playerControls.Gameplay.Jump.performed -= Jump_performed;
        playerControls.Gameplay.Jump.canceled -= Jump_canceled;

        PlayerBlock.OnBlock -= PlayerBlock_OnBlock;

    }

    private void PlayerBlock_OnBlock()
    {
        _canJump = true;
    }

    private void Jump_performed(InputAction.CallbackContext value)
    {
        //throw new System.NotImplementedException();
        return;
    }

    private void Jump_canceled(InputAction.CallbackContext value)
    {
        if (_canJump)
        {
            HandleJump();
        }
        _canJump = false;
    }

    private void HandleJump()
    {

        Vector2 jumpForce = new Vector2(0, 0);
        jumpForce.y = _jumpPower;

        _rigidBody.AddForce(jumpForce, ForceMode2D.Impulse);
    }

}
