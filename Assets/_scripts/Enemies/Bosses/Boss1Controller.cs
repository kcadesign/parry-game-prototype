using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Controller : MonoBehaviour
{
    public CheckTriggerEntered TriggerZoneLeft;
    public CheckTriggerEntered TriggerZoneRight;
    public CheckTriggerEntered TriggerZoneBottom;

    public float AttackDelay = 2f;

    public enum Boss1State
    {
        Idle,
        AttackLeft,
        AttackRight,
        AttackBottom,
        SwitchAttackType
    }

    private Boss1State _currentState;

    private void Start()
    {
        TriggerZoneLeft.SetAttackDelay(AttackDelay);
        TriggerZoneRight.SetAttackDelay(AttackDelay);
        TriggerZoneBottom.SetAttackDelay(AttackDelay);
    }

    private void Update()
    {
        HandleState();
    }

    private void HandleState()
    {
        if (TriggerZoneLeft.CanAttack) _currentState = Boss1State.AttackLeft;
        else if (TriggerZoneRight.CanAttack) _currentState = Boss1State.AttackRight;
        else if (TriggerZoneBottom.CanAttack) _currentState = Boss1State.AttackBottom;
        else _currentState = Boss1State.Idle;

        switch (_currentState)
        {
            case Boss1State.Idle:
                // Boss performs idle actions
                break;
            case Boss1State.AttackLeft:
                //Boss performs attack left actions
                break;
            case Boss1State.AttackRight:
                //Boss performs attack right actions
                break;
            case Boss1State.AttackBottom:
                //Boss performs attack bottom actions
                break;
            case Boss1State.SwitchAttackType:
                break;
            default:
                break;
        }
    }
}
