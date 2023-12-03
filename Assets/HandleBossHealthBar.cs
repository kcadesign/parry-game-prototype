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

    private void OnEnable()
    {
        SendBossHealth.OnBossHealthChange += SendBossHealth_OnBossHealthChange;
    }

    private void OnDisable()
    {
        SendBossHealth.OnBossHealthChange -= SendBossHealth_OnBossHealthChange;
    }

    private void SendBossHealth_OnBossHealthChange(int currentHealth, int maxHealth)
    {
        _barValueMask = GetComponent<Image>();
        _maxFill = maxHealth;
        _currentFill = currentHealth;
        GetSetInitialFill();

        StartCoroutine(ChangeFillOverTime(currentHealth));
    }

    private IEnumerator ChangeFillOverTime(float targetFill)
    {
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

    private void GetSetInitialFill()
    {
        float currentFillPercentage = _currentFill / _maxFill;
        _barValueMask.fillAmount = currentFillPercentage;
    }

}
