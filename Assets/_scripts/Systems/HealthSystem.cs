using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem
{
    private int _currentHealth;
    private int _healthMax;

    public HealthSystem(int healthMax)
    {
        this._currentHealth = healthMax;
        _currentHealth = healthMax;
    }

    public int GetHealth()
    {
        return _currentHealth;
    }

    public void Damage(int damageAmount)
    {
        _currentHealth -= damageAmount;
        
        if (_currentHealth < 0)
        {
            _currentHealth = 0;
        }
    }

    public void Heal(int healAmount)
    {
        _currentHealth += healAmount;

        if (_currentHealth > _healthMax)
        {
            _currentHealth = _healthMax;
        }
    }

    public void ChangeHealth(int healthAmount)
    {
        _currentHealth = healthAmount;
    }
}