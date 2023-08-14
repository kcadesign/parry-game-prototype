using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleEnemyAnimation : MonoBehaviour
{
    private Animator _enemyStateAnimator;

    private EnemyControllerBase _enemyController;

    private bool _targetInSightRange;
    private bool _targetInAttackRange;

    private void Awake()
    {        
        _enemyController = GetComponent<EnemyControllerBase>();

        _enemyStateAnimator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _enemyController.OnEnemyStateChange += EnemyControllerAOE_OnEnemyStateChange;
    }

    private void OnDisable()
    {
        _enemyController.OnEnemyStateChange -= EnemyControllerAOE_OnEnemyStateChange;
    }

    private void EnemyControllerAOE_OnEnemyStateChange(bool inSightRange, bool inAttackRange)
    {
        _targetInSightRange = inSightRange;
        _targetInAttackRange = inAttackRange;
    }

    void Update()
    {
        // No target - Idle animation
        if (!_targetInSightRange && !_targetInAttackRange) 
        {
            _enemyStateAnimator.SetBool("Idle", true);
            _enemyStateAnimator.SetBool("Transform", false);
            _enemyStateAnimator.SetBool("Attack", false);
        }
        // Target aquired but cant attack - Transform animation
        else if (_targetInSightRange && !_targetInAttackRange)
        {
            _enemyStateAnimator.SetBool("Idle", false);
            _enemyStateAnimator.SetBool("Transform", true);
            _enemyStateAnimator.SetBool("Attack", false);
        }
        // Target in attack range - Attack animation
        else if (_targetInSightRange && _targetInAttackRange)
        {
            _enemyStateAnimator.SetBool("Idle", false);
            _enemyStateAnimator.SetBool("Transform", false);
            _enemyStateAnimator.SetBool("Attack", true);
        }
    }
}
