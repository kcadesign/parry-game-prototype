using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleBaseEnemyAnimation : MonoBehaviour
{
    //public SpriteRenderer[] BodySprites;
    public HandleEnemyBodyCollisions CollisionHandler;

    private Animator _enemyStateAnimator;

    //public ParticleSystem DestructionParticle;

    private IHandleState _enemyController;

    protected virtual void Awake()
    {
        _enemyStateAnimator = GetComponent<Animator>();
        _enemyController = GetComponent<IHandleState>();
    }

    private void OnEnable()
    {
        _enemyController.OnHandleState += _enemyController_OnEnemyStateChange;
        //HandleDamageOut.OnOutputDamage += HandleDamageOutput_OnOutputDamage;
        HandleEnemyHealth.OnEnemyDeath += HandleEnemyHealth_OnEnemyDeath;
    }

    private void OnDisable()
    {
        _enemyController.OnHandleState -= _enemyController_OnEnemyStateChange;
        //HandleDamageOut.OnOutputDamage -= HandleDamageOutput_OnOutputDamage;
        HandleEnemyHealth.OnEnemyDeath -= HandleEnemyHealth_OnEnemyDeath;
    }

    private void _enemyController_OnEnemyStateChange(System.Enum enemyState)
    {
        //Debug.Log($"Current enemy state: {enemyState}");

        // Cast the enum to the correct type (EnemyState)
        HandleBaseEnemyState.EnemyState state = (HandleBaseEnemyState.EnemyState)enemyState;

        switch (state)
        {
            case HandleBaseEnemyState.EnemyState.Idle:
                _enemyStateAnimator.SetBool("Idle", true);
                _enemyStateAnimator.SetBool("Transform", false);
                _enemyStateAnimator.SetBool("Attack", false);
                break;
            case HandleBaseEnemyState.EnemyState.Transform:
                _enemyStateAnimator.SetBool("Idle", false);
                _enemyStateAnimator.SetBool("Transform", true);
                _enemyStateAnimator.SetBool("Attack", false);
                break;
            case HandleBaseEnemyState.EnemyState.TransformIdle:
                _enemyStateAnimator.SetBool("Idle", false);
                _enemyStateAnimator.SetBool("Transform", true);
                _enemyStateAnimator.SetBool("Attack", false);
                break;
            case HandleBaseEnemyState.EnemyState.Attack:
                _enemyStateAnimator.SetBool("Idle", false);
                _enemyStateAnimator.SetBool("Transform", false);
                _enemyStateAnimator.SetBool("Attack", true);
                break;
            default:
                _enemyStateAnimator.SetBool("Idle", true);
                _enemyStateAnimator.SetBool("Transform", false);
                _enemyStateAnimator.SetBool("Attack", false);
                break;
        }
    }
    /*
    private void HandleDamageOutput_OnOutputDamage(GameObject collisionObject, int damageAmount)
    {
        if (collisionObject.transform.IsChildOf(transform))
        {/*
            //Animate take damage here
            _enemyStateAnimator.SetTrigger("TakeDamage");
            
            foreach (SpriteRenderer spriteRenderer in BodySprites)
            {
                StartCoroutine(FlashEnemy(spriteRenderer));
            }
        }
    }
    
    protected virtual void OnDestroy()
    {
        if (DestructionParticle != null)
        {
            Instantiate(DestructionParticle, transform.position, Quaternion.identity).Play();
        }
    }
    
    private IEnumerator FlashEnemy(SpriteRenderer spriteRenderer)
    {
        spriteRenderer.color = new Color(0, 0, 100, 0);
        yield return new WaitForSeconds(0.15f);
        spriteRenderer.color = new Color(0, 0, 100, 100);
    }
    */
    private void HandleEnemyHealth_OnEnemyDeath(GameObject deadEnemy)
    {
        if(deadEnemy == gameObject)
        {
            //Debug.Log($"Enemy death message recieved by animator script");

            _enemyStateAnimator.SetTrigger("Dead");
        }
    }
}
