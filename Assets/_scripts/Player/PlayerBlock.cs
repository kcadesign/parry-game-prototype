using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBlock : MonoBehaviour
{
    public delegate void Block(bool isBlocking);
    public static event Block OnBlock;

    protected PlayerControls playerControls;

    public SpriteRenderer spriteRenderer;
    private Color _originalColor;
    public Color BlockingColor;

    private bool _isBlocking;

    //private bool _isBlocking = false;

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
        _isBlocking = true;

        //print("++Player is blocking");
        spriteRenderer.color = BlockingColor;

        OnBlock?.Invoke(_isBlocking);
    }

    private void Block_canceled(InputAction.CallbackContext value)
    {
        _isBlocking = false;

        //print("Button released");
        spriteRenderer.color = _originalColor;

        OnBlock?.Invoke(_isBlocking);
    }

}
