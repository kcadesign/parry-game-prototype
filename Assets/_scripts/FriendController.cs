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
        Vector3 spawnPosition = PlayerTransform.position + SpawnOffset;

        if (!_playerGrounded && !_friendDeployed)
        {
            FollowScript.Offset = new Vector3(0f, -2f, 0f);
            FollowScript.UseLerping = false;
            FollowScript.ConstrainYPosition = true;

            gameObject.transform.position = spawnPosition;
            _friendDeployed = true;
        }
        else if (_friendDeployed)
        {
            FollowScript.Offset = new Vector3(-1f, 0.2f, 0f);
            FollowScript.UseLerping = true;
            FollowScript.ConstrainXPosition = false;
            FollowScript.ConstrainYPosition = false;
            FollowScript.ConstrainZPosition = false;

            _friendDeployed = false;
        }
        OnFriendDeployed?.Invoke(_friendDeployed);
    }

    private void ActivateFriend_canceled(InputAction.CallbackContext value)
    {
        return;
    }

    private void CheckPlayerGrounded_OnGrounded(bool grounded) => _playerGrounded = grounded;
        
    private void OnCollisionEnter2D(Collision2D collision)
    {
        FollowScript.UseLerping = false;
        FollowScript.ConstrainXPosition = true;
        FollowScript.ConstrainYPosition = true;
        FollowScript.ConstrainZPosition = true;
    }
}