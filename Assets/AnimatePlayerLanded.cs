using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatePlayerLanded : MonoBehaviour
{
    private Animator _playerAnimator;
    private bool _grounded;
    private bool _localGroundedCheck = false;

    public bool SlowTime;
    [Range(0.1f, 1.0f)] public float TimeScale = 0.1f;


    private void Awake()
    {
        _playerAnimator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        CheckPlayerGrounded.OnGrounded += CheckPlayerGrounded_OnGrounded;
    }

    private void OnDisable()
    {
        CheckPlayerGrounded.OnGrounded -= CheckPlayerGrounded_OnGrounded;
    }

    private void Start()
    {
        // if slow time is true then set time scale to TimeScale value
        if (SlowTime)
        {
            Time.timeScale = TimeScale;
        }
    }

    private void CheckPlayerGrounded_OnGrounded(bool grounded)
    {
        _grounded = grounded;
        if (_grounded && !_localGroundedCheck)
        {
            _playerAnimator.SetTrigger("Landed");
            _localGroundedCheck = true;
        }
        else if (!_grounded && _localGroundedCheck)
        {
            _localGroundedCheck = false;
        }
    }

}
