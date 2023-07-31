using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    protected PlayerControls playerControls;
    private Vector2 _movementAxis;
    public Vector2 MovementAxis { get { return _movementAxis; } }

    [SerializeField] private float _rollSpeed = 5;
    private Rigidbody2D rigidBody;


    private void Awake()
    {
        playerControls = new PlayerControls();
        rigidBody = GetComponent<Rigidbody2D>();
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

    private void Rolling_performed(InputAction.CallbackContext value)
    {
        _movementAxis = value.ReadValue<Vector2>();
    }

    private void Rolling_canceled(InputAction.CallbackContext value)
    {
        _movementAxis = Vector2.zero;
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        HandleHorizontalMovement();
    }

    private void HandleHorizontalMovement()
    {
        Vector2 movementDirection;

        movementDirection.x = _movementAxis.x;
        movementDirection.y = 0f;

        Vector2 movementForce = _rollSpeed * Time.fixedDeltaTime * movementDirection;
        movementForce.y = 0f;

        if (_movementAxis.magnitude > 0)
        {
            rigidBody.AddForce(movementForce, ForceMode2D.Impulse);
        }

    }
}
