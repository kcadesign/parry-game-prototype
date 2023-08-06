using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBlock : MonoBehaviour
{
    public delegate void Block();
    public static event Block OnBlock;

    protected PlayerControls playerControls;

    public SpriteRenderer spriteRenderer;
    private Color _originalColor;

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
        print("++Player is blocking");
        spriteRenderer.color = new Color(0.47f, 0.82f, 0.96f);

        //_isBlocking = true;
        OnBlock?.Invoke();
    }

    private void Block_canceled(InputAction.CallbackContext value)
    {
        print("Button released");
        spriteRenderer.color = _originalColor;

        //_isBlocking = false;
    }

}
