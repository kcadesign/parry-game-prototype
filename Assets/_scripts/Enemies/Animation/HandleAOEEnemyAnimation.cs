using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleAOEEnemyAnimation : MonoBehaviour
{
    private Animator _enemyStateAnimator;

    private AOEEnemyController _enemyController;

    private bool _targetInSightRange;
    private bool _targetInAttackRange;

    private void Awake()
    {        
        _enemyStateAnimator = GetComponent<Animator>();

        _enemyController = GetComponent<AOEEnemyController>();
    }

    private void OnEnable()
    {
        _enemyController.OnEnemyStateChange += _enemyController_OnEnemyStateChange;
    }

    private void OnDisable()
    {
        _enemyController.OnEnemyStateChange -= _enemyController_OnEnemyStateChange;
    }

    private void _enemyController_OnEnemyStateChange(bool inSightRange, bool inAttackRange)
    {
        //Debug.Log($"Player is in sight range: {inSightRange}");
        //Debug.Log($"Player is in attack range: {inAttackRange}");

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
        // Else revert to idle
        else
        {
            _enemyStateAnimator.SetBool("Idle", true);
            _enemyStateAnimator.SetBool("Transform", false);
            _enemyStateAnimator.SetBool("Attack", false);
        }
    }
}
