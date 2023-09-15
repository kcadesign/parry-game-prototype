using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    public delegate void PlayerVelocityChange(Vector2 playerVelocity);
    public static event PlayerVelocityChange OnPlayerMoveInput;

    protected PlayerControls playerControls;
    private Rigidbody2D _rigidBody;

    private Vector2 _movementAxis;

    [SerializeField] private float _rollSpeed = 5;
    [SerializeField] private float _maxVelocity = 2;

    private void Awake()
    {
        playerControls = new PlayerControls();
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        playerControls.Gameplay.Enable();

        playerControls.Gameplay.Rolling.performed += Rolling_performed;
        playerControls.Gameplay.Rolling.canceled += Rolling_canceled;
    }

    private void OnDisable()
    {
        playerControls.Gameplay.Disable();

        playerControls.Gameplay.Rolling.performed -= Rolling_performed;
        playerControls.Gameplay.Rolling.canceled -= Rolling_canceled;
    }

    void FixedUpdate()
    {
        HandleHorizontalMovement();
        OnPlayerMoveInput?.Invoke(_rigidBody.velocity);
        Debug.Log($"Player current velocity magnitude is: {_rigidBody.velocity.magnitude}");

    }

    private void Rolling_performed(InputAction.CallbackContext value)
    {
        _movementAxis = value.ReadValue<Vector2>();
    }

    private void Rolling_canceled(InputAction.CallbackContext value)
    {
        _movementAxis = value.ReadValue<Vector2>();
    }

    private void HandleHorizontalMovement()
    {
        Vector2 movementDirection = _movementAxis.normalized;
        movementDirection.y = 0f;

        Vector2 movementForce = _rollSpeed * Time.fixedDeltaTime * movementDirection;
        movementForce.y = 0f;

        // Would like to be able to add force in the opposite direction even if velocity is over max
        if (_movementAxis.magnitude > 0 && _rigidBody.velocity.magnitude < _maxVelocity)
        {
            _rigidBody.AddForce(movementForce, ForceMode2D.Impulse);
        }
        
        if(_rigidBody.velocity.magnitude > _maxVelocity)
        {
            //_rigidBody.velocity = Vector2.ClampMagnitude(_rigidBody.velocity, _maxVelocity);
            _rigidBody.AddForce(Vector2.zero);
        }
    }
}
