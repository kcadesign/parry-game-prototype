using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatePlayerParry : MonoBehaviour
{
    private Animator _playerAnimator;

    public SpriteRenderer BodySpriteRenderer;
    public Color ParryColor;

    private void OnEnable()
    {
        PlayerParry.OnParryActive += PlayerParry_OnParryActive;
    }

    private void OnDisable()
    {
        PlayerParry.OnParryActive -= PlayerParry_OnParryActive;
    }

    private void Awake()
    {
        _playerAnimator = GetComponent<Animator>();
    }

    private void PlayerParry_OnParryActive(bool parryPressed)
    {
        //Debug.Log($"Parry pressed: {parryPressed}");
        _playerAnimator.SetBool("Parry", parryPressed);

        BodySpriteRenderer.color = ParryColor;
    }

}
