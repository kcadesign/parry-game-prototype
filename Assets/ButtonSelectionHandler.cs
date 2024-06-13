using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using System;

public class ButtonSelectionHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler
{
    public static event Action OnButtonSelected;
    public static event Action OnButtonpressed;

    public ButtonManager ButtonManager;
    protected PlayerControls playerControls;

    public GameObject ButtonSelector;
    public TextMeshProUGUI ButtonText;
    private Vector3 _buttonPressedPosition;
    private float _buttonMoveAmount = 10;

    [SerializeField] private float _moveTime = 0.1f;
    [Range(0, 2), SerializeField] private float _scaleAmount = 1.1f;

    private Vector3 _startPosition;
    private Vector3 _originalScale;

    private bool _buttonSelected = false;
    private bool _executePressed = false;

    private void Awake()
    {
        playerControls = new PlayerControls();

        //_buttonTextOriginalPosition = ButtonText.transform.position;
    }

    private void OnEnable()
    {
        playerControls.Menus.Enable();

        playerControls.Menus.Execute.performed += Execute_performed;
        playerControls.Menus.Execute.canceled += Execute_canceled;


        _startPosition = transform.position;
        _originalScale = transform.localScale;
    }

    private void OnDisable()
    {
        playerControls.Menus.Disable();

        playerControls.Menus.Execute.performed -= Execute_performed;
        playerControls.Menus.Execute.canceled -= Execute_canceled;
    }

    private void Execute_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        _executePressed = true;
        //Debug.Log("Execute performed");
        if (_buttonSelected && _executePressed)
        {
            MoveTextWithButton(new Vector3(transform.position.x, transform.position.y, transform.position.z));
            OnButtonpressed?.Invoke();
        }

        StartCoroutine(CancelExecute());
    }

    private void Execute_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        _executePressed = false;
        //Debug.Log("Execute canceled");
        MoveTextWithButton(new Vector3(transform.position.x, transform.position.y + _buttonMoveAmount, transform.position.z));
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        eventData.selectedObject = gameObject;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        eventData.selectedObject = null;
    }

    public void OnSelect(BaseEventData eventData)
    {
        OnButtonSelected?.Invoke();

        //_buttonTextOriginalPosition = ButtonText.transform.position;

        //Debug.Log($"{gameObject.name} selected");
        _buttonSelected = true;

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
        _buttonSelected = false;

        LeanTween.scale(gameObject, _originalScale, _moveTime).setEase(LeanTweenType.easeInOutExpo).setIgnoreTimeScale(true);
        ButtonSelector.SetActive(false);
    }

    public void OnPointerDown()
    {
        MoveTextWithButton(new Vector3(transform.position.x, transform.position.y, transform.position.z));
        OnButtonpressed?.Invoke();
    }

    public void OnPointerUp()
    {
        MoveTextWithButton(new Vector3(transform.position.x, transform.position.y + _buttonMoveAmount, transform.position.z));
    }

    public void MoveTextWithButton(Vector3 buttonPosition)
    {
        ButtonText.transform.position = buttonPosition;
    }

    private IEnumerator CancelExecute()
    {
        yield return new WaitForSeconds(0.1f);
        _executePressed = false;
        MoveTextWithButton(new Vector3(transform.position.x, transform.position.y + _buttonMoveAmount, transform.position.z));
    }
}
