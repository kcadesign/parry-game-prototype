using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerParry : MonoBehaviour
{
    protected PlayerControls playerControls;

    public delegate void ParryActive(bool parryPressed);
    public static event ParryActive OnParryActive;

    public static event Action<bool> OnParryBounce;

    private Rigidbody2D _playerRigidbody;

    private bool _parryActive;
    [SerializeField] private float _parryActiveLength = 0.5f;
    //private bool _blockActive;
    //private bool _grounded;
    private bool _stunned;

    private bool _canParryBounce;
    private bool _forceApplied;
    [SerializeField] private float _parryForce = 50;

/*    [Header("Audio")]
    [SerializeField] private AudioClip parryAttackSound;
    [SerializeField] private AudioClip parryBounceSound;
*/
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
        HandlePlayerStun.OnStunned += HandlePlayerStun_OnStunned;
        HandlePlayerCollisions.OnCollision += HandlePlayerCollisions_OnCollision;
    }

    private void OnDisable()
    {
        playerControls.Gameplay.Disable();

        playerControls.Gameplay.Parry.performed -= Parry_performed;
        playerControls.Gameplay.Parry.canceled -= Parry_canceled;

        PlayerBlock.OnBlock -= PlayerBlock_OnBlock;
        CheckPlayerGrounded.OnGrounded -= CheckPlayerGrounded_OnGrounded;
        HandlePlayerStun.OnStunned -= HandlePlayerStun_OnStunned;
        HandlePlayerCollisions.OnCollision -= HandlePlayerCollisions_OnCollision;
    }

    private void HandlePlayerStun_OnStunned(bool stunned)
    {
        if (stunned)
        {
            _stunned = true;
        }
        else
        {
            _stunned = false;
        }
    }

    private void Parry_performed(InputAction.CallbackContext value)
    {
        // if _stunned is true, the player cannot parry
        if (_stunned)
        {
            return;
        }

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
            //HandleParryBounce();
            _canParryBounce = false;
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

            OnParryBounce?.Invoke(true);

            _forceApplied = true;
            _canParryBounce = true;
        }
        OnParryBounce?.Invoke(false);
        _canParryBounce = false;
        _forceApplied = false;
    }


    private IEnumerator ParryActiveTimer()
    {
        // if time scale is 0, the player cannot parry
        if (Time.timeScale == 0)
        {
            yield break;
        }

        _parryActive = true;
        OnParryActive?.Invoke(_parryActive);

        float parryTimer = 0f; // Initialize timer

        while (_parryActive && parryTimer < _parryActiveLength)
        {
            // Increment timer each frame
            parryTimer += Time.deltaTime;
            yield return null; // Wait for the next frame
        }

        // Check if _parryActive is still true when exiting the loop
        if (_parryActive)
        {
            _parryActive = false;
            OnParryActive?.Invoke(_parryActive);
        }
        else
        {
            // Reset the timer if _parryActive became false before _parryActiveLength elapsed
            parryTimer = 0f;
        }
    }
}
