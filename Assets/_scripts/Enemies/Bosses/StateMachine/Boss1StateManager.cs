using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1StateManager : MonoBehaviour
{
    [Header("States")]
    public Boss1BaseState CurrentState;
    public Boss1TrueIdleState TrueIdleState = new Boss1TrueIdleState();

    // Fists states
    public Boss1FistsIdleState FistsIdleState = new Boss1FistsIdleState();

    public Boss1AttackLeftState AttackLeftState = new Boss1AttackLeftState();
    public Boss1HitFromLeftState HitFromLeftState = new Boss1HitFromLeftState();

    public Boss1AttackRightState AttackRightState = new Boss1AttackRightState();
    public Boss1HitFromRightState HitFromRightState = new Boss1HitFromRightState();

    public Boss1AttackBottomState AttackBottomState = new Boss1AttackBottomState();
    public Boss1HitGenericState HitGenericState = new Boss1HitGenericState();

    public Boss1FistsDeathState FistsDeathState = new Boss1FistsDeathState();

    // Projectile states
    public Boss1BulletIdleState BulletIdleState = new Boss1BulletIdleState();

    public Boss1BulletAttackLeftState BulletAttackLeftState = new Boss1BulletAttackLeftState();

    [HideInInspector] public Animator Animator;

    [Header("Attack Zone Triggers")]
    public CheckTriggerEntered TriggerZoneLeft;
    public CheckTriggerEntered TriggerZoneRight;
    public CheckTriggerEntered TriggerZoneBottom;

    [HideInInspector] public bool CanAttackLeft = false;
    [HideInInspector] public bool CanAttackRight = false;
    [HideInInspector] public bool CanAttackBottom = false;
    /*[HideInInspector]*/ public bool TrueIdle = false;
    /*[HideInInspector]*/ public bool FistsIdle = false;
    /*[HideInInspector]*/ public bool BulletIdle = false;

    [Header("Hurt Box")]
    public HandleHurtBoxCollisions RightHurtBox;
    public HandleHurtBoxCollisions LeftHurtBox;

    [Header("Projectiles")]
    public SpawnProjectile NorthProjectileSpawner;
    public SpawnProjectile EastProjectileSpawner;
    public SpawnProjectile SouthProjectileSpawner;
    public SpawnProjectile WestProjectileSpawner;

    [Header("Attack Values")]
    public float AttackDelay = 2f;

    [Header("Phase Change")]
    public float PhaseChangeDelay = 5f;
    public float PhaseChangeCountdown;

    [Header("Health")]
    [HideInInspector] public bool BossDead = false;

    private void Awake()
    {
        Animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        HandleBossHealth.OnBossDeath += HandleBossHealth_OnBossDeath;
    }

    private void OnDisable()
    {
        HandleBossHealth.OnBossDeath -= HandleBossHealth_OnBossDeath;
    }

    void Start()
    {
        TriggerZoneLeft.SetAttackDelay(AttackDelay);
        TriggerZoneRight.SetAttackDelay(AttackDelay);
        TriggerZoneBottom.SetAttackDelay(AttackDelay);

        PhaseChangeCountdown = PhaseChangeDelay;

        CurrentState = TrueIdleState;
        CurrentState.EnterState(this);
    }

    void Update()
    {
        DecideAttackZone();
        PhaseChangeCountdownTimer();

        CurrentState.UpdateState(this);
    }

    public void SwitchState(Boss1BaseState state)
    {
        CurrentState.SwitchState(this);
        CurrentState = state;
        state.EnterState(this);
    }

    private void DecideAttackZone()
    {
        if (TriggerZoneLeft.CanAttack)
        {
            CanAttackLeft = true;
            CanAttackRight = false;
            CanAttackBottom = false;

            /*TrueIdle = false;
            FistsIdle = false;
            BulletIdle = false;*/
        }
        else if (TriggerZoneRight.CanAttack)
        {
            CanAttackLeft = false;
            CanAttackRight = true;
            CanAttackBottom = false;

            /*TrueIdle = false;
            FistsIdle = false;
            BulletIdle = false;*/
        }
        else if (TriggerZoneBottom.CanAttack)
        {
            CanAttackLeft = false;
            CanAttackRight = false;
            CanAttackBottom = true;

            /*TrueIdle = false;
            FistsIdle = false;
            BulletIdle = false;*/
        }
        else
        {
            CanAttackLeft = false;
            CanAttackRight = false;
            CanAttackBottom = false;
            /*TrueIdle = true;*/
            DecideBossPhase();
        }
    }

    private void HandleBossHealth_OnBossDeath(GameObject bossParentObject) => BossDead = true;

    public void DecideBossPhase()
    {
        if (PhaseChangeCountdown <= 0)
        {
            // roll for phase change only when timer hits zero, then reset timer
            int phase = RollForPhase();
            Debug.Log($"Random roll: {phase}");

            if (phase == 1)
            {
                // switch to projectile idle state
                FistsIdle = false;
                BulletIdle = true;
            }
            else // Assuming it's either 1 or 0, so no need to check again
            {
                // switch to fists idle state
                FistsIdle = true;
                BulletIdle = false;
            }

            PhaseChangeCountdown = PhaseChangeDelay;
        }
    }


    public void PhaseChangeCountdownTimer()
    {
        // Countdown phase change timer to zero in seconds
        PhaseChangeCountdown -= Time.deltaTime;
        //Debug.Log($"Phase change countdown: {PhaseChangeCountdown}");
    }

    public int RollForPhase()
    {
        // return a random integer of 0 or 1
        return Random.Range(0, 2);
    }

    private void EnableNorthProjectiles() => NorthProjectileSpawner.InvokeProjectile();
    private void EnableEastProjectiles() => EastProjectileSpawner.InvokeProjectile();
    private void EnableSouthProjectiles() => SouthProjectileSpawner.InvokeProjectile();
    private void EnableWestProjectiles() => WestProjectileSpawner.InvokeProjectile();

}
