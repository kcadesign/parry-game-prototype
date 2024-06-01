using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WipeTransition : SceneTransition
{
    public GameObject _startObject;
    public GameObject _midObject;
    public GameObject _endObject;
    [SerializeField] private float _transitionDuration = 3f;

    public override IEnumerator TransitionIn()
    {
        gameObject.transform.position = _startObject.transform.position;
        LeanTween.moveX(gameObject, _midObject.transform.position.x, _transitionDuration).setEaseOutExpo().setIgnoreTimeScale(true);
        yield return new WaitWhile(() => LeanTween.isTweening(gameObject));
    }

    public override IEnumerator TransitionOut()
    {
        LeanTween.moveX(gameObject, _endObject.transform.position.x, _transitionDuration).setEaseOutExpo().setIgnoreTimeScale(true);
        yield return new WaitWhile(() => LeanTween.isTweening(gameObject));
    }
}
