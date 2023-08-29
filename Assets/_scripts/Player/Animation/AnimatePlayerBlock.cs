using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatePlayerBlock : MonoBehaviour
{
    private Animator _playerAnimator;

    public SpriteRenderer SpriteRenderer;

    //private Color _originalColor;
    //public Color BlockingColor;

    private void Awake()
    {
        _playerAnimator = GetComponent<Animator>();

        //_originalColor = SpriteRenderer.color;
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
        //Debug.Log($"Block pressed: {isBlocking}");

        _playerAnimator.SetBool("Block", isBlocking);
        /*
        if (isBlocking)
        {
            SpriteRenderer.color = BlockingColor;
        }
        else
        {
            SpriteRenderer.color = _originalColor;
        }
        */
    }

}
