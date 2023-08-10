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
        PlayerBlock.OnBlock += PlayerBlock_OnBlock;
    }

    private void OnDisable()
    {
        PlayerBlock.OnBlock -= PlayerBlock_OnBlock;

    }

    private void PlayerBlock_OnBlock(bool isBlocking)
    {
        if (isBlocking)
        {
            _playerAnimator.SetBool("Jump", false);
        }
        else if (!isBlocking)
        {
            _playerAnimator.SetBool("Jump", true);
        }
    }
}
