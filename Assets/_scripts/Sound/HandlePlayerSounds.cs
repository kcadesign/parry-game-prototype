using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandlePlayerSounds : MonoBehaviour
{
    public SoundCollection _playerSounds;

    private AudioSource _audioSource;

    private bool _grounded;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        PlayerJump.OnJump += PlayerJump_OnJump;
        PlayerParry.OnParryActive += PlayerParry_OnParryActive;
        PlayerParry.OnParryBounce += PlayerParry_OnParryBounce;
        AnimatePlayer.OnPassiveBounce += AnimatePlayer_OnPassiveBounce;
        HandlePlayerHealth.OnPlayerHurtBig += HandlePlayerHealth_OnPlayerHurtBig;
        HandlePlayerHealth.OnPlayerHurtSmall += HandlePlayerHealth_OnPlayerHurtSmall;
        HandlePlayerHealth.OnPlayerHealthReplenish += HandlePlayerHealth_OnPlayerHealthReplenish;
        PlayerMove.OnPlayerMoveInput += PlayerMove_OnPlayerMoveInput;
        CheckPlayerGrounded.OnGrounded += CheckPlayerGrounded_OnGrounded;
    }

    private void OnDisable()
    {
        PlayerJump.OnJump -= PlayerJump_OnJump;
        PlayerParry.OnParryActive -= PlayerParry_OnParryActive;
        PlayerParry.OnParryBounce -= PlayerParry_OnParryBounce;
        AnimatePlayer.OnPassiveBounce -= AnimatePlayer_OnPassiveBounce;
        HandlePlayerHealth.OnPlayerHurtBig -= HandlePlayerHealth_OnPlayerHurtBig;
        HandlePlayerHealth.OnPlayerHurtSmall -= HandlePlayerHealth_OnPlayerHurtSmall;
        HandlePlayerHealth.OnPlayerHealthReplenish -= HandlePlayerHealth_OnPlayerHealthReplenish;
        PlayerMove.OnPlayerMoveInput -= PlayerMove_OnPlayerMoveInput;
        CheckPlayerGrounded.OnGrounded -= CheckPlayerGrounded_OnGrounded;
    }

    private void PlayerJump_OnJump(bool jumping)
    {
        //Debug.Log("PlayerJump_OnJump signal recieved");
        if (jumping && _playerSounds != null && _playerSounds.Sounds.Length > 0)
        {
            _playerSounds.PlaySound("Jump", transform);
        }
    }

    private void PlayerParry_OnParryActive(bool parryActive)
    {
        if (parryActive && _playerSounds != null && _playerSounds.Sounds.Length > 0)
        {
            _playerSounds.PlaySound("Parry", transform);
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

    private void AnimatePlayer_OnPassiveBounce()
    {
        if (_playerSounds != null && _playerSounds.Sounds.Length > 0)
        {
            _playerSounds.PlaySound("PassiveBounce", transform);
        }
    }

    private void CheckPlayerGrounded_OnGrounded(bool grounded)
    {
        _grounded = grounded;
    }

    private void PlayerMove_OnPlayerMoveInput(Vector2 velocity)
    {
        float velocityX = Mathf.Abs(velocity.x);
        if (_playerSounds != null && _playerSounds.Sounds.Length > 0)
        {
            bool rolling;
            if (velocityX > 0.4f && _grounded)
            {
                rolling = true;
            }
            else
            {
                rolling = false;
            }

            if (rolling && !_audioSource.isPlaying)
            {
                _audioSource.Play();
            }
            else
            {
                _audioSource.Stop();
            }
        }
    }
}
