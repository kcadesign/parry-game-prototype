using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlVignette : MonoBehaviour
{
    private CanvasGroup _vignetteCanvasGroup;

    private void Awake()
    {
        _vignetteCanvasGroup = GetComponent<CanvasGroup>();
        _vignetteCanvasGroup.alpha = 0;
    }

    private void OnEnable()
    {
        HandlePlayerHealth.OnPlayerHurtSmall += HandlePlayerHealth_OnPlayerHurtSmall;
    }

    private void OnDisable()
    {
        HandlePlayerHealth.OnPlayerHurtSmall -= HandlePlayerHealth_OnPlayerHurtSmall;
    }

    private void HandlePlayerHealth_OnPlayerHurtSmall(bool hurt)
    {
        Debug.Log("Player hurt: " + hurt);
        if (hurt)
        {
            StartCoroutine(FlashVignette());
            //StartCoroutine(AnimateOut());
        }
    }

    private IEnumerator FlashVignette()
    {
        LeanTween.alphaCanvas(_vignetteCanvasGroup, 1, 0.2f);
        yield return new WaitWhile(() => LeanTween.isTweening(gameObject));
        LeanTween.alphaCanvas(_vignetteCanvasGroup, 0, 0.5f);
    }


}
