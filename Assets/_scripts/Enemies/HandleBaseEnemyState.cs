using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleBaseEnemyState : MonoBehaviour, IHandleState
{
    public event Action<Enum> OnHandleState;

    [Header("Layer Mask")]
    public LayerMask TargetLayer;

    [Header("Interaction Parameters")]
    public Transform SightCentre;
    public float SightRange;
    protected bool _targetInSightRange = false;

    public Transform AttackCentre;
    public float AttackRange;
    protected bool _targetInAttackRange = false;

    public enum EnemyState
    {
        Idle,
        Transform,
        TransformIdle,
        Attack
    }

    protected EnemyState _currentState;

    protected void Start()
    {
        ChangeState(EnemyState.Idle);
    }

    protected void Update()
    {
        HandleState();
        //Debug.Log(_currentState);
    }

    protected void ChangeState(EnemyState newState)
    {
        _currentState = newState;
    }

    public void HandleState()
    {
        _targetInSightRange = Physics2D.OverlapCircle(SightCentre.position, SightRange, TargetLayer) != null;
        _targetInAttackRange = Physics2D.OverlapCircle(AttackCentre.position, AttackRange, TargetLayer) != null;

        if (!_targetInSightRange && !_targetInAttackRange) ChangeState(EnemyState.Idle);
        else if (_targetInSightRange && !_targetInAttackRange) ChangeState(EnemyState.TransformIdle);
        else if (_targetInSightRange && _targetInAttackRange) ChangeState(EnemyState.Attack);
        else ChangeState(EnemyState.Idle);

        switch (_currentState)
        {
            case EnemyState.Idle:
                PerformIdleActions();
                OnHandleState?.Invoke(_currentState);
                break;
            case EnemyState.Transform:
                PerformTransformActions();
                OnHandleState?.Invoke(_currentState);
                break;
            case EnemyState.TransformIdle:
                PerformTransformIdleActions();
                OnHandleState?.Invoke(_currentState);
                break;
            case EnemyState.Attack:
                PerformAttackActions();
                OnHandleState?.Invoke(_currentState);
                break;
            default:
                PerformIdleActions();
                OnHandleState?.Invoke(_currentState);
                break;
        }
    }
    
    protected virtual void PerformIdleActions()
    {
        
    }

    protected virtual void PerformTransformActions()
    {

    }

    protected virtual void PerformTransformIdleActions()
    {

    }

    protected virtual void PerformAttackActions()
    {

    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(AttackCentre.position, AttackRange);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(SightCentre.position, SightRange);
    }
}
