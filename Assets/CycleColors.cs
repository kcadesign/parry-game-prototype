using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CycleColors : MonoBehaviour
{
    private SpriteRenderer EyeballSprite;

    public Color[] Colors;
    public float CycleDuration = 1.0f;
    private int _currentIndex = 0;
    private float _timeElapsed = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        EyeballSprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
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
        EyeballSprite.color = currentColor;
    }
}
