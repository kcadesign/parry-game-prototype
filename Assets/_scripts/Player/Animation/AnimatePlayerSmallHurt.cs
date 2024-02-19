using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatePlayerSmallHurt : MonoBehaviour
{
    private Animator _playerAnimator;

    private void Awake()
    {
        _playerAnimator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        HandlePlayerHealth.OnPlayerHurtSmall += HandlePlayerHealth_OnPlayerHurtSmall;
    }

    private void OnDisable()
    {
        HandlePlayerHealth.OnPlayerHurtSmall -= HandlePlayerHealth_OnPlayerHurtSmall;
    }

    private void HandlePlayerHealth_OnPlayerHurtSmall(bool hurt)
    {
        if (hurt)
        {
            _playerAnimator.SetTrigger("SmallHurt");
        }
    }
}
