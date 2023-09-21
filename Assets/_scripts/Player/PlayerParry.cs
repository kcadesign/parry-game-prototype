using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerParry : MonoBehaviour
{
    protected PlayerControls playerControls;

    public delegate void ParryActive(bool parryPressed);
    public static event ParryActive OnParryActive;

    //private Rigidbody2D _playerRigidbody;

    private bool _parryActive;
    //private bool _blockActive;
    //public float ParryForce = 100;

    private void Awake()
    {
        playerControls = new PlayerControls();
        //_playerRigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        playerControls.Gameplay.Enable();

        playerControls.Gameplay.Parry.performed += Parry_performed;
        playerControls.Gameplay.Parry.canceled += Parry_canceled;

        PlayerBlockJump.OnBlock += PlayerBlockJump_OnBlock;
    }

    private void OnDisable()
    {
        playerControls.Gameplay.Disable();

        playerControls.Gameplay.Parry.performed -= Parry_performed;
        playerControls.Gameplay.Parry.canceled -= Parry_canceled;

        PlayerBlockJump.OnBlock -= PlayerBlockJump_OnBlock;
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
    
}
