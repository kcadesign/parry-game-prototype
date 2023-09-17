using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatePlayerParry : MonoBehaviour
{
    private Animator _playerAnimator;

    private void Awake()
    {
        _playerAnimator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        PlayerParry.OnParryActive += PlayerParry_OnParryActive;
    }

    private void OnDisable()
    {
        PlayerParry.OnParryActive -= PlayerParry_OnParryActive;
    }

    private void PlayerParry_OnParryActive(bool parryPressed)
    {
        _playerAnimator.SetBool("Parry", parryPressed);
    }

}
