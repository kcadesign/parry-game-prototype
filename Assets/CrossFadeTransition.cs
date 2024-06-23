using System.Collections;
using UnityEngine;

public class CrossFadeTransition : SceneTransition
{
    private CanvasGroup _canvasGroup;

    [SerializeField] private float FadeDuration = 3f;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public override IEnumerator TransitionIn()
    {
        Debug.Log("Cross fade transitioning in");
        LeanTween.alphaCanvas(_canvasGroup, 1, FadeDuration).setIgnoreTimeScale(true);
        yield return new WaitWhile(() => LeanTween.isTweening(gameObject));
    }

    public override IEnumerator TransitionOut()
    {
        Debug.Log("Cross fade transitioning out");
        LeanTween.alphaCanvas(_canvasGroup, 0, FadeDuration).setIgnoreTimeScale(true);
        yield return new WaitWhile(() => LeanTween.isTweening(gameObject));
    }
}
