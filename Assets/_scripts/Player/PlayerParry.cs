using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerParry : MonoBehaviour
{
    protected PlayerControls playerControls;

    public delegate void ParryActive(bool parryPressed);
    public static event ParryActive OnParryActive;

    private Rigidbody2D _playerRigidbody;
    public Collider2D _playerCollider;
    public PhysicsMaterial2D DefaultPlayerMaterial;
    public PhysicsMaterial2D BouncyMaterial;

    private bool _parryActive = false;
    public float ParryForce = 1;

    private void Awake()
    {
        playerControls = new PlayerControls();
        _playerRigidbody = GetComponent<Rigidbody2D>();
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
        _parryActive = true;
        OnParryActive?.Invoke(_parryActive);
    }

    private void Parry_canceled(InputAction.CallbackContext value)
    {
        //print("Parry button released");
        _parryActive = false;
        _playerCollider.sharedMaterial = DefaultPlayerMaterial;

        OnParryActive?.Invoke(_parryActive);
    }

    private void PlayerBlock_OnBlock(bool isBlocking)
    {
        _parryActive = false;
        OnParryActive?.Invoke(_parryActive);
        //print("Parry button released");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Player trigger entered");
        if (_parryActive)
        {
            _playerCollider.sharedMaterial = BouncyMaterial;
            //_playerRigidbody.AddForce(Vector2.up * ParryForce, ForceMode2D.Impulse);
            //_playerRigidbody.velocity = Vector2.ClampMagnitude(_playerRigidbody.velocity, 20);
        }
        else if (!_parryActive)
        {
            _playerCollider.sharedMaterial = DefaultPlayerMaterial;
            //return;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log($"Enter velocity magnitude is: {_playerRigidbody.velocity.magnitude}");

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        //Debug.Log($"Exit current velocity magnitude is: {_playerRigidbody.velocity.magnitude}");

    }
}
