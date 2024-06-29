using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepingPerryAnim : MonoBehaviour
{
    public GameObject SleepingPerry;
    public GameObject SleepBubble;
    private Vector3 _sleepBubbleBaseSize;
    private Vector3 _sleepBubbleMaxSize;
    public float AnimationLength = 2f;

    private void Awake()
    {
        _sleepBubbleBaseSize = SleepBubble.transform.localScale;
        _sleepBubbleMaxSize = _sleepBubbleBaseSize * 2f;
    }
    private void Start()
    {
        StartCoroutine(AnimateBubble());
        StartCoroutine(AnimateBody());
    }

    private IEnumerator AnimateBody()
    {
        while (true)
        {
            LeanTween.rotateZ(SleepingPerry, 5f, AnimationLength);
            yield return new WaitWhile(() => LeanTween.isTweening(SleepingPerry));
            LeanTween.rotateZ(SleepingPerry, 0f, AnimationLength);
            yield return new WaitWhile(() => LeanTween.isTweening(SleepingPerry));
        }
    }

    private IEnumerator AnimateBubble()
    {
        while (true)
        {
            LeanTween.scale(SleepBubble, _sleepBubbleMaxSize, AnimationLength);
            yield return new WaitWhile(() => LeanTween.isTweening(SleepBubble));
            LeanTween.scale(SleepBubble, _sleepBubbleBaseSize, AnimationLength);
            yield return new WaitWhile(() => LeanTween.isTweening(SleepBubble));
        }
    }
}
