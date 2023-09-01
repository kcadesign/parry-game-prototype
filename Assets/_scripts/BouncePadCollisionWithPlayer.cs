using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePadCollisionWithPlayer : MonoBehaviour
{
    public CircleCollider2D BouncyCollider;
    public CircleCollider2D DefaultCollider;

    private bool _playerIsParrying;
    private bool _friendDeployed;

    private void Awake()
    {
        DefaultCollider.enabled = false;
        BouncyCollider.enabled = false;
    }

    private void OnEnable()
    {
        PlayerParry.OnParryActive += PlayerParry_OnParryActive;
        FriendController.OnFriendDeployed += FriendController_OnFriendDeployed;
    }

    private void OnDisable()
    {
        PlayerParry.OnParryActive -= PlayerParry_OnParryActive;
        FriendController.OnFriendDeployed -= FriendController_OnFriendDeployed;

    }

    private void PlayerParry_OnParryActive(bool parryPressed)
    {
        _playerIsParrying = parryPressed;
    }

    private void FriendController_OnFriendDeployed(bool friendDeployed)
    {
        _friendDeployed = friendDeployed;
    }

    private void FixedUpdate()
    {
        HandlePlayerInteraction();
    }

    private void HandlePlayerInteraction()
    {
        if (!_friendDeployed)
        {
            DefaultCollider.enabled = false;
            BouncyCollider.enabled = false;
        }
        else if (_friendDeployed)
        {
            if (!_playerIsParrying)
            {
                DefaultCollider.enabled = true;
                BouncyCollider.enabled = false;
            }
            else if(_playerIsParrying)
            {
                DefaultCollider.enabled = false;
                BouncyCollider.enabled = true;
            }
        }

    }
}
