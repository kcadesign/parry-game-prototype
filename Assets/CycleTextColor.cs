using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CycleTextColor : MonoBehaviour
{
    private TMP_Text _text;

    public Color[] Colors;
    public float CycleDuration = 1.0f;
    private int _currentIndex = 0;
    private float _timeElapsed = 0.0f;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();        
    }

    private void Update()
    {
        HandleCycleColors();
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
        _text.color = currentColor;
    }

}
