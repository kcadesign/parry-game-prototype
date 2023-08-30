using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatePlayerHitStun : MonoBehaviour
{
    private Animator _playerAnimator;

    public SpriteRenderer SpriteRenderer;

    //private Color _originalColor;
    //public Color StunColor;

    private void Awake()
    {
        _playerAnimator = GetComponent<Animator>();

        //_originalColor = SpriteRenderer.color;
    }

    private void OnEnable()
    {
        HandlePlayerCollisions.OnStunned += HandlePlayerCollisions_OnStunned;
    }

    private void OnDisable()
    {
        HandlePlayerCollisions.OnStunned -= HandlePlayerCollisions_OnStunned;
    }
    private void HandlePlayerCollisions_OnStunned(bool stunned)
    {
        _playerAnimator.SetBool("Stunned", stunned);
        /*
        if (stunned)
        {
            SpriteRenderer.color = StunColor;
        }
        else
        {
            SpriteRenderer.color = _originalColor;
        }
        */
    }

}
