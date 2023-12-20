using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckTriggerEntered : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private Color _originalColor;
    private Color _enteredColor;
    private Color _bossAttackTriggeredColor;

    private float _triggerEnteredTimer = 0f;
    public float AttackDelay;

    public bool CanAttack = false;



    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _originalColor = _spriteRenderer.color;
        _enteredColor = new Color(0f, 0f, 1f, 0.25f); 
        _bossAttackTriggeredColor = new Color(1f, 0f, 0f, 0.25f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            //Debug.Log($"Player entered {gameObject.name}");
            _spriteRenderer.color = _enteredColor;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Start timer
            _triggerEnteredTimer += Time.deltaTime;

            //Debug.Log($"Player is in {gameObject.name}");
            _spriteRenderer.color = _enteredColor;

            if(_triggerEnteredTimer >= AttackDelay)
            {
                //Debug.Log($"Player has been in {gameObject.name} for {_triggerEnteredDuration} seconds");
                CanAttack = true;
                _spriteRenderer.color = _bossAttackTriggeredColor;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Reset timer
            ResetTriggerEnteredTimer();

            CanAttack = false;

            //Debug.Log($"Player exited {gameObject.name}");
            _spriteRenderer.color = _originalColor;
        }
    }

    private void ResetTriggerEnteredTimer() => _triggerEnteredTimer = 0f;

    public float SetAttackDelay(float attackDelay) => AttackDelay = attackDelay;
}
