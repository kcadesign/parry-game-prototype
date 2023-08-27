using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollisionWithPlayer : MonoBehaviour
{
    public delegate void DamagePlayer(int damageAmount);
    public static event DamagePlayer OnDamagePlayer;

    private bool _isParrying;
    [SerializeField] private int _damageAmount = 5;
    [SerializeField] private float _damageForce = 5;

    private void OnEnable()
    {
        PlayerParry.OnParryActive += PlayerParry_OnParryActive;
    }

    private void OnDisable()
    {
        PlayerParry.OnParryActive -= PlayerParry_OnParryActive;
    }

    private void PlayerParry_OnParryActive(bool parryPressed)
    {
        _isParrying = parryPressed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !_isParrying)
        {
            // Damage value is set here and sent to be subtracted from player health
            OnDamagePlayer?.Invoke(_damageAmount);

            Vector2 enemyCenter = transform.position;
            Vector2 playerCenter = collision.transform.position;

            // Determine the direction to apply force based on player's relative position to enemy
            Vector2 forceDirection = (playerCenter - enemyCenter).normalized;

            collision.rigidbody.AddForce(forceDirection * _damageForce, ForceMode2D.Impulse);
        }
    }
}
