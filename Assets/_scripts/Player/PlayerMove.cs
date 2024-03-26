using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    public delegate void PlayerVelocityChange(Vector2 playerVelocity);
    public static event PlayerVelocityChange OnPlayerMoveInput;

    protected PlayerControls playerControls;
    private Rigidbody2D _rigidBody;

    private Vector2 _movementInput;

    [SerializeField] private float _rollSpeed = 5;
    //[SerializeField] private float _maxVelocity = 2;
    [SerializeField][Range(0f, 10f)] private float _acceleration = 1;
    [SerializeField][Range(0f, 10f)] private float _deceleration = 1;

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
    }

    private void Rolling_performed(InputAction.CallbackContext value)
    {
        _movementInput = value.ReadValue<Vector2>();
    }

    private void Rolling_canceled(InputAction.CallbackContext value)
    {
        _movementInput = value.ReadValue<Vector2>();
    }

    private void HandleHorizontalMovement()
    {
        float targetSpeed = _movementInput.x * _rollSpeed;
        float speedDifference = targetSpeed - _rigidBody.velocity.x;
        float accelerationRate = (Mathf.Abs(targetSpeed) > 0.01f) ? _acceleration : _deceleration;
        float movementForce = (Mathf.Abs(speedDifference) * accelerationRate) * Mathf.Sign(speedDifference);
        _rigidBody.AddForce(movementForce * Vector2.right);
    }

}
