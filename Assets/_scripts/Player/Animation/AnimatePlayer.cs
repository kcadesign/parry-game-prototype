using System;
using UnityEngine;

public class AnimatePlayer : MonoBehaviour
{
    public static event Action OnPassiveBounce;

    public Rigidbody2D PlayerRigidbody;
    private Animator _animator;

    private bool _grounded;
    private bool _stunned;

    private bool _localGroundedCheck = false;

    public bool SlowTime;
    [Range(0.1f, 1.0f)] public float TimeScale = 0.1f;



    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        PlayerJump.OnJump += PlayerJump_OnJump;
        CheckPlayerGrounded.OnGrounded += CheckPlayerGrounded_OnGrounded;
        HandlePlayerStun.OnStunned += HandlePlayerStun_OnStunned;
        PlayerParry.OnParryActive += PlayerParry_OnParryActive;
        HandlePlayerHealth.OnPlayerHurtSmall += HandlePlayerHealth_OnPlayerHurtSmall;
    }

    private void OnDisable()
    {
        PlayerJump.OnJump -= PlayerJump_OnJump;
        CheckPlayerGrounded.OnGrounded -= CheckPlayerGrounded_OnGrounded;
        HandlePlayerStun.OnStunned -= HandlePlayerStun_OnStunned;
        PlayerParry.OnParryActive -= PlayerParry_OnParryActive;
        HandlePlayerHealth.OnPlayerHurtSmall -= HandlePlayerHealth_OnPlayerHurtSmall;
    }

    private void Start()
    {
        if (SlowTime)
        {
            Time.timeScale = TimeScale;
        }
    }

    void FixedUpdate()
    {
        AnimatePlayerMove();
    }

    private void AnimatePlayerMove()
    {
        float velocityX = PlayerRigidbody.velocity.x;

        if (velocityX < -0.1) transform.eulerAngles = new Vector3(0, 180, 0);
        else if (velocityX > 0.1) transform.eulerAngles = new Vector3(0, 0, 0);

        _animator.SetFloat("VelocityX", Mathf.Abs(velocityX));

        if (velocityX > -0.01 && velocityX < 0.01) _animator.SetBool("Moving", false);
        else _animator.SetBool("Moving", true);
        
    }

    private void CheckPlayerGrounded_OnGrounded(bool grounded)
    {
        _grounded = grounded;

        if (_grounded && !_localGroundedCheck)
        {
            _animator.SetTrigger("Landed");
            OnPassiveBounce?.Invoke();

            _localGroundedCheck = true;
        }
        else if (!_grounded && _localGroundedCheck)
        {
            _localGroundedCheck = false;
        }
    }

    private void PlayerJump_OnJump(bool jumping)
    {
        if (_grounded && !_stunned)
        {
            _animator.SetTrigger("Jumping");
        }
    }

    private void HandlePlayerStun_OnStunned(bool stunned)
    {
        if (stunned)
        {
            _stunned = true;
        }
        else
        {
            _stunned = false;
        }

        if (stunned)
        {
            _animator.SetBool("Stunned", true);
            _animator.SetTrigger("StunnedTrigger");
        }
        else if (!stunned)
        {
            _animator.SetBool("Stunned", false);
            _animator.ResetTrigger("StunnedTrigger");
        }
    }

    private void PlayerParry_OnParryActive(bool parryPressed)
    {
        _animator.SetBool("Parry", parryPressed);
    }

    private void HandlePlayerHealth_OnPlayerHurtSmall(bool hurt)
    {
        if (hurt)
        {
            _animator.SetTrigger("SmallHurt");
        }
    }

}
