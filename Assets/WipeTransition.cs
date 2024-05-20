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
        float elapsedTime = 0f;
        while (elapsedTime < _transitionDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / _transitionDuration;
            transform.position = Vector3.Lerp(_startObject.transform.position, _midObject.transform.position, t);
            yield return null;
        }
    }

    public override IEnumerator TransitionOut()
    {
        float elapsedTime = 0f;
        while (elapsedTime < _transitionDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / _transitionDuration;
            transform.position = Vector3.Lerp(_midObject.transform.position, _endObject.transform.position, t);
            yield return null;
        }
    }
}
