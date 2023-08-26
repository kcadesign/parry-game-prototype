using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem
{
    private int _health;
    private int _healthMax;

    public HealthSystem(int healthMax)
    {
        this._health = healthMax;
        _health = healthMax;
    }

    public int GetHealth()
    {
        return _health;
    }

    public void Damage(int damageAmount)
    {
        _health -= damageAmount;

        if (_health < 0)
        {
            _health = 0;
        }
    }

    public void Heal(int healAmount)
    {
        _health += healAmount;

        if (_health > _healthMax)
        {
            _health = _healthMax;
        }
    }
}