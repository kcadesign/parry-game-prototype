using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1StateManager : MonoBehaviour
{
    [Header("States")]
    public Boss1BaseState CurrentState;
    public Boss1IdleState IdleState = new Boss1IdleState();

    public Boss1AttackLeftState AttackLeftState = new Boss1AttackLeftState();
    public Boss1HitFromLeftState HitFromLeftState = new Boss1HitFromLeftState();

    public Boss1AttackRightState AttackRightState = new Boss1AttackRightState();
    public Boss1HitFromRightState HitFromRightState = new Boss1HitFromRightState();

    public Boss1AttackBottomState AttackBottomState = new Boss1AttackBottomState();
    public Boss1HitGenericState HitGeneric = new Boss1HitGenericState();

    public Boss1FistsDeathState FistsDeathState = new Boss1FistsDeathState();

    [HideInInspector] public Animator Animator;

    [Header("Attack Zone Triggers")]
    public CheckTriggerEntered TriggerZoneLeft;
    public CheckTriggerEntered TriggerZoneRight;
    public CheckTriggerEntered TriggerZoneBottom;

    [HideInInspector] public bool CanAttackLeft = false;
    [HideInInspector] public bool CanAttackRight = false;
    [HideInInspector] public bool CanAttackBottom = false;
    [HideInInspector] public bool Idle = false;

    [Header("Hurt Box")]
    public HandleHurtBoxCollisions RightHurtBoxCollisions;
    public HandleHurtBoxCollisions LeftHurtBoxCollisions;

    [Header("Attack")]
    public float AttackDelay = 2f;

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

        CurrentState = IdleState;
        CurrentState.EnterState(this);
    }

    void Update()
    {
        DecideAttackZone();

        CurrentState.UpdateState(this);        
        
        //Debug.Log($"State manager Right Deflected: {RightHurtBoxCollisions.Deflected}");
        //Debug.Log($"State manager Left Deflected: {LeftHurtBoxCollisions.Deflected}");

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
            Idle = false;
        }
        else if (TriggerZoneRight.CanAttack)
        {
            CanAttackLeft = false;
            CanAttackRight = true;
            CanAttackBottom = false;
            Idle = false;
        }
        else if (TriggerZoneBottom.CanAttack)
        {
            CanAttackLeft = false;
            CanAttackRight = false;
            CanAttackBottom = true;
            Idle = false;
        }
        else
        {
            CanAttackLeft = false;
            CanAttackRight = false;
            CanAttackBottom = false;
            Idle = true;
        }
    }

    private void HandleBossHealth_OnBossDeath(GameObject bossParentObject) => BossDead = true;

}
