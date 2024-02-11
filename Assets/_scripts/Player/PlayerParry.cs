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
    [SerializeField] private float _parryActiveLength = 0.5f;
    //private bool _blockActive;
    //private bool _grounded;

    private bool _canParryBounce;
    private bool _forceApplied;
    [SerializeField] private float _parryForce = 50;

    //public LayerMask GroundLayer;

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
        CheckPlayerGrounded.OnGrounded += CheckPlayerGrounded_OnGrounded;
        HandlePlayerCollisions.OnCollision += HandlePlayerCollisions_OnCollision;
    }

    private void OnDisable()
    {
        playerControls.Gameplay.Disable();

        playerControls.Gameplay.Parry.performed -= Parry_performed;
        playerControls.Gameplay.Parry.canceled -= Parry_canceled;

        PlayerBlock.OnBlock -= PlayerBlock_OnBlock;
        CheckPlayerGrounded.OnGrounded -= CheckPlayerGrounded_OnGrounded;
        HandlePlayerCollisions.OnCollision -= HandlePlayerCollisions_OnCollision;
    }

    private void Parry_performed(InputAction.CallbackContext value)
    {
        // If the parry is active for longer than 0.5f, set it to false
        StartCoroutine(ParryActiveTimer());
    }

    private void Parry_canceled(InputAction.CallbackContext value)
    {
        _parryActive = false;
        OnParryActive?.Invoke(_parryActive);
    }
    
    private void PlayerBlock_OnBlock(bool isBlocking)
    {
        if (isBlocking)
        {
            _parryActive = false;
        }
        OnParryActive?.Invoke(_parryActive);
    }
    
    private void CheckPlayerGrounded_OnGrounded(bool grounded)
    {
        //Debug.Log($"Is grounded: {grounded}");
        if (grounded)
        {
            HandleParryBounce();
        }
        else if (!grounded)
        {
            _canParryBounce = true;
            _forceApplied = false;
        }
        //Debug.Log($"Can parry bounce: {_canParryBounce}");

    }

    private void HandlePlayerCollisions_OnCollision(GameObject collidedObject)
    {
        HandleParryBounce();
    }

    private void HandleParryBounce()
    {
        if (_parryActive && _canParryBounce && !_forceApplied)
        {
            _playerRigidbody.velocity = new(_playerRigidbody.velocity.x, 0);
            _playerRigidbody.AddForce(Vector2.up * _parryForce, ForceMode2D.Impulse);

            _forceApplied = true;
            _canParryBounce = true;
        }
        _canParryBounce = false;
        _forceApplied = false;
    }


    /*
    private void OnCollisionEnter2D(Collision2D collision)
    {
        HandleParryBounce(collision);
    }
    
    private void OnCollisionStay2D(Collision2D collision)
    {
        HandleParryBounce(collision);
    }
    
    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log($"Force applied: {_canParryBounce}");

    }

    
    private void HandleParryBounce(Collision2D collision)
    {
        if ((GroundLayer.value & (1 << collision.gameObject.layer)) != 0)
        {
            //Debug.Log($"Player is grounded: {_grounded}");
            if (_parryActive && !_forceApplied && _grounded)
            {
                _playerRigidbody.velocity = new(_playerRigidbody.velocity.x, 0);
                _playerRigidbody.AddForce(Vector2.up * _parryForce, ForceMode2D.Impulse);
                Debug.Log("Force applied");

                _forceApplied = true;
            }
        }
    }*/

    private IEnumerator ParryActiveTimer()
    {
        _parryActive = true;
        OnParryActive?.Invoke(_parryActive);
        Debug.Log($"Parry active: {_parryActive}");
        yield return new WaitForSeconds(_parryActiveLength);
        _parryActive = false;
        OnParryActive?.Invoke(_parryActive);
        Debug.Log($"Parry active: {_parryActive}");
    }
}
