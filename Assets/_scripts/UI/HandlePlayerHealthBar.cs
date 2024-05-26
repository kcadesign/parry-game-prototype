using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandlePlayerHealthBar : MonoBehaviour
{
    public Image BarValueMask;
    private float _currentFill;
    private float _maxFill = 100;
    [SerializeField] private float _fillSpeed = 1.0f;

    private void Awake()
    {
        _currentFill = _maxFill;
        GetSetCurrentFill();
    }

    private void OnEnable()
    {
        HandlePlayerHealth.OnHealthInitialise += HandlePlayerHealth_OnHealthInitialise;
        HandlePlayerHealth.OnHealthChange += HandlePlayerHealth_OnHealthChange;
    }

    private void OnDisable()
    {
        HandlePlayerHealth.OnHealthInitialise -= HandlePlayerHealth_OnHealthInitialise;
        HandlePlayerHealth.OnHealthChange -= HandlePlayerHealth_OnHealthChange;
    }

    private void HandlePlayerHealth_OnHealthInitialise(int maxHealth)
    {
        _maxFill = maxHealth;
        _currentFill = maxHealth;
        GetSetCurrentFill();
    }

    private void HandlePlayerHealth_OnHealthChange(int currentHealth)
    {
        //Debug.Log($"Current health is: {currentHealth}");

        // Use a coroutine to smoothly update the fill amount
        StartCoroutine(ChangeFillOverTime(currentHealth));
    }

    private IEnumerator ChangeFillOverTime(float targetFill)
    {
        float initialFill = BarValueMask.fillAmount;
        float timer = 0;

        while (timer < 1.0f)
        {
            timer += Time.deltaTime * _fillSpeed;
            BarValueMask.fillAmount = Mathf.Lerp(initialFill, targetFill / _maxFill, timer);
            yield return null;
        }

        // Ensure the fill amount is exactly the target value when the animation ends
        BarValueMask.fillAmount = targetFill / _maxFill;
    }

    private void GetSetCurrentFill()
    {
        float currentFillPercentage = _currentFill / _maxFill;
        BarValueMask.fillAmount = currentFillPercentage;
    }
}
