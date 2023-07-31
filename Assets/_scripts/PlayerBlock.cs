using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBlock : MonoBehaviour
{
    protected PlayerControls playerControls;
    private Rigidbody2D _rigidBody;

    public SpriteRenderer spriteRenderer;
    private Color _originalColor;

    private void Awake()
    {
        playerControls = new PlayerControls();
        _rigidBody = GetComponent<Rigidbody2D>();

        _originalColor = spriteRenderer.color;
    }

    private void OnEnable()
    {
        playerControls.Gameplay.Enable();

        playerControls.Gameplay.Block.performed += Block_performed;
        playerControls.Gameplay.Block.canceled += Block_canceled;
    }

    private void OnDisable()
    {
        playerControls.Gameplay.Disable();

        playerControls.Gameplay.Block.performed -= Block_performed;
        playerControls.Gameplay.Block.canceled -= Block_canceled;

    }

    private void Block_performed(InputAction.CallbackContext value)
    {
        print("++Player is blocking");
        spriteRenderer.color = Color.blue;

    }

    private void Block_canceled(InputAction.CallbackContext value)
    {
        print("--Player is not blocking");
        spriteRenderer.color = _originalColor;

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
