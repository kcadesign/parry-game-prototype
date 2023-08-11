using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleEnemyAnimationAOE : MonoBehaviour
{
    private Animator _enemyStateAnimator;

    private EnemyControllerAOE _enemyControllerAOE;

    private bool _targetInSightRange;
    private bool _targetInAttackRange;

    private void Awake()
    {        
        _enemyControllerAOE = GetComponent<EnemyControllerAOE>();

        _enemyStateAnimator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _enemyControllerAOE.OnEnemyStateChange += EnemyControllerAOE_OnEnemyStateChange;
    }

    private void OnDisable()
    {
        _enemyControllerAOE.OnEnemyStateChange -= EnemyControllerAOE_OnEnemyStateChange;
    }

    private void EnemyControllerAOE_OnEnemyStateChange(bool inSightRange, bool inAttackRange)
    {
        _targetInSightRange = inSightRange;
        _targetInAttackRange = inAttackRange;
    }

    void Update()
    {
        if (!_targetInSightRange && !_targetInAttackRange) 
        {
            _enemyStateAnimator.SetBool("Idle", true);
            _enemyStateAnimator.SetBool("Transform", false);
            _enemyStateAnimator.SetBool("Attack", false);
        } 
        else if (_targetInSightRange && !_targetInAttackRange)
        {
            _enemyStateAnimator.SetBool("Idle", false);
            _enemyStateAnimator.SetBool("Transform", true);
            _enemyStateAnimator.SetBool("Attack", false);
        }
        else if (_targetInSightRange && _targetInAttackRange)
        {
            _enemyStateAnimator.SetBool("Idle", false);
            _enemyStateAnimator.SetBool("Transform", false);
            _enemyStateAnimator.SetBool("Attack", true);
        }
    }
}
