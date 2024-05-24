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
        float elapsedTime = 0f;
        while (_canvasGroup.alpha < 1)
        {
            elapsedTime += Time.deltaTime;
            _canvasGroup.alpha = Mathf.Clamp01(elapsedTime / FadeDuration);
            yield return null;
        }
    }

    public override IEnumerator TransitionOut()
    {
        float elapsedTime = 0f;
        while (_canvasGroup.alpha > 0)
        {
            elapsedTime += Time.deltaTime;
            _canvasGroup.alpha = Mathf.Clamp01(1 - (elapsedTime / FadeDuration));
            yield return null;
        }
    }
}
