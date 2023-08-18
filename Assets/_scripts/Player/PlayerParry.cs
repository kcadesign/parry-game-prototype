using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerParry : MonoBehaviour
{
    protected PlayerControls playerControls;

    public delegate void ParryActive(bool parryPressed);
    public static event ParryActive OnParryActive;

    public SpriteRenderer BodySpriteRenderer;
    public Color ParryColor;

    private bool _parryActive = false;

    private void Awake()
    {
        playerControls = new PlayerControls();

    }

    private void OnEnable()
    {
        playerControls.Gameplay.Enable();

        playerControls.Gameplay.Parry.performed += Parry_performed;
        playerControls.Gameplay.Parry.canceled += Parry_canceled;

        PlayerBlock.OnBlock += PlayerBlock_OnBlock;
    }


    private void OnDisable()
    {
        playerControls.Gameplay.Disable();

        playerControls.Gameplay.Parry.performed -= Parry_performed;
        playerControls.Gameplay.Parry.canceled -= Parry_canceled;

        PlayerBlock.OnBlock -= PlayerBlock_OnBlock;
    }

    private void Parry_performed(InputAction.CallbackContext value)
    {
        //print("Parry button pressed");
        BodySpriteRenderer.color = ParryColor;
        _parryActive = true;
        OnParryActive?.Invoke(_parryActive);
    }

    private void Parry_canceled(InputAction.CallbackContext value)
    {
        //print("Parry button released");
        _parryActive = false;
        OnParryActive?.Invoke(_parryActive);
    }
    private void PlayerBlock_OnBlock(bool isBlocking)
    {
        //BodySpriteRenderer.color = _originalColor;
        _parryActive = false;
        OnParryActive?.Invoke(_parryActive);
        print("Parry button released");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //print(collision.gameObject.tag);

        if (_parryActive)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                // Will need to refactor for health points
                Destroy(collision.gameObject.transform.parent.gameObject);
            }

        }
    }
}
