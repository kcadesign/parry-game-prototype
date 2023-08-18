using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControllerBase : MonoBehaviour
{
    public delegate void EnemyStateChange(bool inSightRange, bool inAttackRange);
    public event EnemyStateChange OnEnemyStateChange; 

    [Header("Layer Mask")]
    public LayerMask TargetLayer;

    [Header("Interaction Parameters")]
    public Transform SightCentre;
    public float SightRange;
    protected bool _targetInSightRange = false;

    public Transform AttackCentre;
    public float AttackRange;
    protected bool _targetInAttackRange = false;

    protected enum _enemyState
    {
        Idle,
        Transform,
        TransformIdle,
        Attack
    }

    protected _enemyState _currentState;

    protected void Start()
    {
        ChangeState(_enemyState.Idle);
    }

    protected void Update()
    {
        HandleState();
        //Debug.Log(_currentState);
    }

    protected void ChangeState(_enemyState newState)
    {
        _currentState = newState;
    }

    public void HandleState()
    {
        _targetInSightRange = Physics2D.OverlapCircle(SightCentre.position, SightRange, TargetLayer) != null;
        _targetInAttackRange = Physics2D.OverlapCircle(AttackCentre.position, AttackRange, TargetLayer) != null;

        if (!_targetInSightRange && !_targetInAttackRange) ChangeState(_enemyState.Idle);
        else if (_targetInSightRange && !_targetInAttackRange) ChangeState(_enemyState.TransformIdle);
        else if (_targetInSightRange && _targetInAttackRange) ChangeState(_enemyState.Attack);

        switch (_currentState)
        {
            case _enemyState.Idle:
                PerformIdleActions();
                OnEnemyStateChange?.Invoke(_targetInSightRange, _targetInAttackRange);
                break;
            case _enemyState.Transform:
                PerformTransformActions();
                OnEnemyStateChange?.Invoke(_targetInSightRange, _targetInAttackRange);
                break;
            case _enemyState.TransformIdle:
                PerformTransformIdleActions();
                OnEnemyStateChange?.Invoke(_targetInSightRange, _targetInAttackRange);
                break;
            case _enemyState.Attack:
                PerformAttackActions();
                OnEnemyStateChange?.Invoke(_targetInSightRange, _targetInAttackRange);
                break;
            default:
                PerformIdleActions();
                OnEnemyStateChange?.Invoke(_targetInSightRange, _targetInAttackRange);
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
