using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1StateManager : MonoBehaviour
{
    public Boss1BaseState _currentState;
    public Boss1IdleState _idleState = new Boss1IdleState();
    public Boss1AttackLeftState _attackLeftState = new Boss1AttackLeftState();
    public Boss1AttackRightState _attackRightState = new Boss1AttackRightState();
    public Boss1AttackBottomState _attackBottomState = new Boss1AttackBottomState();

    public CheckTriggerEntered TriggerZoneLeft;
    public CheckTriggerEntered TriggerZoneRight;
    public CheckTriggerEntered TriggerZoneBottom;

    public Animator _animator;

    [HideInInspector] public bool CanAttackLeft = false;
    [HideInInspector] public bool CanAttackRight = false;
    [HideInInspector] public bool CanAttackBottom = false;

    public float AttackDelay = 2f;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    void Start()
    {
        _currentState = _idleState;

        _currentState.EnterState(this);
    }

    void Update()
    {
        ChooseAttackZone();

        _currentState.UpdateState(this);
    }

    public void SwitchState(Boss1BaseState state)
    {
        _currentState = state;
        state.EnterState(this);
    }

    private void ChooseAttackZone()
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
        }
    }

}
