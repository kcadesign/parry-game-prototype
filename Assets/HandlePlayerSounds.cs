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
        HandlePlayerHealth.OnPlayerHurtBig += HandlePlayerHealth_OnPlayerHurtBig;
        HandlePlayerHealth.OnPlayerHurtSmall += HandlePlayerHealth_OnPlayerHurtSmall;
    }

    private void OnDisable()
    {
        PlayerJump.OnJump -= PlayerJump_OnJump;
        PlayerParry.OnParryBounce -= PlayerParry_OnParryBounce;
        HandlePlayerHealth.OnPlayerHurtBig -= HandlePlayerHealth_OnPlayerHurtBig;
        HandlePlayerHealth.OnPlayerHurtSmall -= HandlePlayerHealth_OnPlayerHurtSmall;
    }

    private void PlayerJump_OnJump(bool jumping)
    {
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
}
