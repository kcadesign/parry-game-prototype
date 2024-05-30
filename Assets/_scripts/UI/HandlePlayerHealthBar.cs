using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HandlePlayerHealthBar : MonoBehaviour
{
    public Image BarValueMask;
    private float _currentFill;
    private float _maxFill;
    [SerializeField] private float _fillSpeed = 1.0f;

    private void Awake()
    {
        //BarValueMask.fillAmount = 1.0f;
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
        Debug.Log($"Initializing Health: maxHealth = {maxHealth}");

        if (maxHealth <= 0)
        {
            Debug.LogError("Max Health must be greater than 0");
            return;
        }

        _maxFill = maxHealth;
        _currentFill = maxHealth;
        UpdateHealthBarImmediately();
    }

    private void HandlePlayerHealth_OnHealthChange(int currentHealth)
    {
        Debug.Log($"Health Changed: currentHealth = {currentHealth}");

        _currentFill = currentHealth;
        // Use a coroutine to smoothly update the fill amount
        StartCoroutine(ChangeFillOverTime(currentHealth));
    }

    private IEnumerator ChangeFillOverTime(float targetHealth)
    {
        float initialFill = BarValueMask.fillAmount;
        float targetFill = targetHealth / _maxFill;
        float timer = 0;

        while (timer < 1.0f)
        {
            timer += Time.deltaTime * _fillSpeed;
            BarValueMask.fillAmount = Mathf.Lerp(initialFill, targetFill, timer);
            yield return null;
        }

        // Ensure the fill amount is exactly the target value when the animation ends
        BarValueMask.fillAmount = targetFill;
    }

    private void UpdateHealthBarImmediately()
    {
        BarValueMask.fillAmount = _currentFill / _maxFill;
    }
}
