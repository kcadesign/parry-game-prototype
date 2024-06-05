using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateSelector : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 10f;

    private void OnEnable()
    {
        gameObject.transform.localScale = Vector3.zero;
        // lean tween scale from 0 to 1
        LeanTween.scale(gameObject, new Vector3(1, 1, 1), 0.5f).setEase(LeanTweenType.easeOutBack).setIgnoreTimeScale(true);
    }

    private void OnDisable()
    {
        // lean tween scale from 1 to 0
        LeanTween.scale(gameObject, new Vector3(0, 0, 0), 0.5f).setEase(LeanTweenType.easeInBack).setIgnoreTimeScale(true);
    }

    private void Update()
    {
        transform.Rotate(Vector3.back, _rotationSpeed * Time.unscaledDeltaTime);
    }
}
