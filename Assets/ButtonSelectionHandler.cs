using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class ButtonSelectionHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler
{
    public ButtonManager ButtonManager;

    public GameObject ButtonSelector;
    public TextMeshProUGUI ButtonText;
    private Vector3 _buttonTextOriginalPosition;

    [SerializeField] private float _moveTime = 0.1f;
    [Range(0, 2), SerializeField] private float _scaleAmount = 1.1f;

    private Vector3 _startPosition;
    private Vector3 _originalScale;

    private void Awake()
    {
        _startPosition = transform.position;
        _originalScale = transform.localScale;

        _buttonTextOriginalPosition = ButtonText.transform.position;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        eventData.selectedObject = gameObject;
        //LeanTween.scale(gameObject, _originalScale * _scaleAmount, _moveTime).setEase(LeanTweenType.easeOutExpo);
        //ButtonSelector.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        eventData.selectedObject = null;
        //LeanTween.scale(gameObject, _originalScale, _moveTime).setEase(LeanTweenType.easeOutExpo);
        //ButtonSelector.SetActive(false);
    }

    public void OnSelect(BaseEventData eventData)
    {
        Debug.Log($"{gameObject.name} selected");
        LeanTween.scale(gameObject, _originalScale * _scaleAmount, _moveTime).setEase(LeanTweenType.easeInOutExpo).setIgnoreTimeScale(true);
        
        ButtonManager.LastSelectedButton = gameObject;
        for (int i = 0; i < ButtonManager.Buttons.Length; i++)
        {
            if (ButtonManager.Buttons[i] == gameObject)
            {
                ButtonManager.LastSelectedIndex = i;
                return;
            }
        }
        ButtonSelector.SetActive(true);
    }

    public void OnDeselect(BaseEventData eventData)
    {
        LeanTween.scale(gameObject, _originalScale, _moveTime).setEase(LeanTweenType.easeInOutExpo).setIgnoreTimeScale(true);
        ButtonSelector.SetActive(false);

    }

    public void OnPointerDown()
    {
        Debug.Log("Pointer Down");
        MoveTextWithButton(new Vector3(transform.position.x, transform.position.y, transform.position.z));
    }

    public void OnPointerUp()
    {
        Debug.Log("Pointer Up");
        MoveTextWithButton(_buttonTextOriginalPosition);
     }

    public void MoveTextWithButton(Vector3 buttonPosition)
    {
        ButtonText.transform.position = buttonPosition;
    }

    private IEnumerator AnimateButtonIn()
    {
        yield return null;
    }

    private IEnumerator AnimateButtonOut()
    {
        yield return null;
    }
}
