using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using System;
using UnityEngine.UI;

public class ButtonSelectionHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler
{
    public static event Action OnButtonSelected;
    public static event Action OnButtonpressed;

    public ButtonManager ButtonManager;
    protected PlayerControls playerControls;

    public GameObject ButtonSelector;
    public GameObject ButtonShadow;
    private Sprite ButtonShadowSpriteDefault;
    public Sprite ButtonShadowSpritePressed;

    private float _buttonSelectMoveTime = 0.25f;
    private float _buttonMoveAmount = 10;

    private float _scaleAmount = 1.1f;
    private float _buttonPressMoveTime = 0.1f;

    private Vector3 _originalScale;
    private Vector3 _originalPosition;

    private void Awake()
    {
        playerControls = new PlayerControls();
        _originalScale = transform.localScale;

        ButtonShadowSpriteDefault = ButtonShadow.GetComponent<Image>().sprite;
    }

    private void OnEnable()
    {
        playerControls.Menus.Enable();
        playerControls.Menus.Execute.performed += Execute_performed;
        playerControls.Menus.Execute.canceled += Execute_canceled;
    }

    private void OnDisable()
    {
        playerControls.Menus.Disable();
        playerControls.Menus.Execute.performed -= Execute_performed;
        playerControls.Menus.Execute.canceled -= Execute_canceled;

        ResetButton();
    }

    private void Start()
    {
        StartCoroutine(GetButtonStartPosition());
    }

    private void Update()
    {
        ButtonShadow.transform.localScale = gameObject.transform.localScale;
    }

    private void Execute_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (EventSystem.current.currentSelectedGameObject == gameObject)
        {
            OnButtonPress();
        }
    }

    private void Execute_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (EventSystem.current.currentSelectedGameObject == gameObject)
        {
            OnButtonRelease();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        eventData.selectedObject = gameObject;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        eventData.selectedObject = null;
        ResetButton();
    }

    public void OnSelect(BaseEventData eventData)
    {
        OnButtonSelected?.Invoke();

        LeanTween.scale(gameObject, _originalScale * _scaleAmount, _buttonSelectMoveTime).setEase(LeanTweenType.easeInOutExpo).setIgnoreTimeScale(true);

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
        LeanTween.scale(gameObject, _originalScale, _buttonSelectMoveTime).setEase(LeanTweenType.easeInOutExpo).setIgnoreTimeScale(true);
        ResetButton();

        ButtonSelector.SetActive(false);
        Debug.Log("Button deselected: " + gameObject.name);
    }

    public void OnPointerDown()
    {
        if (EventSystem.current.currentSelectedGameObject == gameObject)
        {
            OnButtonPress();
        }
    }

    public void OnPointerUp()
    {
        if (EventSystem.current.currentSelectedGameObject == gameObject)
        {
            OnButtonRelease();
        }
    }

    private void OnButtonPress()
    {
        LeanTween.moveLocalY(gameObject, transform.localPosition.y - _buttonMoveAmount, _buttonPressMoveTime).setEase(LeanTweenType.linear).setIgnoreTimeScale(true);
        ButtonShadow.GetComponent<Image>().sprite = ButtonShadowSpritePressed;

        OnButtonpressed?.Invoke();
    }

    private void OnButtonRelease()
    {
        LeanTween.moveLocalY(gameObject, transform.localPosition.y + _buttonMoveAmount, _buttonPressMoveTime).setEase(LeanTweenType.linear).setIgnoreTimeScale(true);
        ButtonShadow.GetComponent<Image>().sprite = ButtonShadowSpriteDefault;
    }

    private IEnumerator GetButtonStartPosition()
    {
        yield return new WaitWhile(() => LeanTween.isTweening(gameObject));
        _originalPosition = transform.localPosition;
    }

    private void ResetButton()
    {
        transform.localScale = _originalScale;
        transform.localPosition = _originalPosition;
        ButtonShadow.transform.localScale = gameObject.transform.localScale;
        ButtonShadow.GetComponent<Image>().sprite = ButtonShadowSpriteDefault;
    }
}
