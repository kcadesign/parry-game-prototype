using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Controller : MonoBehaviour
{
    [HideInInspector] public Animator Animator;

    [Header("Attack Zone Triggers & State")]
    public CheckTriggerEntered TriggerZoneLeft;
    public CheckTriggerEntered TriggerZoneRight;
    public CheckTriggerEntered TriggerZoneBottom;

    [HideInInspector] public bool CanAttackLeft = false;
    [HideInInspector] public bool CanAttackRight = false;
    [HideInInspector] public bool CanAttackBottom = false;

    //[HideInInspector] public bool IsAttacking = false;

    public bool FistsIdle = false;
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
    public float MinPhaseLength = 5f;
    public float MaxPhaseLength = 10f;
    public float PhaseChangeCountdown = 0f;

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

    public void DecideBossPhase()
    {
        if (PhaseChangeCountdown <= 0)
        {
            PhaseChange();
            PhaseChangeCountdown = Random.Range(MinPhaseLength, MaxPhaseLength);
        }
    }

    public void CountdownPhaseChange() => PhaseChangeCountdown -= Time.deltaTime;

    public float RollForPhase() => Random.Range(MinPhaseLength, MaxPhaseLength);

    public void PhaseChange()
    {
        FistsIdle = !FistsIdle;
        BulletIdle = !BulletIdle;
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

    private void HandleBossHealth_OnBossDeath(GameObject bossParentObject) => BossDead = true;

}
