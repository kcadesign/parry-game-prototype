using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerParry : MonoBehaviour
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
        spriteRenderer.color = Color.white;

    }

    private void Parry_canceled(InputAction.CallbackContext value)
    {
        print("Parry window closed");
        //spriteRenderer.color = _originalColor;

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
