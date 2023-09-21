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
        PlayerBlockJump.OnBlock += PlayerBlockJump_OnBlock;
    }

    private void OnDisable()
    {
        PlayerBlockJump.OnBlock -= PlayerBlockJump_OnBlock;
    }

    private void PlayerBlockJump_OnBlock(bool isBlocking)
    {
        if (isBlocking)
        {
            _playerAnimator.SetTrigger("Jumping");
        }
    }
}
