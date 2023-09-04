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

    //private bool _isParrying;

    private void Awake()
    {
        PlayerHealth = new HealthSystem(_maxHealth);
        _currentHealth = PlayerHealth.GetHealth();

        OnHealthChange?.Invoke(_currentHealth);
    }

    private void OnEnable()
    {
        //PlayerParry.OnParryActive += PlayerParry_OnParryActive;
        EnemyCollisionWithPlayer.OnDamagePlayer += EnemyCollisionWithPlayer_OnDamagePlayer;
        PlayerTriggerEnter.OnAreaDamagePlayer += PlayerTriggerEnter_OnAreaDamagePlayer;
    }


    private void OnDisable()
    {
        //PlayerParry.OnParryActive -= PlayerParry_OnParryActive;
        EnemyCollisionWithPlayer.OnDamagePlayer -= EnemyCollisionWithPlayer_OnDamagePlayer;
        PlayerTriggerEnter.OnAreaDamagePlayer -= PlayerTriggerEnter_OnAreaDamagePlayer;
    }

    private void EnemyCollisionWithPlayer_OnDamagePlayer(int damageAmount)
    {
        PlayerHealth.Damage(damageAmount);

        _currentHealth = PlayerHealth.GetHealth();
        OnHealthChange?.Invoke(_currentHealth);

        //print($"Player health is: {PlayerHealth.GetHealth()}");
    }    
    
    private void PlayerTriggerEnter_OnAreaDamagePlayer(int damageAmount)
    {
        PlayerHealth.Damage(damageAmount);

        _currentHealth = PlayerHealth.GetHealth();
        OnHealthChange?.Invoke(_currentHealth);
    }

    /*
    private void PlayerParry_OnParryActive(bool parryPressed)
    {
        _isParrying = parryPressed;
    }*/
}
