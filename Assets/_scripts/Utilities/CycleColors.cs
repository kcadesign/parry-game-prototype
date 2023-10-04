using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CycleColors : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;

    private IParryable _deflect;
    private bool _deflected;

    public Color[] Colors;
    public float CycleDuration = 1.0f;
    private int _currentIndex = 0;
    private float _timeElapsed = 0.0f;

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _deflect = GetComponent<IParryable>();

        if (_deflect == null)
        {
            Debug.LogWarning("No collision handler component found on " + gameObject);
        }
    }

    private void OnEnable()
    {
        if (_deflect != null)
        {
            _deflect.OnDeflect += _deflect_OnDeflect;
        }
    }

    private void OnDisable()
    {
        if (_deflect != null)
        {
            _deflect.OnDeflect -= _deflect_OnDeflect;
        }
    }

    private void _deflect_OnDeflect(GameObject arg1, bool deflected)
    {
        _deflected = deflected;
    }

    private void Update()
    {
        if (!_deflected)
        {
            HandleCycleColors();
        }
    }

    private void HandleCycleColors()
    {
        _timeElapsed += Time.deltaTime;

        if (_timeElapsed >= CycleDuration)
        {
            _timeElapsed -= CycleDuration;
            _currentIndex = (_currentIndex + 1) % Colors.Length;
        }

        Color currentColor = Color.Lerp(Colors[_currentIndex], Colors[(_currentIndex + 1) % Colors.Length], _timeElapsed / CycleDuration);
        _spriteRenderer.color = currentColor;
    }
}
