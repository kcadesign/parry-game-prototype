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
        StartCoroutine(FadeTextInOut());
        //StartCoroutine(FadeOutText());
    }

    private void OnDisable()
    {
        ButtonContainer.SetActive(false);
    }

    private IEnumerator FadeTextInOut()
    {
        LeanTween.alphaCanvas(_canvasGroup, 1, 1.5f).setDelay(0.5f).setIgnoreTimeScale(true);
        yield return new WaitWhile(() => LeanTween.isTweening(gameObject));
        LeanTween.alphaCanvas(_canvasGroup, 0, 1f).setIgnoreTimeScale(true);
        yield return new WaitWhile(() => LeanTween.isTweening(gameObject));

        ButtonContainer.SetActive(true);
    }

/*    private IEnumerator FadeOutText()
    {
        LeanTween.alphaCanvas(_canvasGroup, 0, 1f).setDelay(2f).setIgnoreTimeScale(true);
        yield return new WaitWhile(() => LeanTween.isTweening(gameObject));
        ButtonContainer.SetActive(true);
    }
*/}
