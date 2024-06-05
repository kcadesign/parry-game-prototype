using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateHover : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(MoveUp());
    }

    private IEnumerator MoveUp()
    {
        LeanTween.moveY(gameObject, gameObject.transform.position.y + 0.1f, 1f);
        yield return new WaitWhile(() => LeanTween.isTweening(gameObject));
        StartCoroutine(MoveDown());
    }

    private IEnumerator MoveDown()
    {
        LeanTween.moveY(gameObject, gameObject.transform.position.y - 0.1f, 1f);
        yield return new WaitWhile(() => LeanTween.isTweening(gameObject));
        StartCoroutine(MoveUp());

    }
}
