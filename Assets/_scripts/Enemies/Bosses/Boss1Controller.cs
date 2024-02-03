using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Controller : MonoBehaviour
{
    [HideInInspector] public Animator Animator;

    [Header("Attack Zone Triggers")]
    public CheckTriggerEntered TriggerZoneLeft;
    public CheckTriggerEntered TriggerZoneRight;
    public CheckTriggerEntered TriggerZoneBottom;

    [HideInInspector] public bool CanAttackLeft = false;
    [HideInInspector] public bool CanAttackRight = false;
    [HideInInspector] public bool CanAttackBottom = false;

    [HideInInspector] public bool IsAttacking = false;

    /*[HideInInspector]*/
    public bool FistsIdle = false;
    /*[HideInInspector]*/
    public bool BulletIdle = false;

    [Header("Hurt Box")]
    public HandleHurtBoxCollisions LeftHurtBox;
    public HandleHurtBoxCollisions RightHurtBox;
    public bool LeftFistDeflected = false;
    public bool RightFistDeflected = false;

    [Header("Projectiles")]
    public SpawnProjectile NorthProjectileSpawner;
    public SpawnProjectile EastProjectileSpawner;
    public SpawnProjectile SouthProjectileSpawner;
    public SpawnProjectile WestProjectileSpawner;

    [Header("Attack Values")]
    public float AttackDelay = 2f;

    [Header("Phase Change")]
    //public float PhaseChangeDelay = 5f;
    public float MinPhaseLength = 5f;
    public float MaxPhaseLength = 10f;
    public float PhaseChangeCountdown = 0f;
    //public bool CanChangePhase = false;

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

        FistsIdle = true;
    }

    void Update()
    {
        DecideAttackZone();
        CountdownPhaseChange();
        CheckDeflected();

        //Debug.Log($"Right fist deflected: {RightHurtBox.Deflected}");
    }

    private void DecideAttackZone()
    {
        if (TriggerZoneLeft.CanAttack)
        {
            CanAttackLeft = true;
            CanAttackRight = false;
            CanAttackBottom = false;
        }
        else if (TriggerZoneRight.CanAttack)
        {
            CanAttackLeft = false;
            CanAttackRight = true;
            CanAttackBottom = false;
        }
        else if (TriggerZoneBottom.CanAttack)
        {
            CanAttackLeft = false;
            CanAttackRight = false;
            CanAttackBottom = true;
        }
        else
        {
            CanAttackLeft = false;
            CanAttackRight = false;
            CanAttackBottom = false;
            DecideBossPhase();
        }
    }

    private void HandleBossHealth_OnBossDeath(GameObject bossParentObject) => BossDead = true;

    public void DecideBossPhase()
    {
        if (PhaseChangeCountdown <= 0 /*&& CanChangePhase*/)
        {
            Debug.Log("Deciding boss phase");

            PhaseChange();

            PhaseChangeCountdown = Random.Range(MinPhaseLength, MaxPhaseLength);
        }
    }

    public void CountdownPhaseChange()
    {
        // Countdown phase change timer to zero in seconds
        PhaseChangeCountdown -= Time.deltaTime;
    }

    public float RollForPhase()
    {
        return Random.Range(MinPhaseLength, MaxPhaseLength);
    }

    public void PhaseChange()
    {
        FistsIdle = !FistsIdle;
        BulletIdle = !BulletIdle;
    }

    public void CheckBossAttacking()
    {
        if (CanAttackLeft || CanAttackRight || CanAttackBottom)
        {
            IsAttacking = true;
        }
        else
        {
            IsAttacking = false;
        }
    }

    public void CheckDeflected()
    {
        LeftFistDeflected = LeftHurtBox.Deflected;
        RightFistDeflected = RightHurtBox.Deflected;
    }

    private void EnableNorthProjectiles() => NorthProjectileSpawner.InvokeProjectile();
    private void EnableEastProjectiles() => EastProjectileSpawner.InvokeProjectile();
    private void EnableSouthProjectiles() => SouthProjectileSpawner.InvokeProjectile();
    private void EnableWestProjectiles() => WestProjectileSpawner.InvokeProjectile();

}
