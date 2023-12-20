using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1StateManager : MonoBehaviour
{
    public Boss1BaseState CurrentState;
    public Boss1IdleState IdleState = new Boss1IdleState();
    public Boss1AttackLeftState AttackLeftState = new Boss1AttackLeftState();
    public Boss1HitFromLeftState HitFromLeftState = new Boss1HitFromLeftState();
    public Boss1AttackRightState AttackRightState = new Boss1AttackRightState();
    public Boss1AttackBottomState AttackBottomState = new Boss1AttackBottomState();

    public CheckTriggerEntered TriggerZoneLeft;
    public CheckTriggerEntered TriggerZoneRight;
    public CheckTriggerEntered TriggerZoneBottom;

    public Animator Animator;

    /*[HideInInspector]*/ public bool CanAttackLeft = false;
    /*[HideInInspector]*/ public bool CanAttackRight = false;
    /*[HideInInspector]*/ public bool CanAttackBottom = false;
    /*[HideInInspector]*/ public bool Idle = false;

    public HandleHurtBoxCollisions LeftHurtBoxCollisions;
    public bool Deflected = false;

    public float AttackDelay = 2f;

    private void Awake()
    {
        Animator = GetComponent<Animator>();
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
        Deflected = LeftHurtBoxCollisions.Deflected;
        //Debug.Log($"Deflected: {Deflected}");

        ChooseAttackZone();

        CurrentState.UpdateState(this);
    }

    public void SwitchState(Boss1BaseState state)
    {
        CurrentState.SwitchState(this);
        CurrentState = state;
        state.EnterState(this);
    }

    private void ChooseAttackZone()
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

}
