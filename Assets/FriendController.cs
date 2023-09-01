using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FriendController : MonoBehaviour
{
    public delegate void FriendDeployed(bool friendDeployed);
    public static event FriendDeployed OnFriendDeployed;

    protected PlayerControls playerControls;
    public Transform PlayerTransform;

    public FriendFollowPlayer FollowScript;
    private Vector3 _spawnPosition;
    public Vector3 SpawnOffset;


    private bool _playerGrounded;
    private bool _friendDeployed = false;

    private void Awake()
    {
        playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        playerControls.Gameplay.Enable();

        playerControls.Gameplay.ActivateFriend.performed += ActivateFriend_performed;
        playerControls.Gameplay.ActivateFriend.canceled += ActivateFriend_canceled;

        CheckPlayerGrounded.OnGrounded += CheckPlayerGrounded_OnGrounded;
    }


    private void OnDisable()
    {
        playerControls.Gameplay.Disable();

        playerControls.Gameplay.ActivateFriend.performed -= ActivateFriend_performed;
        playerControls.Gameplay.ActivateFriend.canceled -= ActivateFriend_canceled;

        CheckPlayerGrounded.OnGrounded -= CheckPlayerGrounded_OnGrounded;

    }

    private void ActivateFriend_performed(InputAction.CallbackContext value)
    {
        // On button press, friend appears below the player
        // If player is grounded friend will not appear below player
        // If friend has been positioned, pressing the button again brings the friend back to the player
        _spawnPosition = PlayerTransform.position + SpawnOffset;

        if (!_playerGrounded && !_friendDeployed)
        {
            FollowScript.enabled = false;
            gameObject.transform.position = _spawnPosition;
            _friendDeployed = true;

            OnFriendDeployed?.Invoke(_friendDeployed);
            print(_friendDeployed);
        }
        /*
        if (_friendDeployed)
        {
            //FollowScript.enabled = true;
            _friendDeployed = false;

            OnFriendDeployed?.Invoke(_friendDeployed);

        }
        */
    }

    private void ActivateFriend_canceled(InputAction.CallbackContext value)
    {
        // on button release do nothing
        throw new System.NotImplementedException();
    }

    private void CheckPlayerGrounded_OnGrounded(bool grounded)
    {
        _playerGrounded = grounded;
    }
}
