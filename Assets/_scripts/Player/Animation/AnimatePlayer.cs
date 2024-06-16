using System;
using System.Collections;
using UnityEngine;

public class AnimatePlayer : MonoBehaviour
{
    public static event Action OnPassiveBounce;
    public static event Action OnPlayerDeathAnimEnd;

    private Rigidbody2D PlayerRigidbody;
    private Animator _animator;
    public ParticleSystem DustParticles;
    public SpriteRenderer PlayerSprite;
    public Sprite DeathSprite;
    public GameObject Shadow;

    private bool _grounded;
    private bool _hasNegativeYVelocity;

    private bool _playerDead = false;

    private bool _localGroundedCheck = false;

    public bool SlowTime;
    [Range(0.1f, 1.0f)] public float TimeScale = 0.1f;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        PlayerRigidbody = GetComponent<Rigidbody2D>();
        Shadow = transform.Find("Shadow").gameObject;
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

    private void Update()
    {
        // debug for slowing time to check animations
        if (SlowTime)
        {
            Time.timeScale = TimeScale;
        }

        if (_playerDead)
        {
            Time.timeScale = 0f;
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
        if (_hasNegativeYVelocity && !_grounded)
        {
            _animator.SetBool("Falling", true);
            Debug.Log("Falling bool set");
        }
        else
        {
            _animator.SetBool("Falling", false);
        }
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
        _playerDead = true;
        Debug.Log("Player dead observed in animation script");

        _animator.SetBool("Falling", false);
        _animator.SetTrigger("Dead");

        StartCoroutine(PlayerDeathMovement());
        StartCoroutine(PlayerDeathRotation());
    }

    public void CreateDustParticles()
    {
        DustParticles.Play();
    }

    private IEnumerator PlayerDeathMovement()
    {
        Debug.Log("Player death movement coroutine started");
        PlayerSprite.sprite = DeathSprite;
        gameObject.transform.localScale = new Vector3(1, 1, 1);
        Shadow.SetActive(false);

        yield return new WaitForSecondsRealtime(1f);
        LeanTween.moveY(gameObject, transform.position.y + 2, 0.5f).setEaseOutExpo().setIgnoreTimeScale(true);
        yield return new WaitWhile(() => LeanTween.isTweening(gameObject));
        LeanTween.moveY(gameObject, transform.position.y - 7, 0.75f).setEaseInExpo().setIgnoreTimeScale(true);
        yield return new WaitWhile(() => LeanTween.isTweening(gameObject));
        OnPlayerDeathAnimEnd?.Invoke();
    }

    private IEnumerator PlayerDeathRotation()
    {
        yield return new WaitForSecondsRealtime(1f);
        while (true)
        {
            transform.Rotate(Vector3.forward, 180f * Time.unscaledDeltaTime);
            yield return null; // Wait for the next frame
        }
    }
}
