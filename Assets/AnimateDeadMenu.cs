using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateDeadMenu : MonoBehaviour
{
    public GameObject ButtonContainer;
    private CanvasGroup _canvasGroup;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _canvasGroup.alpha = 0;
    }

    private void OnEnable()
    {
        StartCoroutine(FadeInText());
        StartCoroutine(FadeOutText());
    }

    private void OnDisable()
    {
        ButtonContainer.SetActive(false);
    }

    private IEnumerator FadeInText()
    {
        LeanTween.alphaCanvas(_canvasGroup, 1, 1f);
        yield return null;
    }

    private IEnumerator FadeOutText()
    {
        LeanTween.alphaCanvas(_canvasGroup, 0, 1f).setDelay(3f);
        yield return new WaitWhile(() => LeanTween.isTweening(gameObject));
        ButtonContainer.SetActive(true);
    }
}
