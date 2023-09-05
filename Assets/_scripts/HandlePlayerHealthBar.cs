using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandlePlayerHealthBar : MonoBehaviour
{
    public Image BarValueMask;
    private float _currentFill;
    private float _maxFill = 100;

    private void Awake()
    {
        _currentFill = _maxFill;
        GetSetCurrentFill();
    }

    private void OnEnable()
    {
        HandlePlayerHealth.OnHealthChange += HandlePlayerHealth_OnHealthChange;
    }

    private void OnDisable()
    {
        HandlePlayerHealth.OnHealthChange -= HandlePlayerHealth_OnHealthChange;
    }

    private void HandlePlayerHealth_OnHealthChange(int currentHealth)
    {
        //print($"Current health is: {currentHealth}");

        _currentFill = currentHealth;
        GetSetCurrentFill();
    }


    private void GetSetCurrentFill()
    {
        float currentFillPercentage = _currentFill / _maxFill;
        BarValueMask.fillAmount = currentFillPercentage;        
        //print($"Current fill is: {currentFillPercentage}");
    }
}
