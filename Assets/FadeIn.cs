using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeIn : MonoBehaviour
{
    private CanvasGroup _canvasGroup;

    [SerializeField] private float FadeDuration = 3f;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _canvasGroup.alpha = 1;
    }

    void Start()
    {
        StartCoroutine(Transition());
    }

    private IEnumerator Transition()
    {
        yield return new WaitForSeconds(1);
        Debug.Log("Screen fade transitioning");
        LeanTween.alphaCanvas(_canvasGroup, 0, FadeDuration).setIgnoreTimeScale(true);
        yield return new WaitWhile(() => LeanTween.isTweening(gameObject));
    }

}
