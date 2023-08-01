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
    }

    private void Jump_performed(InputAction.CallbackContext value)
    {
        //throw new System.NotImplementedException();
        return;
    }

    private void Jump_canceled(InputAction.CallbackContext value)
    {
        HandleJump();
    }

    private void HandleJump()
    {
        Vector2 jumpForce = new Vector2 (0,0);
        jumpForce.y = _jumpPower;

        _rigidBody.AddForce(jumpForce, ForceMode2D.Impulse);
    }

    private void OnDisable()
    {
        playerControls.Gameplay.Disable();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
