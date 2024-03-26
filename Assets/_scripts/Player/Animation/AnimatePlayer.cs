using System;
using UnityEngine;

public class AnimatePlayer : MonoBehaviour
{
    public static event Action OnPassiveBounce;

    private Rigidbody2D PlayerRigidbody;
    private Animator _animator;
    public ParticleSystem DustParticles;

    private bool _grounded;
    private bool _hasNegativeYVelocity;

    private bool _localGroundedCheck = false;

    public bool SlowTime;
    [Range(0.1f, 1.0f)] public float TimeScale = 0.1f;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        PlayerRigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        PlayerJump.OnJump += PlayerJump_OnJump;
        CheckPlayerGrounded.OnGrounded += CheckPlayerGrounded_OnGrounded;
        HandlePlayerStun.OnStunned += HandlePlayerStun_OnStunned;
        PlayerParry.OnParryActive += PlayerParry_OnParryActive;
        HandlePlayerHealth.OnPlayerHurtSmall += HandlePlayerHealth_OnPlayerHurtSmall;
        HandlePlayerHealth.OnPlayerDead += HandlePlayerHealth_OnPlayerDead;
    }

    private void OnDisable()
    {
        PlayerJump.OnJump -= PlayerJump_OnJump;
        CheckPlayerGrounded.OnGrounded -= CheckPlayerGrounded_OnGrounded;
        HandlePlayerStun.OnStunned -= HandlePlayerStun_OnStunned;
        PlayerParry.OnParryActive -= PlayerParry_OnParryActive;
        HandlePlayerHealth.OnPlayerHurtSmall -= HandlePlayerHealth_OnPlayerHurtSmall;
        HandlePlayerHealth.OnPlayerDead -= HandlePlayerHealth_OnPlayerDead;
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
        CheckYVelocity();
        AnimateFalling();
    }

    private void AnimatePlayerMove()
    {
        float velocityX = PlayerRigidbody.velocity.x;

        if (velocityX < -0.1) transform.eulerAngles = new Vector3(0, 180, 0);
        else if (velocityX > 0.1) transform.eulerAngles = new Vector3(0, 0, 0);

        _animator.SetFloat("VelocityX", Mathf.Abs(velocityX));

        if (velocityX > -0.02 && velocityX < 0.02) _animator.SetBool("Moving", false);
        else _animator.SetBool("Moving", true);

        if (_grounded && PlayerRigidbody.velocity.magnitude > 0.1f)
        {
            CreateDustParticles();
        }
    }

    private void CheckYVelocity()
    {
        // check if player is falling
        if (PlayerRigidbody.velocity.y < -0.1) _hasNegativeYVelocity = true;
    }
    private void AnimateFalling()
    {
        if (_hasNegativeYVelocity && !_grounded) _animator.SetBool("Falling", true);
        else _animator.SetBool("Falling", false);
    }

    private void CheckPlayerGrounded_OnGrounded(bool grounded)
    {
        _grounded = grounded;
        if (_hasNegativeYVelocity)
        {
            if (_grounded && !_localGroundedCheck)
            {
                CreateDustParticles();
                _animator.SetTrigger("Landed");
                OnPassiveBounce?.Invoke();

                _localGroundedCheck = true;
            }
            else if (!_grounded && _localGroundedCheck)
            {
                _localGroundedCheck = false;
            }
        }
        // if player is grounded, they are no longer falling
        _hasNegativeYVelocity = false;
    }

    private void PlayerJump_OnJump(bool jumping)
    {
        if (jumping)
        {
            _animator.ResetTrigger("Landed");
            _animator.SetBool("Falling", false);
            CreateDustParticles();
            _animator.SetTrigger("Jumping");
        }
    }

    private void HandlePlayerStun_OnStunned(bool stunned)
    {
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

    private void HandlePlayerHealth_OnPlayerDead()
    {
        _animator.SetTrigger("Dead");
        Time.timeScale = 0.1f;
    }

    public void CreateDustParticles()
    {
        DustParticles.Play();
    }
}
