using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerParry : MonoBehaviour
{
    protected PlayerControls playerControls;

    public SpriteRenderer BodySpriteRenderer;
    private Color _originalColor;

    private void Awake()
    {
        playerControls = new PlayerControls();

        _originalColor = BodySpriteRenderer.color;
    }

    private void OnEnable()
    {
        playerControls.Gameplay.Enable();

        playerControls.Gameplay.Parry.performed += Parry_performed;
        playerControls.Gameplay.Parry.canceled += Parry_canceled;
    }

    private void OnDisable()
    {
        playerControls.Gameplay.Disable();

        playerControls.Gameplay.Parry.performed -= Parry_performed;
        playerControls.Gameplay.Parry.canceled -= Parry_canceled;

    }

    private void Parry_performed(InputAction.CallbackContext value)
    {
        print("**PARRY**");
        BodySpriteRenderer.color = Color.white;

    }

    private void Parry_canceled(InputAction.CallbackContext value)
    {
        //print("Parry window closed");
        //BodySpriteRenderer.color = _originalColor;

    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
