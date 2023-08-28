using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandlePlayerHealth : MonoBehaviour
{
    public delegate void PlayerHealthChange(int currentHealth);
    public static event PlayerHealthChange OnHealthChange;

    public HealthSystem PlayerHealth;

    [SerializeField] private int _maxHealth = 5;
    private int _currentHealth;

    private bool _isParrying;

    private void Awake()
    {
        PlayerHealth = new HealthSystem(_maxHealth);
        _currentHealth = PlayerHealth.GetHealth();

        OnHealthChange?.Invoke(PlayerHealth.GetHealth());
    }

    private void OnEnable()
    {
        PlayerParry.OnParryActive += PlayerParry_OnParryActive;
        EnemyCollisionWithPlayer.OnDamagePlayer += EnemyCollisionWithPlayer_OnDamagePlayer;
    }

    private void OnDisable()
    {
        PlayerParry.OnParryActive -= PlayerParry_OnParryActive;
        EnemyCollisionWithPlayer.OnDamagePlayer -= EnemyCollisionWithPlayer_OnDamagePlayer;
    }
    private void EnemyCollisionWithPlayer_OnDamagePlayer(int damageAmount)
    {
        PlayerHealth.Damage(damageAmount);
        //print($"Player health is: {PlayerHealth.GetHealth()}");
    }

    private void PlayerParry_OnParryActive(bool parryPressed)
    {
        _isParrying = parryPressed;
    }

    void Update()
    {
        _currentHealth = PlayerHealth.GetHealth();
    }
}
