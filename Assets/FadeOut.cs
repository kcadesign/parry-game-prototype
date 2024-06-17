using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOut : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    [SerializeField] private float _fadeOutDuration;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        StartCoroutine(FadeOutSprite(_fadeOutDuration));
    }

    public IEnumerator FadeOutSprite(float fadeOutDuration)
    {
        while (_spriteRenderer.color.a > 0)
        {
            _spriteRenderer.color = new Color(_spriteRenderer.color.r, _spriteRenderer.color.g, _spriteRenderer.color.b, _spriteRenderer.color.a - Time.deltaTime / fadeOutDuration);
            yield return null;
        }
    }
}
