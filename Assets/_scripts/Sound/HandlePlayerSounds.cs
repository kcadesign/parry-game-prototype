using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandlePlayerSounds : MonoBehaviour
{
    public SoundCollection _playerSounds;

    private void OnEnable()
    {
        PlayerJump.OnJump += PlayerJump_OnJump;
        PlayerParry.OnParryBounce += PlayerParry_OnParryBounce;
        HandlePlayerCollisions.OnPassiveBounce += HandlePlayerCollisions_OnPassiveBounce;
        HandlePlayerHealth.OnPlayerHurtBig += HandlePlayerHealth_OnPlayerHurtBig;
        HandlePlayerHealth.OnPlayerHurtSmall += HandlePlayerHealth_OnPlayerHurtSmall;
        HandlePlayerHealth.OnPlayerHealthReplenish += HandlePlayerHealth_OnPlayerHealthReplenish;
    }

    private void OnDisable()
    {
        PlayerJump.OnJump -= PlayerJump_OnJump;
        PlayerParry.OnParryBounce -= PlayerParry_OnParryBounce;
        HandlePlayerCollisions.OnPassiveBounce -= HandlePlayerCollisions_OnPassiveBounce;
        HandlePlayerHealth.OnPlayerHurtBig -= HandlePlayerHealth_OnPlayerHurtBig;
        HandlePlayerHealth.OnPlayerHurtSmall -= HandlePlayerHealth_OnPlayerHurtSmall;
        HandlePlayerHealth.OnPlayerHealthReplenish -= HandlePlayerHealth_OnPlayerHealthReplenish;
    }

    private void PlayerJump_OnJump(bool jumping)
    {
        //Debug.Log("PlayerJump_OnJump signal recieved");
        if (jumping && _playerSounds != null && _playerSounds.Sounds.Length > 0)
        {
            _playerSounds.PlaySound("Jump", transform);
        }
    }

    private void PlayerParry_OnParryBounce(bool parryBouncing)
    {
        if (parryBouncing && _playerSounds != null && _playerSounds.Sounds.Length > 0)
        {
            _playerSounds.PlaySound("ParryBounce", transform);
        }
    }

    private void HandlePlayerHealth_OnPlayerHurtBig(bool playerHurt)
    {
        if (playerHurt && _playerSounds != null && _playerSounds.Sounds.Length > 0)
        {
            _playerSounds.PlaySound("HurtBig", transform);
        }
    }

    private void HandlePlayerHealth_OnPlayerHurtSmall(bool playerHurt)
    {
        if (playerHurt && _playerSounds != null && _playerSounds.Sounds.Length > 0)
        {
            _playerSounds.PlaySound("HurtSmall", transform);
        }
    }

    private void HandlePlayerHealth_OnPlayerHealthReplenish()
    {
        if (_playerSounds != null && _playerSounds.Sounds.Length > 0)
        {
            _playerSounds.PlaySound("HealthReplenish", transform);
        }
    }

    private void HandlePlayerCollisions_OnPassiveBounce()
    {
        if (_playerSounds != null && _playerSounds.Sounds.Length > 0)
        {
            _playerSounds.PlaySound("PassiveBounce", transform);
        }
    }


}
