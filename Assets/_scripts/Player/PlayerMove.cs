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
    [SerializeField] private float _maxVelocity = 2;
    //[SerializeField] private float _acceleration = 1;
    [SerializeField][Range(0f,10f)] private float _deceleration = 1;

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
        HandleDeceleration();
        OnPlayerMoveInput?.Invoke(_rigidBody.velocity);
        //Debug.Log($"Player current velocity magnitude is: {_rigidBody.velocity.magnitude}");
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
        if (_rigidBody.velocity.magnitude < _maxVelocity)
        {
            Vector2 movementDirection = _movementInput.normalized;
            Vector2 movementForce = _rollSpeed * Time.fixedDeltaTime * movementDirection;
            movementForce.y = 0f;

            _rigidBody.AddForce(movementForce, ForceMode2D.Impulse);
        }
        else if (_rigidBody.velocity.magnitude >= _maxVelocity)
        {
            _rigidBody.AddForce(Vector2.zero);
        }
    }

    private void HandleDeceleration()
    {
        if (_movementInput.magnitude == 0 && _rigidBody.velocity.magnitude > 0)
        {
            Vector2 decelerationForce = _deceleration * Time.fixedDeltaTime * -_rigidBody.velocity.normalized;
            _rigidBody.AddForce(decelerationForce, ForceMode2D.Impulse);
        }
    }
}
