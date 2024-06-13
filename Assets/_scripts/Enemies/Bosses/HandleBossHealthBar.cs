using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HandleBossHealthBar : MonoBehaviour
{
    private Image _barValueMask;
    public Image EaseBar;
    private float _currentFill;
    private float _maxFill;
    [SerializeField] private float _fillSpeed = 1.0f;

    private void Awake()
    {
        _barValueMask = GetComponent<Image>();
        SetInitialFill();
    }

    private void OnEnable()
    {
        HandleBossHealth.OnBossHealthChange += HandleBossHealth_OnBossHealthChange;
    }

    private void OnDisable()
    {
        HandleBossHealth.OnBossHealthChange -= HandleBossHealth_OnBossHealthChange;
    }

    private void HandleBossHealth_OnBossHealthChange(int currentHealth, int maxHealth)
    {
        _maxFill = maxHealth;
        _currentFill = currentHealth;

        _barValueMask.fillAmount = _currentFill / _maxFill;

        StopAllCoroutines();
        StartCoroutine(ChangeFillOverTime(_currentFill));
    }

    private IEnumerator ChangeFillOverTime(float targetFill)
    {
        float initialFill = EaseBar.fillAmount;
        float targetFillAmount = targetFill / _maxFill;
        float timer = 0f;

        while (timer < 1.0f)
        {
            timer += Time.deltaTime * _fillSpeed;
            EaseBar.fillAmount = Mathf.Lerp(initialFill, targetFillAmount, timer);
            yield return null;
        }

        // Ensure the fill amount is exactly the target value when the animation ends
        EaseBar.fillAmount = targetFillAmount;
    }

    private void SetInitialFill()
    {
        _barValueMask.fillAmount = 1;
        EaseBar.fillAmount = 1;
    }
}
