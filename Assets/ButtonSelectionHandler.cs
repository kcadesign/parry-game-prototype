using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonSelectionHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler
{
    public GameObject ButtonSelector;

    [SerializeField] private float _verticalMoveAmount = 30f;
    [SerializeField] private float _moveTime = 0.1f;
    [Range(0, 2), SerializeField] private float _scaleAmount = 1.1f;

    private Vector3 _startPosition;
    private Vector3 _originalScale;

    private void Start()
    {
        _startPosition = transform.position;
        _originalScale = transform.localScale;
    }

/*    private IEnumerator AnimateButton(bool startingAnimation)
    {
        Vector3 endPosition;
        Vector3 endScale;

        float elapsedTime = 0f;
        while(elapsedTime < _moveTime)
        {
            elapsedTime += Time.deltaTime;
            if (startingAnimation)
            {
                endPosition = _startPosition + new Vector3(0, _verticalMoveAmount, 0);
                endScale = _originalScale * _scaleAmount;
            }
            else
            {
                endPosition = _startPosition;
                endScale = _originalScale;
            }

            Vector3 lerpedposition = Vector3.Lerp(transform.position, endPosition, (elapsedTime / _moveTime));
            Vector3 lerpedScale = Vector3.Lerp(transform.localScale, endScale, (elapsedTime / _moveTime));

            transform.position = lerpedposition;
            transform.localScale = lerpedScale;

            yield return null;
        }
    }
*/
    public void OnPointerEnter(PointerEventData eventData)
    {
        eventData.selectedObject = gameObject;
        LeanTween.scale(gameObject, _originalScale * _scaleAmount, _moveTime).setEase(LeanTweenType.easeOutExpo);
        ButtonSelector.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        eventData.selectedObject = null;
        LeanTween.scale(gameObject, _originalScale, _moveTime).setEase(LeanTweenType.easeOutExpo);
        ButtonSelector.SetActive(false);
    }

    public void OnSelect(BaseEventData eventData)
    {
        //StartCoroutine(AnimateButton(true));
        LeanTween.scale(gameObject, _originalScale * _scaleAmount, _moveTime).setEase(LeanTweenType.easeOutExpo);
        ButtonSelector.SetActive(true);
    }

    public void OnDeselect(BaseEventData eventData)
    {
        //StartCoroutine(AnimateButton(false));
        LeanTween.scale(gameObject, _originalScale, _moveTime).setEase(LeanTweenType.easeOutExpo);
        ButtonSelector.SetActive(false);
    }
}
