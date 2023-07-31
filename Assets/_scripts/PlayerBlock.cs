using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBlock : MonoBehaviour
{
    protected PlayerControls playerControls;

    public SpriteRenderer spriteRenderer;
    private Color _originalColor;

    private void Awake()
    {
        playerControls = new PlayerControls();

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
        print("Button released");
        spriteRenderer.color = _originalColor;

    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
