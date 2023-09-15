using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatePlayerBlock : MonoBehaviour
{
    private Animator _playerAnimator;
    public SpriteRenderer SpriteRenderer;

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
        _playerAnimator.SetBool("Block", isBlocking);
    }

}
