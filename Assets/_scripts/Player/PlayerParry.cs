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

    private bool _parryActive;
    //private bool _blockActive;
    private bool _grounded;

    private bool _forceApplied;
    [SerializeField] private float _parryForce = 50;

    public LayerMask GroundLayer;

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

        PlayerBlockJump.OnBlock += PlayerBlockJump_OnBlock;
        CheckPlayerGrounded.OnGrounded += CheckPlayerGrounded_OnGrounded;
    }

    private void OnDisable()
    {
        playerControls.Gameplay.Disable();

        playerControls.Gameplay.Parry.performed -= Parry_performed;
        playerControls.Gameplay.Parry.canceled -= Parry_canceled;

        PlayerBlockJump.OnBlock -= PlayerBlockJump_OnBlock;
        CheckPlayerGrounded.OnGrounded -= CheckPlayerGrounded_OnGrounded;
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
        OnParryActive?.Invoke(_parryActive);
    }

    private void PlayerBlockJump_OnBlock(bool isBlocking)
    {
        //print("Parry button released");
        _parryActive = false;
        OnParryActive?.Invoke(_parryActive);
    }
    private void CheckPlayerGrounded_OnGrounded(bool grounded)
    {
        _grounded = grounded;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((GroundLayer.value & (1 << collision.gameObject.layer)) != 0)
        {
            if (_parryActive && !_forceApplied && _grounded)
            {
                _playerRigidbody.velocity = new(_playerRigidbody.velocity.x, 0);
                _playerRigidbody.AddForce(Vector2.up * _parryForce, ForceMode2D.Impulse);
                //Debug.Log("Force applied");

                _forceApplied = true;
            }

        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        _forceApplied = false;
    }
}
