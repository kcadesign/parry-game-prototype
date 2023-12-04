using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandleBossHealthBar : MonoBehaviour
{
    private Image _barValueMask;
    private float _currentFill;
    private float _maxFill;
    [SerializeField] private float _fillSpeed = 1.0f;

    private void Awake()
    {
        Debug.Log("Awake");
        _barValueMask = GetComponent<Image>();
        SetInitialFill();
    }
    
    private void OnEnable()
    {
        Debug.Log("Enable");

        HandleBossHealth.OnBossHealthChange += HandleBossHealth_OnBossHealthChange;
    }

    private void OnDisable()
    {
        HandleBossHealth.OnBossHealthChange -= HandleBossHealth_OnBossHealthChange;
    }

    private void HandleBossHealth_OnBossHealthChange(int currentHealth, int maxHealth)
    {
        Debug.Log("Boss health change");

        _maxFill = maxHealth;
        _currentFill = currentHealth;

        Debug.Log($"MAX boss health: {maxHealth}");
        Debug.Log($"Current boss health: {currentHealth}");

        _barValueMask.fillAmount = _currentFill / _maxFill;

        StartCoroutine(ChangeFillOverTime(currentHealth));
    }
    
    private IEnumerator ChangeFillOverTime(float targetFill)
    {
        Debug.Log("Change fill over time");

        float initialFill = _barValueMask.fillAmount;
        float timer = 0;

        while (timer < 1.0f)
        {
            timer += Time.deltaTime * _fillSpeed;
            _barValueMask.fillAmount = Mathf.Lerp(initialFill, targetFill / _maxFill, timer);
            yield return null;
        }

        // Ensure the fill amount is exactly the target value when the animation ends
        _barValueMask.fillAmount = targetFill / _maxFill;
    }
    
    private void SetInitialFill() => _barValueMask.fillAmount = 1;
    

}
