using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckTriggerEntered : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private Color _originalColor;
    private Color _enteredColor;
    private Color _bossAttackTriggeredColor;

    private float _triggerStayDuration = 0f;
    [HideInInspector] public float AttackDelay;

    [HideInInspector] public bool CanAttack = false;



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
            _triggerStayDuration += Time.deltaTime;

            //Debug.Log($"Player is in {gameObject.name}");
            _spriteRenderer.color = _enteredColor;

            if(_triggerStayDuration >= AttackDelay)
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
            _triggerStayDuration = 0f;

            CanAttack = false;

            //Debug.Log($"Player exited {gameObject.name}");
            _spriteRenderer.color = _originalColor;
        }
    }

    //public float SetAttackDelay(float attackDelay) => AttackDelay = attackDelay;
}
