using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleBaseEnemyAnimation : MonoBehaviour
{    
    public SpriteRenderer[] BodySprites;
    public HandleEnemyCollisions CollisionHandler;

    private Animator _enemyStateAnimator;
    [SerializeField] private EnemyControllerBase _enemyController;

    protected virtual void Awake()
    {
        _enemyStateAnimator = GetComponent<Animator>();
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
        HandleSightAttackAnimation(inSightRange, inAttackRange);
    }

    private void HandleSightAttackAnimation(bool inSightRange, bool inAttackRange)
    {
        // No target - Idle animation
        if (!inSightRange && !inAttackRange)
        {
            _enemyStateAnimator.SetBool("Idle", true);
            _enemyStateAnimator.SetBool("Transform", false);
            _enemyStateAnimator.SetBool("Attack", false);
        }
        // Target aquired but cant attack - Transform animation
        else if (inSightRange && !inAttackRange)
        {
            _enemyStateAnimator.SetBool("Idle", false);
            _enemyStateAnimator.SetBool("Transform", true);
            _enemyStateAnimator.SetBool("Attack", false);
        }
        // Target in attack range - Attack animation
        else if (inSightRange && inAttackRange)
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

    protected virtual void Update()
    {
        if (CollisionHandler.EnemyHit)
        {
            foreach (SpriteRenderer spriteRenderer in BodySprites)
            {
                StartCoroutine(FlashEnemy(spriteRenderer));
            }
        }
        else
        {
            return;
        }
    }

    private IEnumerator FlashEnemy(SpriteRenderer spriteRenderer)
    {
        spriteRenderer.color = new Color(0, 0, 100, 0);
        yield return new WaitForSeconds(0.15f);
        spriteRenderer.color = new Color(0, 0, 100, 100);
    }
}
