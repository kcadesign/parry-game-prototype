using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBlock : MonoBehaviour
{
    public delegate void Block(bool isBlocking);
    public static event Block OnBlock;
    protected PlayerControls playerControls;

    public bool _blockActive;

    private void Awake()
    {
        playerControls = new PlayerControls();
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
        //Debug.Log(value);

        _blockActive = true;
        OnBlock?.Invoke(_blockActive);
    }

    private void Block_canceled(InputAction.CallbackContext value)
    {
        //Debug.Log(value);

        _blockActive = false;
        OnBlock?.Invoke(_blockActive);
    }
}
