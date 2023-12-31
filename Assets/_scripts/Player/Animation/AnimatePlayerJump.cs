using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatePlayerJump : MonoBehaviour
{
    private Animator _playerAnimator;

    private void Awake()
    {
        _playerAnimator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        PlayerJump.OnJump += PlayerJump_OnJump;
    }

    private void OnDisable()
    {
        PlayerJump.OnJump -= PlayerJump_OnJump;
    }

    private void PlayerJump_OnJump(bool jumping)
    {
        if (jumping)
        {
            _playerAnimator.SetTrigger("Jumping");
        }
    }

}
