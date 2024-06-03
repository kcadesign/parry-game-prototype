using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateParryText : MonoBehaviour
{
    private void Start()
    {
        gameObject.transform.localScale = new Vector3(0, 0, 0);
        StartCoroutine(AnimateText());
    }

    private IEnumerator AnimateText()
    {
        LeanTween.scale(gameObject, new Vector3(1f, 1f, 1f), 0.75f).setEaseInExpo();
        yield return new WaitWhile(() => LeanTween.isTweening(gameObject));
        LeanTween.scale(gameObject, new Vector3(0.8f, 0.8f, 0.8f), 0.75f);
        yield return new WaitWhile(() => LeanTween.isTweening(gameObject));
        LeanTween.scale(gameObject, new Vector3(1f, 1f, 1f), 0.75f);
        yield return new WaitWhile(() => LeanTween.isTweening(gameObject));
        LeanTween.scale(gameObject, new Vector3(0.8f, 0.8f, 0.8f), 0.75f);
        yield return new WaitWhile(() => LeanTween.isTweening(gameObject));
        LeanTween.scale(gameObject, new Vector3(1f, 1f, 1f), 0.75f);
        yield return new WaitWhile(() => LeanTween.isTweening(gameObject));
        LeanTween.scale(gameObject, new Vector3(0, 0f, 0f), 0.75f).setEaseInExpo();

    }
}
